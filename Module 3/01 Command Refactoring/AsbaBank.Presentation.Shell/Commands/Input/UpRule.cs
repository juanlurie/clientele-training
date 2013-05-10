using System;

namespace AsbaBank.Presentation.Shell.Commands.Input
{
    public class UpRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public UpRule()
        {
        }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.UpArrow == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            RuleFactory.Line = RuleFactory.MoveToPreviousHistoryItem(RuleFactory.Line);
        }
    }
}