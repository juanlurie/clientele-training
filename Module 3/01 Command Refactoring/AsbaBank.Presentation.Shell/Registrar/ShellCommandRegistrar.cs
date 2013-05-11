using System.Collections.Generic;
using AsbaBank.Presentation.Shell.Commands.Account.AddAccount;
using AsbaBank.Presentation.Shell.Commands.Account.CreditAccount;
using AsbaBank.Presentation.Shell.Commands.Account.DebitAccount;
using AsbaBank.Presentation.Shell.Commands.Account.GetAccountBalance;
using AsbaBank.Presentation.Shell.Commands.Account.IssueBankCard;
using AsbaBank.Presentation.Shell.Commands.Account.StopBankCard;
using AsbaBank.Presentation.Shell.Commands.Account.CloseAccount;
using AsbaBank.Presentation.Shell.Commands.ClientCommands;
using AsbaBank.Presentation.Shell.Interfaces;

namespace AsbaBank.Presentation.Shell.Registrar
{
    public static class ShellCommandRegistrar
    {
        public static Dictionary<string, IShellCommand>  Commands;
        static ShellCommandRegistrar()
        {
            Commands = new Dictionary<string, IShellCommand>();
            RegisterCommands();
        }

        static void RegisterCommand(IShellCommand command)
        {
            Commands.Add(command.Key, command);
        }

        static void RegisterCommands()
        {
            RegisterCommand(new RegisterClientShell());
            RegisterCommand(new AddAccountShell());
            RegisterCommand(new CloseAccountShell());
            RegisterCommand(new CreditAccountShell());
            RegisterCommand(new DebitAccountShell());
            RegisterCommand(new GetAccountBalanceShell());
            RegisterCommand(new IssueBankCardShell());
            RegisterCommand(new StopBankCardShell());
        }
    }
}
