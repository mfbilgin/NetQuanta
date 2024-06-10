using System.Linq.Expressions;
using Core.Entities.Abstracts;
using Core.Extensions.Paging;

namespace Core.DataAccess;

public interface IEntityRepository<T> where T :  AbstractEntity, new()
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    T? Get(Expression<Func<T, bool>> filter);
    T? GetById(Guid id);
    PageableModel<T> GetList(int index, int size, Expression<Func<T, bool>>? filter = null);
}