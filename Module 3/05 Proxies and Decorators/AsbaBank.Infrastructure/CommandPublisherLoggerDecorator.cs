using System;
using System.Collections.Generic;
using System.Linq;
using AsbaBank.Core;
using AsbaBank.Core.Commands;

namespace AsbaBank.Infrastructure
{
    public class CommandPublisherLoggerDecorator : IPublishCommands
    {
        private readonly IPublishCommands commandPublisher;
        private ConsoleWindowLogger logger;

        public CommandPublisherLoggerDecorator(IPublishCommands commandPublisher)
        {
            logger = new ConsoleWindowLogger();
            this.commandPublisher = commandPublisher;
        }

        public void Publish(ICommand command)
        {
            commandPublisher.Publish(command);
        }

        public void Subscribe(object handler)
        {
            commandPublisher.Subscribe(handler);

            foreach (var type in GetCommandHandlerTypes(handler))
            {
                logger.Verbose("Subscribed handler for command {0}", type.GenericTypeArguments.First().Name);
            }
        }

        private static IEnumerable<Type> GetCommandHandlerTypes(object handler)
        {
            return handler.GetType()
                          .GetInterfaces()
                          .Where(x =>
                                 x.IsGenericType &&
                                 x.GetGenericTypeDefinition() == typeof(IHandleCommand<>));
        }
    }
}