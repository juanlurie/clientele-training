using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class LeftRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            return ConsoleKey.LeftArrow == consoleKeyInfo.Key;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            RuleFactory.MoveLeft();
        }
    }
}