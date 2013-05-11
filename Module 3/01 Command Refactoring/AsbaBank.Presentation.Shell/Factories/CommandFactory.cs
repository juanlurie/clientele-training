using System.Collections.Generic;
using System.Linq;
using AsbaBank.Presentation.Shell.Interfaces;
using AsbaBank.Presentation.Shell.Registrar;

namespace AsbaBank.Presentation.Shell.Factories
{
    public class CommandFactory
    {
        private Dictionary<string, IShellCommand> shellCommands;
        public CommandFactory()
        {
            shellCommands = new Dictionary<string, IShellCommand>();
            RegisterCommands();
        }

        public void ExecuteCommand(string[] split)
        {
            IShellCommand shellCommand = GetShellCommand(split.First());
            ICommand command = shellCommand.Build(split.Skip(1).ToArray());

            command.Execute();
        }

        public IEnumerable<IShellCommand> GetShellCommands()
        {
            return shellCommands.Values;
        }

        public IShellCommand GetShellCommand(string key)
        {
            return shellCommands[key];
        }

        private void RegisterCommands()
        {
            shellCommands = ShellCommandRegistrar.Commands;
        }
    }
}