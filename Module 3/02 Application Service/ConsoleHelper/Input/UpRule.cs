using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class UpRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.UpArrow == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            RuleFactory.Line = RuleFactory.MoveToPreviousHistoryItem();
        }
    }
}