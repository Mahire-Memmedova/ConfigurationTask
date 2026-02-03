using System.Data;
using ConfigurationsTask.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ConfigurationsTask.DAL.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property<int>("Id");
        builder.Property(b => b.Name).HasMaxLength(50).IsRequired()
            .HasColumnType(SqlDbType.NVarChar.ToString());
        builder.HasMany(p => p.Products)
            .WithOne(b => b.Brand)
            .HasForeignKey(b => b.BrandId);

    }
    
}