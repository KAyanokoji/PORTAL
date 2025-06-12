using System.Linq.Expressions;
using PORTAL.DOMAIN.Entities;

namespace PORTAL.DOMAIN.Interfaces;


public interface IGenericRepository<T> where T : class
{


    T? GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);

    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    #region  ASYNC INTERFACES
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
    Task SoftDeleteAsync(T entity);

    Task<int> GetLatestId<T>() where T : BaseEntity;

    #endregion
}