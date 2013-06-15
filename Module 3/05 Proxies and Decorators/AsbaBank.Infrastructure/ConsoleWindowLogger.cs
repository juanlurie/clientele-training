using System;
using System.Globalization;

using AsbaBank.Core;

namespace AsbaBank.Infrastructure
{
    public class Log4NetLogger : ILog
    {
        public Log4NetLogger()
        {
        }

        public void Verbose(string message, params object[] values)
        {
            
        }

        public void Debug(string message, params object[] values)
        {
            
        }

        public void Info(string message, params object[] values)
        {
            
        }

        public void Warn(string message, params object[] values)
        {
            
        }

        public void Error(string message, params object[] values)
        {
            
        }

        public void Fatal(string message, params object[] values)
        {
            
        }
    }

    public class ConsoleWindowLogger : ILog
    {
        private const string MessageFormat = "{0}";
        private static readonly object Sync = new object();
        private readonly ConsoleColor originalColor = Console.ForegroundColor;

        public virtual void Verbose(string message, params object[] values)
        {
            Log(ConsoleColor.DarkGreen, message, values);
        }

        public virtual void Debug(string message, params object[] values)
        {
            Log(ConsoleColor.Green, message, values);
        }

        public virtual void Info(string message, params object[] values)
        {
            Log(ConsoleColor.White, message, values);
        }

        public virtual void Warn(string message, params object[] values)
        {
            Log(ConsoleColor.Yellow, message, values);
        }

        public virtual void Error(string message, params object[] values)
        {
            Log(ConsoleColor.DarkRed, message, values);
        }

        public virtual void Fatal(string message, params object[] values)
        {
            Log(ConsoleColor.Red, message, values);
        }

        private void Log(ConsoleColor color, string message, params object[] values)
        {
            lock (Sync)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(FormatMessage(message, values));
                Console.ForegroundColor = originalColor;
            }
        }

        public static string FormatMessage(string message, params object[] values)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                MessageFormat,
                string.Format(CultureInfo.InvariantCulture, message, values));
        }
    }
}