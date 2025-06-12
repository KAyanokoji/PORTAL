using Microsoft.EntityFrameworkCore;
using PORTAL.DOMAIN.Entities;
using PORTAL.DOMAIN.Interfaces;
using PORTAL.INFRASTRUCTURE.Persistence;

namespace PORTAL.INFRASTRUCTURE.Repositories
{
    public class PermissionRepository(ApplicationDbContext context) : GenericRepository<Permission>(context), IPermissionRepository
    {
        // private readonly ApplicationDbContext context;

        // public async Task<Permission> Create(Permission permission)
        // {
        //     var entityEntry = await _context.Permissions
        //         .AddAsync(permission)
        //         .ConfigureAwait(false);

        //     await _context.SaveChangesAsync()
        //         .ConfigureAwait(false);

        //     return entityEntry.Entity;
        // }

        // public async Task<Permission?> GetPermissionById(int id)
        // {

        //     return await _context.Permissions
        //     .AsNoTracking()
        //     .FirstOrDefaultAsync(x => x.PermissionId == id)
        //     .ConfigureAwait(false);
        // }

        // public async Task<List<Permission>> GetPermissions()
        // {
        //     return await _context.Permissions
        //         .AsNoTracking()
        //         .ToListAsync()
        //         .ConfigureAwait(false);
        // }

        // public async Task<Permission> UpdatePermission(Permission permission)
        // {
        //     var entity = _context.Permissions.Update(permission).Entity;
        //     await _context.SaveChangesAsync().ConfigureAwait(false); 
        //     return entity;
        // }
    }
}