using AsbaBank.Core;
using AsbaBank.Core.Commands;

namespace AsbaBank.Infrastructure
{
    public class CommandPublisherLoggerDecorator : IPublishCommands
    {
        private readonly IPublishCommands commandPublisher;

        public CommandPublisherLoggerDecorator(IPublishCommands commandPublisher)
        {
            this.commandPublisher = commandPublisher;
        }

        public void Publish(ICommand command)
        {
            commandPublisher.Publish(command);
        }
    }
}