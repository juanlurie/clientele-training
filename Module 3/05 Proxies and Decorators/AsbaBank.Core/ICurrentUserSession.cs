using System.Collections.Generic;
using System.Security.Principal;
using AsbaBank.Core.Commands;

namespace AsbaBank.Core
{
    public interface ICurrentUserSession : IPrincipal
    {
        bool IsInRole(IList<UserRole> roles);
    }
}