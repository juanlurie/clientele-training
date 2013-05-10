using System;
using System.Collections;
using System.Linq;

namespace AsbaBank.Presentation.Shell
{
    class Program
    {
        static void Main()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;

            Stack history = new Stack();

            PrintHelp();

            string line = "";
            while (true)
            {
                ConsoleKeyInfo myKey = Console.ReadKey(true);

                if (myKey.Key == ConsoleKey.Enter)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    Console.WriteLine();
                    var split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    history.Push(split);
                    TryHandleRequest(split);

                    line = "";
                }
                else if (myKey.Key == ConsoleKey.Tab)
                {
                    var command = Environment.GetShellCommands().FirstOrDefault(x => x.Key.ToLower().Contains(line.ToLower()));
                    if (command != null)
                    {
                        line = command.Key + " ";
                        ClearCurrentConsoleLine();
                        Console.Write(command.Key + " ");
                    }
                }
                else if (myKey.Key == ConsoleKey.UpArrow)
                {
                    string[] historyItem = (string[])history.Pop();
                    var command = historyItem.Aggregate("", (current, item) => current + item + " ");
                    line = command;
                    Console.Write(command);
                }
                else
                {
                    if (myKey.Key != ConsoleKey.Backspace)
                    {
                        Console.Write(myKey.KeyChar);
                        line = line + myKey.KeyChar;
                    }
                    else
                    {
                        if (line.Length > 0)
                            line = line.Remove(line.Length - 1, 1);
                        Backspace();
                    }
                }
            }
        }

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private static void Backspace()
        {
            int cursorX = Console.CursorLeft;
            if (cursorX > 0)
            {
                Console.CursorLeft--;
                Console.Write(" ");
                Console.CursorLeft--;
            }
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
            IShellCommand shellCommand = Environment.GetShellCommand(split.First());
            ICommand command = shellCommand.Build(split.Skip(1).ToArray());

            command.Execute();
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

            Console.ForegroundColor = originalColor;
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
