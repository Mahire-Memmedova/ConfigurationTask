using System.Linq.Expressions;

namespace ConfigurationsTask.Core.DAL.Repositories.Abstract;

public interface IBaseRepository<TEntity>
where TEntity : class,new()
{
    public  Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,params  string [] includes);
    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter=null,params  string [] includes);   
    public Task<List<TEntity>> GetAllPaginateAsync(int page, int size, Expression<Func<TEntity, bool>> filter=null,params  string [] includes);   
    Task AddAsync(TEntity entity);
    public void Update(TEntity entity);
    public void Remove(TEntity entity);
    public Task SaveAsync();
}