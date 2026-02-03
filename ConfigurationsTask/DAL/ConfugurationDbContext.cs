using System.Reflection;
using ConfigurationsTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationsTask.DAL;

public class ConfugurationDbContext:DbContext
{
   public ConfugurationDbContext(DbContextOptions<ConfugurationDbContext> options) : base(options)
   {
      
   }
   public DbSet<Product> Products { get; set; }
   public DbSet<Brand> Brands { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
     modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
     
     
   }
}