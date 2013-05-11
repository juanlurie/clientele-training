using System;
using System.Linq;
using ConsoleHelper.Input;

namespace AsbaBank.Presentation.Shell
{
    internal class Program
    {
        private static void Main()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;

            PrintHelp();

            Action<string[]> executeDelegate = TryHandleRequest;
            var commandList = Environment.CommandFactory.GetShellCommands().Select(item => item.Key).ToList();
            new InputRuleFactory(executeDelegate, commandList);

        }

        private static void PrintHelp()
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Available commands:");
            Console.WriteLine("");
            var index = 1;

            foreach (var shellCommand in Environment.CommandFactory.GetShellCommands())
            {
                Console.WriteLine(index + ". " + shellCommand.Usage);
                index++;
            }

            Console.ForegroundColor = originalColor;
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void TryHandleRequest(string[] split)
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

        public static void HandleRequest(string[] split)
        {
            Environment.ExecuteCommand(split);
        }
    }


}