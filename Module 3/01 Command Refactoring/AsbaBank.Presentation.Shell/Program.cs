using System;
using System.Collections;
using System.Linq;

namespace AsbaBank.Presentation.Shell
{
    internal class Program
    {
        private static void Main()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;

            var history = new Stack();

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
                    if (line.ToLower() == "exit")
                        return;

                    Console.WriteLine();
                    string[] split = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    history.Push(split);
                    TryHandleRequest(split);

                    line = "";
                }
                else if (myKey.Key == ConsoleKey.Tab)
                {
                    IShellCommand command = Environment.CommandFactory.GetShellCommands().FirstOrDefault(x => line != null && x.Key.ToLower().Contains(line.ToLower()));
                    if (command != null)
                    {
                        line = command.Key + " ";
                        ClearCurrentConsoleLine();
                        Console.Write(command.Key + " ");
                    }
                }
                else if (myKey.Key == ConsoleKey.UpArrow)
                {
                    var historyItem = (string[])history.Pop();
                    string command = historyItem.Aggregate("", (current, item) => current + item + " ");
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
                        if (!string.IsNullOrEmpty(line))
                        {
                            line = line.Remove(line.Length - 1, 1);
                        }
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
            {
                Console.Write(" ");
            }
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
            Environment.ExecuteCommand(split);
        }

        private static void PrintHelp()
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Available commands:");

            foreach (IShellCommand shellCommand in Environment.CommandFactory.GetShellCommands())
            {
                Console.WriteLine(shellCommand.Usage);
            }

            Console.ForegroundColor = originalColor;
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}