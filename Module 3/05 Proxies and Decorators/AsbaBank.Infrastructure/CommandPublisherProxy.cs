using System;
using System.Linq;
using AsbaBank.Core.Commands;

namespace AsbaBank.Infrastructure
{
    public class CommandPublisherProxy : LocalCommandPublisher
    {
        private LocalCommandPublisher commandPublisher;

        public CommandPublisherProxy()
        {
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
               .FirstOrDefault(a => a is AuthorizeAttribute) as AuthorizeAttribute;

            if (authorizeAttribute == null || authorizeAttribute.Roles.Any())
            {
                return;
            }

            //Logger.Error("User {0} attempted to execute command {1} which requires roles {2}",
            //             currentUser, command.GetType().Name, authorizeAttribute);

            throw new Exception("You are not authorized to execute this command.");
        }
    }
}