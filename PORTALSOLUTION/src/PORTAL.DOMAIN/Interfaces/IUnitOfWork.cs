namespace PORTAL.DOMAIN.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPermissionRepository Permissions { get; }
    IRoleRepository Roles{ get; }

    int Complete();
    Task<int> CompleteAsync();

}