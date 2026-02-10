using System.Linq.Expressions;
using ConfigurationsTask.Core.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationsTask.Core.DAL.Repositories.Concrete.Entityframework;

public class EfBaseRepository<TEntity,TContext>:IBaseRepository<TEntity>
where TEntity : class,new()
where TContext: DbContext
{
   private readonly TContext _context;
   private IBaseRepository<TEntity> _baseRepositoryImplementation;

   public EfBaseRepository(TContext context)
    {
        _context = context;
    }
    
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,params  string [] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        query =GetQueryable(includes);
        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,params  string [] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        query =GetQueryable(includes);
        return filter == null
            ? await  query.ToListAsync()
            : await  query.Where(filter).ToListAsync();
    }

    public async Task<List<TEntity>> GetAllPaginateAsync(int page, int size, Expression<Func<TEntity, bool>> filter = null, params string[] includes)
    {
    
        IQueryable<TEntity> query = _context.Set<TEntity>();
        query =GetQueryable(includes);
        return filter == null
            ? await query.Skip((page-1)*size).Take(size).ToListAsync()
            : await query.Skip((page-1)*size).Take(size).Where(filter).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    public IQueryable<TEntity> GetQueryable(string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}