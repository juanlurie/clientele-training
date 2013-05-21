using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class DeleteRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.Delete == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            RuleFactory.Line = RuleFactory.DeleteKey(RuleFactory.Line);
            
        }
    }
}