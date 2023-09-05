using AutoMapper;
using HouseOrganizer.DTOs.Casa;
using HouseOrganizer.Entities;
using HouseOrganizer.Repositories;
using HouseOrganizer.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

#region Entity

builder.Services.AddScoped<ICasaRepository, CasaRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);

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
