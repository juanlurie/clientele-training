using System;
using System.Linq;
using AsbaBank.Core;
using AsbaBank.Core.Commands;
using AsbaBank.Infrastructure.CommandPublishers;

namespace AsbaBank.Infrastructure.CommandProxy
{
    public class CommandPublisherProxy : LocalCommandPublisher
    {
        private readonly ICurrentUserSession currentUser;
        private readonly LocalCommandPublisher commandPublisher;

        public CommandPublisherProxy(ICurrentUserSession currentUser)
        {
            this.currentUser = currentUser;
            commandPublisher = new LocalCommandPublisher();
        }

        public override void Subscribe(object handler)
        {
            commandPublisher.Subscribe(handler);
        }

        public override void Publish(ICommand command)
        {
            AuthorizeCommand(command);
            commandPublisher.Publish(command);
        }

        private void AuthorizeCommand(ICommand command)
        {
            var authorizeAttribute = Attribute.GetCustomAttributes(command.GetType())
               .FirstOrDefault(x => x is AuthorizeAttribute) as AuthorizeAttribute;

            if (authorizeAttribute == null || currentUser.IsInRole(authorizeAttribute.Roles))
            {
                return;
            }

            string message = String.Format("User {0} attempted to execute command {1} which requires roles {2}", currentUser, command.GetType().Name, authorizeAttribute);

            throw new UnauthorizedAccessException(message);
        }
    }
}