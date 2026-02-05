using System.Reflection;
using ConfigurationsTask.Entities;
using ConfigurationsTask.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationsTask.DAL;

public class ConfugurationDbContext: IdentityDbContext<AppUser>
{
   public ConfugurationDbContext(DbContextOptions<ConfugurationDbContext> options) : base(options)
   {
      
   }
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfugurationDbContext).Assembly);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
   }
   public DbSet<Product> Products { get; set; }
   public DbSet<Brand> Brands { get; set; }

}