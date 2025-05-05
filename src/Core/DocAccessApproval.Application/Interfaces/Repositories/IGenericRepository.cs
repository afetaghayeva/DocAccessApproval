using DocAccessApproval.Domain.SeedWork;
using DocAccessApproval.Domain.SeedWork.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DocAccessApproval.Application.Interfaces.Repositories;

public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, Func<IQueryable<T>,
                                                                        IIncludableQueryable<T, object>>?
                                                                    include = null);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                      Func<IQueryable<T>,IIncludableQueryable<T, object>>?
                                                                    include = null);


    Task<IPaginate<T>> GetAllAsPaginateAsync(int index = 0, int size = 10, Expression<Func<T, bool>>? predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    Task<T> AddAsync(T entity);
    T Update(T entity);
    T Delete(T entity);
}

