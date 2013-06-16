using System;
using System.Collections.Generic;
using System.Linq;
using AsbaBank.Core;
using AsbaBank.Core.Commands;

namespace AsbaBank.Infrastructure.CommandPublishers
{
    public class CommandPublisherLoggerDecorator : IPublishCommands
    {
        private readonly IPublishCommands commandPublisher;
        private readonly ILog logger;

        public CommandPublisherLoggerDecorator(IPublishCommands commandPublisher, ILog logger)
        {
            this.commandPublisher = commandPublisher;
            this.logger = logger;
        }

        public void Publish(ICommand command)
        {
            try
            {
                commandPublisher.Publish(command);
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void Subscribe(object handler)
        {
            try
            {
                commandPublisher.Subscribe(handler);

                foreach (var type in GetCommandHandlerTypes(handler))
                {
                    logger.Verbose("Subscribed handler for command {0}", type.GenericTypeArguments.First().Name);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
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