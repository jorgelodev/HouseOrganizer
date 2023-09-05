using HouseOrganizer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOrganizer.Configurations
{
    public class CasaConfiguration : IEntityTypeConfiguration<Casa>
    {
        public void Configure(EntityTypeBuilder<Casa> builder)
        {
            builder.ToTable("Casa");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnType("int")
                .UseIdentityColumn();

            builder.Property(p => p.Descricao).HasColumnType("varchar(100)");            
            
        }
    }
}
