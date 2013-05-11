using System;
using AsbaBank.Presentation.Shell.Interfaces;

namespace AsbaBank.Presentation.Shell.Commands.Account.GetAccountBalance
{
    public class GetAccountBalanceShell : IShellCommand
    {
        public string Usage { get { return String.Format("{0} <Client Id>", Key); } }
        public string Key { get { return "GetAccountBalance"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            int clientId;
            var clientIdResult = int.TryParse(args[0], out clientId);

            if (!clientIdResult)
            {
                throw new ArgumentException(String.Format("Incorrect usage. Usage is: {0}", Usage));
            }

            return new GetAccountBalance(clientId);
        }
    }
}