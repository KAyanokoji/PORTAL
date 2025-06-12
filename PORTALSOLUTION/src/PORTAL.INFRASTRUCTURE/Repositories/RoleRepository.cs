using PORTAL.DOMAIN.Entities;
using PORTAL.DOMAIN.Interfaces;
using PORTAL.INFRASTRUCTURE.Persistence;

namespace PORTAL.INFRASTRUCTURE.Repositories;

public class RoleRepository(ApplicationDbContext context) :GenericRepository<Role>(context), IRoleRepository
{

    // public Task<Role> CreateRole(Role request)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Role> DeleteRole(int roleId)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<IEnumerable<Role>> GetRole()
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Role> GetRoleById(int roleId)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Role> UpdateRole(Role request, int roleId)
    // {
    //     throw new NotImplementedException();
    // }
}