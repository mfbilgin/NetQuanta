namespace Core.Extensions.Paging;

public static class PaginationExtension
{
    public static PageableModel<TEntity> ToPaginate<TEntity>(this IQueryable<TEntity>? queryable, int index, int size)
    {
        ArgumentNullException.ThrowIfNull(queryable);
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(size, 1);
        
        var count = queryable.Count();
        var data = queryable.Skip((index - 1) * size).Take(size).ToList();
        
        return new PageableModel<TEntity>(data, index, size, count);
    }
}