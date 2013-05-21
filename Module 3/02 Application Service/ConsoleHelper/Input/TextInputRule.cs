using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class TextInputRule : IInputCommand
    {
        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.Enter != consoleKeyInfo.Key
                         && ConsoleKey.UpArrow != consoleKeyInfo.Key
                         && ConsoleKey.DownArrow != consoleKeyInfo.Key
                         && ConsoleKey.Backspace != consoleKeyInfo.Key
                         && ConsoleKey.Tab != consoleKeyInfo.Key
                         && ConsoleKey.LeftArrow != consoleKeyInfo.Key
                         && ConsoleKey.RightArrow != consoleKeyInfo.Key
                         && ConsoleKey.Delete != consoleKeyInfo.Key;
            return result;
        }

        public InputRuleFactory RuleFactory { get; set; }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            var consoleX = Console.CursorLeft;
            if (!string.IsNullOrEmpty(RuleFactory.Line) && consoleX < RuleFactory.Line.Length)
            {
                RuleFactory.ClearCurrentConsoleLine();
                RuleFactory.Line = RuleFactory.Line.Insert(consoleX, consoleKey.KeyChar.ToString());
                Console.Write(RuleFactory.Line);
                Console.CursorLeft = consoleX + 1;
            }
            else
            {
                Console.Write(consoleKey.KeyChar);
                RuleFactory.Line = RuleFactory.Line + consoleKey.KeyChar;
            }
        }
    }
}