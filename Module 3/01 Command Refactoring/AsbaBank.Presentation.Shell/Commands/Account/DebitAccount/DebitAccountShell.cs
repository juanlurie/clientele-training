using System;

namespace AsbaBank.Presentation.Shell.Commands.Account.DebitAccount
{
    public class DebitAccountShell : IShellCommand
    {
        public string Usage { get { return String.Format("{0} <Client Id> <Amount>", Key); } }
        public string Key { get { return "DebitAccount"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            decimal amount;
            int clientId;
            var amountResult = decimal.TryParse(args[1], out amount);
            var clientIdResult = int.TryParse(args[0], out clientId);

            if (!amountResult || !clientIdResult)
            {
                throw new ArgumentException(String.Format("Incorrect usage. Usage is: {0}", Usage));
            }

            return new DebitAccount(clientId,  amount);
        }
    }
}