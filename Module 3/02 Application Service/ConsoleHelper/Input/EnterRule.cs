using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    class EnterInputRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.Enter == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            if (string.IsNullOrWhiteSpace(RuleFactory.Line))
                return;

            if(RuleFactory.Line.ToLower() == "exit")
                Environment.Exit(0);

            string[] split = RuleFactory.Line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            RuleFactory.AddHistoryItem(split);
            Console.WriteLine();
            RuleFactory.EnterKeyAction(split);

            RuleFactory.Line = "";
        }
    }
}