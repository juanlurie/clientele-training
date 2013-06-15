using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AsbaBank.Core;
using AsbaBank.Core.Commands;

namespace AsbaBank.Infrastructure
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

            IList<Type> haltOnExceptionList = new List<Type>();
            int delay = 0;
            int retryCount = 0;
            if (retryCommandAttribute != null)
            {
                delay = retryCommandAttribute.Delay;
                retryCount = retryCommandAttribute.RetryCount;
                haltOnExceptionList = retryCommandAttribute.HaltOnExceptionList;
            }

            int count = 0;
            do
            {
                try
                {
                    publisher.Publish(command);
                    break;
                }
                catch(Exception ex)
                {
                    if (haltOnExceptionList.Any(x=>x ==  ex.GetType()))
                        break;
                    count++;
                    if (count > retryCount)
                        throw;
                    Thread.Sleep(delay);
                }
            } while (count < retryCount);
        }

        public void Subscribe(object handler)
        {
            publisher.Subscribe(handler);
        }
    }
}