using System.Collections.Generic;
using System.Security.Principal;

namespace AsbaBank.Core
{
    public interface ICurrentUserSession : IPrincipal
    {
        bool IsInRole(IEnumerable<string> roles);
    }
}