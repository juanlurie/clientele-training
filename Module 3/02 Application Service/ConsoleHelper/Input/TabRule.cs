using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class TabInputRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.Tab == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            RuleFactory.Line = RuleFactory.AutoCompleteLine(RuleFactory.Line);
        }
    }
}