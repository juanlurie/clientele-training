using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AsbaBank.Core;
using AsbaBank.Core.Commands;

namespace AsbaBank.Infrastructure.CommandPublishers
{
    public class CommandPublisherRetryDecorator : IPublishCommands
    {
        private readonly IPublishCommands publisher;

        public CommandPublisherRetryDecorator(IPublishCommands publisher)
        {
            this.publisher = publisher;
        }

        public void Publish(ICommand command)
        {
            var retryCommandAttribute = Attribute.GetCustomAttributes(command.GetType())
               .FirstOrDefault(a => a is RetryCommandAttribute) as RetryCommandAttribute;

            if (retryCommandAttribute == null)
            {
                publisher.Publish(command);
                return;
            }

            Retry(command, retryCommandAttribute);
        }

        private void Retry(ICommand command, RetryCommandAttribute retryCommandAttribute)
        {
            int delay = retryCommandAttribute.Delay;
            int retryCount = retryCommandAttribute.RetryCount;
            IList<Type> haltOnExceptionList = retryCommandAttribute.HaltOnExceptionList;

            do
            {
                try
                {
                    publisher.Publish(command);
                    break;
                }
                catch (Exception ex)
                {
                    if (haltOnExceptionList.Any(x => x == ex.GetType()))
                        throw;

                    if (retryCount <= 0)
                        throw;

                    Thread.Sleep(delay);
                }
            } while (retryCount-- > 0);
        }

        public void Subscribe(object handler)
        {
            publisher.Subscribe(handler);
        }
    }
}