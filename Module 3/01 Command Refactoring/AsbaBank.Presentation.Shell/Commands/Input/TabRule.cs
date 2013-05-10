using System;

namespace AsbaBank.Presentation.Shell.Commands.Input
{
    public class TabInputRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public TabInputRule()
        {
        }

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