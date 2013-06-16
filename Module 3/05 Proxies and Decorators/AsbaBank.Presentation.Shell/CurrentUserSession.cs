using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using AsbaBank.Core;
using AsbaBank.Core.Commands;

namespace AsbaBank.Presentation.Shell
{
    public class CurrentUserSession : ICurrentUserSession
    {
        private readonly IList<UserRole> allowedRoles;
        public IIdentity Identity { get; private set; }


        public CurrentUserSession(IIdentity identity, params UserRole[] roles)
        {
            Identity = identity;
            allowedRoles = roles.ToList();
        }

        public override string ToString()
        {
            return String.Format("{0} with roles {1}", Identity.Name, String.Join(", ", allowedRoles));
        }

        public bool IsInRole(IList<UserRole> roles)
        {
            return roles.Any(s => allowedRoles.Contains(s));
        }

        public bool IsInRole(string role)
        {
            UserRole userRole;
            var result = Enum.TryParse(role, true, out userRole);
            return result && allowedRoles.Contains(userRole);
        }
    }
}