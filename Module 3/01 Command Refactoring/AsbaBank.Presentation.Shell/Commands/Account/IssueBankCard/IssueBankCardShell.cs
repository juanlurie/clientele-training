using System;

namespace AsbaBank.Presentation.Shell.Commands.Account.IssueBankCard
{
    public class IssueBankCardShell : IShellCommand
    {
        public string Usage { get { return String.Format("{0} <Client Id> <Account Number>", Key); } }
        public string Key { get { return "IssueBankCard"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            int clientId;
            var result = int.TryParse(args[0], out clientId);

            if (!result)
            {
                throw new ArgumentException(String.Format("Incorrect usage. Usage is: {0}", Usage));
            }

            int accountNumber;
            var accountNumberResult = int.TryParse(args[1], out accountNumber);

            if (!accountNumberResult)
            {
                throw new ArgumentException(String.Format("Incorrect usage. Usage is: {0}", Usage));
            }

            return new IssueBankCard(clientId,accountNumber);
        }
    }
}