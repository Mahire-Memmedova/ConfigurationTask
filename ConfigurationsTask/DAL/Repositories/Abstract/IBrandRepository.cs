using System.Linq.Expressions;
using ConfigurationsTask.Entities;
using ConfigurationsTask.Entities.Dtos.Brands;

namespace ConfigurationsTask.DAL.Repositories.Abstract;

public interface IBrandRepository
{
    public  Task<Brand> GetBrandAsync(Expression<Func<Brand, bool>> filter );
    public Task<List<Brand>> GetBrandsAsync(Expression<Func<Brand, bool>> filter=null);
    public Task AddAsync(Brand brand);
    public void UpdateAsync(Brand brand);
    public void Remove(Brand brand);
    public Task SaveAsync();
}