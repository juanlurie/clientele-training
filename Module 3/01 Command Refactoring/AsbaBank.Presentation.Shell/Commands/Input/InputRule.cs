using System;
using System.Collections.Generic;
using System.Linq;

namespace AsbaBank.Presentation.Shell.Commands.Input
{
    public class InputRuleFactory
    {
        public IList<IInputCommand> Rules;

        public ConsoleKey Key { get; set; }
        public ConsoleKeyInfo ConsoleKeyInfo { get; set; }

        public InputRuleFactory()
        {
            History = new List<string>();
            HistoryIndex = 0;

            Rules = new List<IInputCommand>
            {
                new EnterInputRule{RuleFactory = this}
                ,new TextInputRule{RuleFactory = this}
                , new TabInputRule{RuleFactory = this}
                , new BackspaceRule {RuleFactory = this}
                , new UpRule {RuleFactory = this}
                , new DownRule {RuleFactory = this}
            };
        }

        public List<string> History { get; set; }

        public int HistoryIndex { get; set; }

        public string Line { get; set; }

        public IInputCommand GetRule()
        {
            var rule = Rules.FirstOrDefault(x => x.IsMatch(ConsoleKeyInfo));
            return rule;
        }

        public string Execute()
        {
            var rule = GetRule();
            if (rule == null)
                return "";
            rule.Execute(ConsoleKeyInfo);
            return "";
        }

        public string MoveToNextHistoryItem(string line)
        {
            if (History.Count > HistoryIndex)
            {
                ClearCurrentConsoleLine();
                HistoryIndex++;
                string command = History[HistoryIndex - 1];
                line = command;
                Console.Write(command);
            }
            return line;
        }

        public string MoveToPreviousHistoryItem(string line)
        {
            if (HistoryIndex > 0)
            {
                ClearCurrentConsoleLine();
                HistoryIndex--;
                string command = History[HistoryIndex];
                line = command;
                Console.Write(command);
            }
            return line;
        }

        public string AutoCompleteLine(string line)
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

        public void AddHistoryItem(IEnumerable<string> split)
        {
            var historyItem = split.Aggregate("", (current, item) => current + item + " ");
            historyItem = historyItem.Remove(historyItem.Length - 1);
            if (History.All(x => x != historyItem))
            {
                History.Add(historyItem);
            }
            HistoryIndex = History.Count;
        }

        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void Backspace()
        {
            int cursorX = Console.CursorLeft;
            if (cursorX > 0)
            {
                Console.CursorLeft--;
                Console.Write(" ");
                Console.CursorLeft--;
            }
        }

        public void TryHandleRequest(string[] split)
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