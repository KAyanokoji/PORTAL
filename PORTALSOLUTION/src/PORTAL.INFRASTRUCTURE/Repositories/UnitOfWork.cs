using PORTAL.DOMAIN.Interfaces;
using PORTAL.INFRASTRUCTURE.Persistence;

namespace PORTAL.INFRASTRUCTURE.Repositories;


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Permissions = new PermissionRepository(_context);
        Roles = new RoleRepository(_context);
    }

    public IPermissionRepository Permissions { get; private set; }

    public IRoleRepository Roles { get; private set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}