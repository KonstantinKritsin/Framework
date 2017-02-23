using System;

namespace Project.Common.InternalContracts.Authentication
{
    [Flags]
    public enum UserRoles : uint
    {
        None = 0x0,
        Admin = 0x1,
        Role1 = 0x2,
        Role2 = 0x4,
        Impersonation = 0x8,

        All = ~None
    }
}