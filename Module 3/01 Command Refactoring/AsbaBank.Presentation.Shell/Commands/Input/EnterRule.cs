using System;

namespace AsbaBank.Presentation.Shell.Commands.Input
{
    public class EnterInputRule : IInputCommand
    {
        public InputRuleFactory RuleFactory { get; set; }

        public EnterInputRule()
        {
        }

        public bool IsMatch(ConsoleKeyInfo consoleKeyInfo)
        {
            var result = ConsoleKey.Enter == consoleKeyInfo.Key;
            return result;
        }

        public void Execute(ConsoleKeyInfo consoleKey)
        {
            if (string.IsNullOrWhiteSpace(RuleFactory.Line))
            {
                return;
            }
            //if (inputString.ToLower() == "exit")
            //    return "Exit";

            string[] split = RuleFactory.Line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            RuleFactory.AddHistoryItem(split);

            RuleFactory.TryHandleRequest(split);

            RuleFactory.Line = "";
        }
    }
}