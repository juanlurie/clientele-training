using System;

namespace AsbaBank.Presentation.Shell.Commands.Input
{
    public class BackspaceRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public BackspaceRule()
        {
        }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.Backspace == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            if (!string.IsNullOrEmpty(RuleFactory.Line))
                RuleFactory.Line = RuleFactory.Line.Remove(RuleFactory.Line.Length - 1, 1);

            RuleFactory.Backspace();
        }
    }
}