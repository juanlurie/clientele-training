using System;
using System.Collections.Generic;
using ConsoleHelper.Factory;

namespace ConsoleHelper.Input
{
    public static class BindHelpers
    {
        public static void Input(Action<string[]> onEnterKeyPressedAction, IList<string> commandList)
        {
// ReSharper disable ObjectCreationAsStatement
            new InputRuleFactory(onEnterKeyPressedAction, commandList);
// ReSharper restore ObjectCreationAsStatement
        }
    }
}
