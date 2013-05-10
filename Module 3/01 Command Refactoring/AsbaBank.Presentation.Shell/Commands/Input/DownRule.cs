using System;

namespace AsbaBank.Presentation.Shell.Commands.Input
{
    public class DownRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public DownRule()
        {
        }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.DownArrow == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            RuleFactory.Line = RuleFactory.MoveToNextHistoryItem(RuleFactory.Line);
        }
    }
}