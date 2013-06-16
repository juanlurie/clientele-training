using System;
using System.Collections.Generic;

namespace AsbaBank.Core.Commands
{
    public enum UserRole
    {
        Guest,
        Administrator
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeAttribute : Attribute
    {
        public UserRole[] Roles { get; private set; }

        public AuthorizeAttribute(UserRole role)
        {
            Roles = new[]
            {
                role
            };
        }

        public AuthorizeAttribute(params UserRole[] roles)
        {
            Roles = roles;
        }

        public override string ToString()
        {
            return String.Join(", ", Roles);
        }
    }
}