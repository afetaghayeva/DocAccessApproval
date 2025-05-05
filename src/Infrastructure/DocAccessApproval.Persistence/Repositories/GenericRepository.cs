using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.SeedWork;
using DocAccessApproval.Domain.SeedWork.Paging;
using DocAccessApproval.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using System.Linq.Expressions;

namespace DocAccessApproval.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    protected readonly DocAccessApprovalDbContext _context;

    public GenericRepository(DocAccessApprovalDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public virtual async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);

        return entity;
    }

    public virtual async Task<IPaginate<T>> GetAllAsPaginateAsync(int index = 0, int size = 10,
                                                    Expression<Func<T, bool>>? predicate = null,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                    Func<IQueryable<T>, IIncludableQueryable<T, object>>?
                                                                    include = null)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        if (include != null) query = include(query);

        if (orderBy != null)
            return await orderBy(query).ToPaginateAsync(index, size);

        return await query.ToPaginateAsync(index, size);
    }

    public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                     Func<IQueryable<T>, IIncludableQueryable<T, object>>?
                                                                    include = null)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        if (include != null) query = include(query);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var query = _context.Set<T>().AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        if (include != null) query = include(query);

        return await query.FirstOrDefaultAsync(predicate);
    }

    public virtual T Update(T entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Modified;
        return entity;
    }
}

public static class IQueryablePaginateExtensions
{
    public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size,
                                                              int from = 0,
                                                              CancellationToken cancellationToken = default)
    {
        if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");

        int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
        List<T> items = await source.Skip((index - from) * size).Take(size).ToListAsync(cancellationToken)
                                    .ConfigureAwait(false);
        Paginate<T> list = new()
        {
            Index = index,
            Size = size,
            From = from,
            Count = count,
            Items = items,
            Pages = (int)Math.Ceiling(count / (double)size)
        };

        return list;
    }
}