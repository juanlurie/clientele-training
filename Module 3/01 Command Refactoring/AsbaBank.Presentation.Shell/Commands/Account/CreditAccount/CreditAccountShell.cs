using System;
using AsbaBank.Presentation.Shell.Interfaces;

namespace AsbaBank.Presentation.Shell.Commands.Account.CreditAccount
{
    public class CreditAccountShell : IShellCommand
    {
        public string Usage { get { return String.Format("{0} <Client Id> <Account Number> <Amount>", Key); } }
        public string Key { get { return "CreditAccount"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            decimal amount;
            int clientId;
            var amountResult = decimal.TryParse(args[2], out amount);
            var clientIdResult = int.TryParse(args[0], out clientId);

            if (!amountResult || !clientIdResult)
            {
                throw new ArgumentException(String.Format("Incorrect usage. Usage is: {0}", Usage));
            }

            var accountNumber = args[1];

            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException(String.Format("Incorrect usage. Usage is: {0}", Usage));
            }

            return new CreditAccount(clientId, accountNumber, amount);
        }
    }
}