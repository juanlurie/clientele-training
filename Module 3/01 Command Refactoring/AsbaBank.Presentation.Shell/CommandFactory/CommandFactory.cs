using System.Collections.Generic;
using System.Linq;
using AsbaBank.Presentation.Shell.Commands;
using AsbaBank.Presentation.Shell.Commands.Account.AddAccount;
using AsbaBank.Presentation.Shell.Commands.Account.CloseAccount;
using AsbaBank.Presentation.Shell.Commands.Account.CreditAccount;
using AsbaBank.Presentation.Shell.Commands.Account.DebitAccount;
using AsbaBank.Presentation.Shell.Commands.Account.GetAccountBalance;
using AsbaBank.Presentation.Shell.Commands.Account.IssueBankCard;
using AsbaBank.Presentation.Shell.Commands.Account.StopBankCard;

namespace AsbaBank.Presentation.Shell.CommandFactory
{
    public class CommandFactory
    {
        private readonly Dictionary<string, IShellCommand> shellCommands;
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
            RegsiterCommand(new RegisterClientShell());
            RegsiterCommand(new AddAccountShell());
            RegsiterCommand(new CloseAccountShell());
            RegsiterCommand(new CreditAccountShell());
            RegsiterCommand(new DebitAccountShell());
            RegsiterCommand(new GetAccountBalanceShell());
            RegsiterCommand(new IssueBankCardShell());
            RegsiterCommand(new StopBankCardShell());
        }

        private void RegsiterCommand(IShellCommand command)
        {
            shellCommands.Add(command.Key, command);
        }
    }
}