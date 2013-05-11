using System;
using ConsoleHelper.Input;

namespace AsbaBank.Presentation.Shell
{
    public interface IInputCommand
    {
        InputRuleFactory RuleFactory { get; set; }
        void Execute(ConsoleKeyInfo consoleKey);

        bool IsMatch(ConsoleKeyInfo consoleKeyInfo);
    }
}