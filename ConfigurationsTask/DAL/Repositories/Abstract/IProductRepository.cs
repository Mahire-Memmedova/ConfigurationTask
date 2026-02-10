using ConfigurationsTask.Entities;
using System.Linq.Expressions;
using ConfigurationsTask.Core.DAL.Repositories.Abstract;

namespace ConfigurationsTask.DAL.Repositories.Abstract;

public partial interface IProductRepository:IBaseRepository<Product>
{
    
}