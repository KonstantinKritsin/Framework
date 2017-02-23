using System.Linq;
using System.Security.Principal;
using System.Threading;
using Project.Framework.CrossCuttingConcerns.Identity;

namespace Project.Common.InternalContracts.Authentication
{
    public class ProjectPrincipal : IProjectPrincipal
    {
        private UserRoles _roles;
        public ProjectPrincipal(IMember member)
        {
            Member = member;

            var modelMember = member as IModelMember<UserIdentityModel>;
            if (modelMember != null)
                Lang = modelMember.Model?.Lang;

            Roles = GetRoles();

            if (Member != null)
                CanBeImpersonated = Roles.HasFlag(UserRoles.Admin) || Roles.HasFlag(UserRoles.Impersonation);
        }

        public bool IsInRole(string role)
        {
            return Member.Roles.Contains(role);
        }

        public IIdentity Identity { get; } = Thread.CurrentPrincipal.Identity;

        public IMember Member { get; }

        public bool CanBeImpersonated { get; }

        public string Lang { get; }

        public UserRoles Roles { get; }

        private UserRoles GetRoles()
        {
            if (_roles != UserRoles.None)
                return _roles;

            var user = Member;
            if (user == null)
                return _roles;

            foreach (var role in user.Roles)
            {
                switch (role)
                {
                    case "Administrators":
                        _roles |= UserRoles.Admin;
                        break;
                    case "Role1":
                        _roles |= UserRoles.Role1;
                        break;
                    case "Role2":
                        _roles |= UserRoles.Role2;
                        break;
                    case "Impersonation":
                        _roles |= UserRoles.Role2;
                        break;
                }
            }
            return _roles;
        }
    }
}