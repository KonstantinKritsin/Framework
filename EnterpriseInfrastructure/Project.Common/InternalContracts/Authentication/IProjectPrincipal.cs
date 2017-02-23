using Project.Framework.CrossCuttingConcerns.Identity;

namespace Project.Common.InternalContracts.Authentication
{
    public interface IProjectPrincipal : IPrincipal<IMember>
    {
        UserRoles Roles { get; }
        bool CanBeImpersonated { get; }
        string Lang { get; }
    }
}