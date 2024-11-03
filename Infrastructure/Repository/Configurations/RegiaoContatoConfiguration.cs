using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    public class RegiaoContatoConfiguration : IEntityTypeConfiguration<RegiaoContato>
    {
        public void Configure(EntityTypeBuilder<RegiaoContato> builder)
        {
            builder.ToTable("RegiaoContato");
            builder.HasKey(p => p.Id);
            //builder.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.DDD).HasColumnType("INT").IsRequired();
            builder.Property(p => p.Regiao).HasColumnType("VARCHAR(15)").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("VARCHAR(20)").IsRequired();
        }
    }
}
