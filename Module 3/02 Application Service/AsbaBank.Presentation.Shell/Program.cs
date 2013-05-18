using System;
using System.Linq;
using AsbaBank.Core;
using AsbaBank.Core.Commands;
using ConsoleHelper.Input;

namespace AsbaBank.Presentation.Shell
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(Environment.GetDataStoreType());
            Console.WriteLine();

            PrintHelp();

            Action<string[]> executeDelegate = TryHandleRequest;

            var commandList = Environment.GetShellCommands().Select(item => item.Key).ToList();
            commandList.AddRange(Environment.GetSystemCommands().Select(item => item.Key).ToList());

            BindHelpers.Input(executeDelegate, commandList);
        }

        private static void TryHandleRequest(string[] split)
        {
            try
            {
                HandleRequest(split);
            }
            catch (Exception ex)
            {
                Environment.Logger.Fatal(ex.Message);
            }
        }

        private static void HandleRequest(string[] split)
        {
            if (Environment.IsSystemCommand(split.First()))
            {
                var command = Environment.GetSystemCommand(split.First());
                command.Execute(split.Skip(1).ToArray());
            }
            else
            {
                ICommandBuilder commandBuilder = Environment.GetShellCommand(split.First());
                ICommand command = commandBuilder.Build(split.Skip(1).ToArray());

                Environment.GetScriptRecorder().AddCommand(command);

                IPublishCommands commandPublisher = Environment.GetCommandPublisher();
                commandPublisher.Publish(command);
            }
        }

        private static void PrintHelp()
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
           
            Console.WriteLine("Available commands:");

            foreach (var shellCommand in Environment.GetShellCommands())
            {
                Console.WriteLine(shellCommand.Usage);
            }

            Console.WriteLine();
            Console.WriteLine("System commands:");

            foreach (var systemCommand in Environment.GetSystemCommands())
            {
                Console.WriteLine(systemCommand.Usage);
            }

            Console.ForegroundColor = originalColor;
            Console.WriteLine();
            Console.WriteLine();
        }
    }    
}
