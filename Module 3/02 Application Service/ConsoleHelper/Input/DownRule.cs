using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class DownRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.DownArrow == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            RuleFactory.Line = RuleFactory.MoveToNextHistoryItem();
        }
    }
}