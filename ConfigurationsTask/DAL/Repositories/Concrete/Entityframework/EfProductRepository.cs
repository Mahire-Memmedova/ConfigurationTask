using System.Linq.Expressions;
using ConfigurationsTask.Core.DAL.Repositories.Concrete.Entityframework;
using ConfigurationsTask.DAL.Repositories.Abstract;
using ConfigurationsTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationsTask.DAL.Repositories.Concrete.Entityframework;

public class EfProductRepository:EfBaseRepository<Product,ConfugurationDbContext>,IProductRepository
{
    public EfProductRepository(ConfugurationDbContext context) : base(context)
    {
    }
}