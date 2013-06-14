using System;
using System.Collections.Generic;

namespace AsbaBank.Core.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeAttribute : Attribute
    {
        public HashSet<string> Roles { get; private set; }

        public AuthorizeAttribute(string role)
        {
            Roles = new HashSet<string>
            {
                role
            };
        }

        public AuthorizeAttribute(params string[] roles)
        {
            Roles = new HashSet<string>(roles);
        }

        public override string ToString()
        {
            return String.Join(", ", Roles);
        }
    }
}