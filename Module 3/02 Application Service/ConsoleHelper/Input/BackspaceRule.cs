using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class BackspaceRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.Backspace == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            var consoleX = Console.CursorLeft;

            if (!string.IsNullOrEmpty(RuleFactory.Line) && consoleX > 0)
            {
                RuleFactory.ClearCurrentConsoleLine();
                RuleFactory.Line = RuleFactory.Line.Remove(consoleX - 1, 1);
                Console.Write(RuleFactory.Line);
                Console.CursorLeft = consoleX - 1;
            }
        }
    }
}