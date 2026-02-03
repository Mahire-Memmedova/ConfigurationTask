using ConfigurationsTask.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationsTask.DAL.Configurations;

public class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property<int>("Id");
        builder.Property(p => p.Name).HasMaxLength(50).HasDefaultValue("Kitab").IsRequired();
    }
}