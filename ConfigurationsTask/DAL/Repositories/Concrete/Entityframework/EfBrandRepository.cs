using System.Linq.Expressions;
using ConfigurationsTask.DAL.Repositories.Abstract;
using ConfigurationsTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationsTask.DAL.Repositories.Concrete.Entityframework;

public class EfBrandRepository: IBrandRepository
{
    private readonly ConfugurationDbContext _context;
    public EfBrandRepository(ConfugurationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Brand> GetBrandAsync(Expression<Func<Brand, bool>> filter)
    {
      return await _context.Brands.FirstOrDefaultAsync(filter);
    }

    public async Task<List<Brand>> GetBrandsAsync(Expression<Func<Brand, bool>> filter = null)
    {
       return filter == null
          ? await _context.Brands.ToListAsync()
          : await _context.Brands.Where(filter).ToListAsync();
    }

    public async Task AddAsync(Brand brand)
    {
       await _context.Brands.AddAsync(brand);
    }

    public void UpdateAsync(Brand brand)
    {
       _context.Brands.Update(brand);
    }

    public void Remove(Brand brand)
    {
       _context.Brands.Remove(brand);
    }

    public async Task SaveAsync()
    {
       await _context.SaveChangesAsync();
    }
}