using System.Linq.Expressions;
using Core.Entities.Abstracts;
using Core.Extensions.Paging;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework;


// If you want to use the Entity Framework Core in your project, you can use this class for generic repository pattern.
// You can use by add services.AddScoped<IRoleRepository, EfRoleRepository> in DataAccessServiceRegistration file.
public abstract class EfEntityRepositoryBase<T>(DbContext context) : IEntityRepository<T>
    where T : AbstractEntity, new()
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
        context.SaveChanges();
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
        context.SaveChanges();
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
        context.SaveChanges();
    }

    public T? Get(Expression<Func<T, bool>> filter)
    {
        return context.Set<T>().AsNoTracking().SingleOrDefault(filter);
    }

    public T? GetById(Guid id)
    {
        return context.Set<T>().AsNoTracking().SingleOrDefault(t => t.Id == id);
    }

    public PageableModel<T> GetList(int index, int size, Expression<Func<T, bool>>? filter = null)
    {
        var queryable = filter is null
            ? context.Set<T>().AsNoTracking()
            : context.Set<T>().AsNoTracking().Where(filter);
        return queryable.ToPaginate(index, size);
    }
}