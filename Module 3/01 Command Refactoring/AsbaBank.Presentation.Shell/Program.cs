using System;
using System.Collections.Generic;
using System.Linq;

namespace AsbaBank.Presentation.Shell
{
    internal class Program
    {
        private static List<string> history;

        private static int historyIndex;

        private static void Main()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;

            history = new List<string>();
            historyIndex = 0;

            PrintHelp();

            string line = "";
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
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
                    string[] split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    AddHistoryItem(split);

                    TryHandleRequest(split);

                    line = "";
                }
                else if (myKey.Key == ConsoleKey.Tab)
                {
                    line = AutoCompleteLine(line);
                }
                else if (myKey.Key == ConsoleKey.UpArrow)
                {
                    line = MoveToPreviousHistoryItem(line);
                }
                else if (myKey.Key == ConsoleKey.DownArrow)
                {
                    line = MoveToNextHistoryItem(line);
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
                            line = line.Remove(line.Length - 1, 1);

                        Backspace();
                    }
                }
            }
        }

        private static string MoveToNextHistoryItem(string line)
        {
            if (history.Count > historyIndex)
            {
                ClearCurrentConsoleLine();
                historyIndex++;
                string command = history[historyIndex - 1];
                line = command;
                Console.Write(command);
            }
            return line;
        }

        private static string MoveToPreviousHistoryItem(string line)
        {
            if (historyIndex > 0)
            {
                ClearCurrentConsoleLine();
                historyIndex--;
                string command = history[historyIndex];
                line = command;
                Console.Write(command);
            }
            return line;
        }

        private static string AutoCompleteLine(string line)
        {
            IShellCommand command = Environment.CommandFactory.GetShellCommands().FirstOrDefault(x => line != null && x.Key.ToLower().Contains(line.ToLower()));
            if (command != null)
            {
                line = command.Key + " ";
                ClearCurrentConsoleLine();
                Console.Write(command.Key + " ");
            }
            return line;
        }

        private static void AddHistoryItem(IEnumerable<string> split)
        {
            var historyItem = split.Aggregate("", (current, item) => current + item + " ");
            historyItem = historyItem.Remove(historyItem.Length - 1);
            if (history.All(x => x != historyItem))
            {
                history.Add(historyItem);
            }
            historyIndex = history.Count;
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