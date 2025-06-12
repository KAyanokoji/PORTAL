using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PORTAL.DOMAIN.Entities;
using PORTAL.DOMAIN.Interfaces;
using PORTAL.INFRASTRUCTURE.Persistence;

namespace PORTAL.INFRASTRUCTURE.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext context = context;

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        context.Set<T>().AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await context.Set<T>().AddRangeAsync(entities);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().Where(expression);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().Where(expression).ToListAsync();
    }

    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public T? GetById(int id)
    {
        var result = context.Set<T>().Find(id);
        return result;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var result = await context.Set<T>().FindAsync(id);
        return result;
    }

    public void Update(T entity)
    {
        //context.Update(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public async Task SoftDeleteAsync(T entity)
    {
        await UpdateAsync(entity);
    }

    // public async Task<int> GetLatestId()
    // {
    //     var latestId = await context.Set<T>().MaxAsync(x => (int?)x.Id);
    //     return  latestId;
    // // }
    // public async Task<int> GetLatestId<T>() where T : BaseEntity
    // {
    //     var latestId = await context.Set<T>().MaxAsync(x => (int?)x.Id);
    //     return latestId ?? 0;
    // }

    public async Task<int> GetLatestId<T>() where T : BaseEntity
    {
        var latestId = await context.Set<T>().MaxAsync(x => (int?)x.Id);
        return latestId ?? 0;
    }
}