using AutoMapper;
using HouseOrganizer.DTOs.Casa;
using HouseOrganizer.Entities;
using HouseOrganizer.Repositories;
using HouseOrganizer.Repositories.Interfaces;
using HouseOrganizer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    //CONFIGURANDO ARQUIVO DE DOCUMENTAÇAO DO SUMMARY
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HouseOrganizer", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    //CONFIGURANDO O SWAGGER PARA RECEBER O JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization Header - utilizado com Bearer Authentication." + Environment.NewLine +
        "Digite 'Bearer' [espaço] e então seu token no campo abaixo" + Environment.NewLine +
        "Exemplo (informar sem as aspas): 'Bearer 1234abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

#region AutoMapper

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<CasaDTO, Casa>().ReverseMap();
    cfg.CreateMap<CadastrarCasaDTO, Casa>().ReverseMap();
    cfg.CreateMap<AlterarCasaDTO, Casa>().ReverseMap();
});

IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

#endregion

#region Mapeamento de Serviços

builder.Services.AddScoped<ICasaRepository, CasaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);

#endregion

#region JWT
// CONFIGURAÇÃO JWT
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Secret"));

builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

    });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
