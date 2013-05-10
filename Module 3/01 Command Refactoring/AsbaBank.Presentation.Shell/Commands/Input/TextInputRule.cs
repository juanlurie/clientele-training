using System;

namespace AsbaBank.Presentation.Shell.Commands.Input
{
    public class TextInputRule : IInputCommand
    {
        private readonly string inputString;

        public TextInputRule()
        {
        }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.Enter != consoleKeyInfo.Key
                         && ConsoleKey.UpArrow != consoleKeyInfo.Key
                         && ConsoleKey.DownArrow != consoleKeyInfo.Key
                         && ConsoleKey.Backspace != consoleKeyInfo.Key
                         && ConsoleKey.Tab != consoleKeyInfo.Key;
            return result;
        }

        public InputRuleFactory RuleFactory { get; set; }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            Console.Write(consoleKey.KeyChar);
            RuleFactory.Line = RuleFactory.Line + consoleKey.KeyChar;
        }
    }
}