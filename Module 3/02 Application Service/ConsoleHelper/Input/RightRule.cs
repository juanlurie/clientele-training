using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class RightRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.RightArrow == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            RuleFactory.MoveRight();
        }
    }
}