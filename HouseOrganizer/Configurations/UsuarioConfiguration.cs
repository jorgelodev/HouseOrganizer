using HouseOrganizer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOrganizer.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnType("int")
                .UseIdentityColumn();

            builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.NomeUsuario).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Senha).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Permissao).HasConversion<int>().IsRequired();
        }
    }
}
