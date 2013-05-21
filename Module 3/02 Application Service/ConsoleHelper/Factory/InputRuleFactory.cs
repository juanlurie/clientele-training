using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleHelper.Input;

namespace ConsoleHelper.Factory
{
    sealed class InputRuleFactory
    {
        internal readonly IList<string> CommandList;
        internal readonly Action<string[]> EnterKeyAction;
        internal readonly IList<IInputCommand> Rules;

        public InputRuleFactory(Action<string[]> enterKeyAction, IList<string> commandList)
        {
            EnterKeyAction = enterKeyAction;
            CommandList = commandList;
            History = new List<string> { "" };
            HistoryIndex = 0;

            Rules = new List<IInputCommand>
                {
                    new EnterInputRule {RuleFactory = this}
                    ,
                    new TextInputRule {RuleFactory = this}
                    ,
                    new TabInputRule {RuleFactory = this}
                    ,
                    new BackspaceRule {RuleFactory = this}
                    ,
                    new UpRule {RuleFactory = this}
                    ,
                    new DownRule {RuleFactory = this}
                    ,
                    new LeftRule {RuleFactory = this}
                    ,
                    new RightRule {RuleFactory = this}
                    ,
                    new DeleteRule {RuleFactory = this}
                };

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                ConsoleKeyInfo key = Console.ReadKey(true);
                ConsoleKeyInfo = key;
                Execute();
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns

        internal ConsoleKeyInfo ConsoleKeyInfo { get; set; }

        internal List<string> History { get; set; }

        internal int HistoryIndex { get; set; }

        internal string Line { get; set; }

        internal IInputCommand GetRule()
        {
            IInputCommand rule = Rules.FirstOrDefault(x => x.IsMatch(ConsoleKeyInfo));
            return rule;
        }

        internal string Execute()
        {
            IInputCommand rule = GetRule();
            if (rule == null)
                return "";
            rule.Execute(ConsoleKeyInfo);
            return "";
        }

        internal string MoveToNextHistoryItem()
        {
            string line;
            if (History.Count > HistoryIndex + 1)
            {
                ClearCurrentConsoleLine();
                HistoryIndex++;
                string command = History[HistoryIndex];
                line = command;
                Console.Write(command);
            }
            else
            {
                HistoryIndex = 0;
                ClearCurrentConsoleLine();
                string command = History[HistoryIndex];
                line = command;
                Console.Write(command);
            }
            return line;
        }

        internal string MoveToPreviousHistoryItem()
        {
            string line;
            if (HistoryIndex > 0)
            {
                ClearCurrentConsoleLine();
                HistoryIndex--;
                string command = History[HistoryIndex];
                line = command;
                Console.Write(command);
            }
            else
            {
                HistoryIndex = History.Count - 1;
                ClearCurrentConsoleLine();
                string command = History[HistoryIndex];
                line = command;
                Console.Write(command);
            }
            return line;
        }

        internal string DeleteKey(string line)
        {
            int cursorX = Console.CursorLeft;
            if (cursorX < line.Length)
            {
                ClearCurrentConsoleLine();
                line = line.Remove(cursorX, 1);
                Console.Write(line);
                Console.CursorLeft = cursorX;
            }
            return line;
        }

        internal string AutoCompleteLine(string line)
        {
            var commands = CommandList.Where(x => line != null && x.ToLower().StartsWith(line.ToLower())).ToList();
            if (commands.Count() > 1)
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                ClearCurrentConsoleLine();
                Console.WriteLine("Available Commands :");
                Console.WriteLine(commands.Aggregate((x, y) => x + "   " + y));
                Console.ForegroundColor = originalColor;
                Console.Write(line);
                return line;
            }
            var command = commands.FirstOrDefault();

            if (command == null)
                return line;

            line = command + " ";
            ClearCurrentConsoleLine();
            Console.Write(command + " ");

            return line;
        }

        internal void AddHistoryItem(IEnumerable<string> split)
        {
            string historyItem = split.Aggregate("", (current, item) => current + item + " ");
            historyItem = historyItem.Remove(historyItem.Length - 1);
            if (History.All(x => x != historyItem))
            {
                History.Add(historyItem);
            }
            HistoryIndex = History.Count;
        }

        internal void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, currentLineCursor);
        }

        internal void Backspace()
        {
            int cursorX = Console.CursorLeft;
            if (cursorX > 0)
            {
                MoveLeft();
                Console.Write(" ");
                MoveLeft();
            }
        }
        internal void MoveRight()
        {
            int cursorX = Console.CursorLeft;
            if (cursorX < Line.Length)
            {
                Console.CursorLeft++;
            }
        }

        internal void MoveLeft()
        {
            int cursorX = Console.CursorLeft;
            if (cursorX > 0)
            {
                Console.CursorLeft--;
            }
        }
    }
}