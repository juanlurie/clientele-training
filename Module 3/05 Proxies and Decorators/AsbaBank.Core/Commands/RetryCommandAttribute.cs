using System;
using System.Collections.Generic;

namespace AsbaBank.Core.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RetryCommandAttribute : Attribute
    {
        public int RetryCount { get; set; }
        public int Delay { get; set; }
        public Type[] HaltOnExceptionList { get; set; }
    }
}