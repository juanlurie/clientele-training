using System;
using AsbaBank.Presentation.Shell.Commands.Input;

namespace AsbaBank.Presentation.Shell
{
    internal class Program
    {
        private static void Main()
        {
            InputRuleFactory ruleFactory = new InputRuleFactory();
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;

            PrintHelp();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                var key = Console.ReadKey(true);
                ruleFactory.ConsoleKeyInfo = key;
                ruleFactory.Execute();
            }
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
    }
}