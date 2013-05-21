using System;
using ConsoleHelper.Factory;

namespace ConsoleHelper
{
    interface IInputCommand
    {
        InputRuleFactory RuleFactory { get; set; }
        void Execute(ConsoleKeyInfo consoleKey);

        bool IsMatch(ConsoleKeyInfo consoleKeyInfo);
    }
}