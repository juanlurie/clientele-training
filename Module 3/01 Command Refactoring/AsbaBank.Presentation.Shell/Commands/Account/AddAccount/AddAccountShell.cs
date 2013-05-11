using System;
using AsbaBank.Presentation.Shell.Interfaces;

namespace AsbaBank.Presentation.Shell.Commands.Account.AddAccount
{
    public class AddAccountShell : IShellCommand
    {
        public string Usage { get { return String.Format("{0} <Client Id>", Key); } }
        public string Key { get { return "AddAccount"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            int clientId;
            var result = int.TryParse(args[0], out clientId);

            if (!result)
            {
                throw new ArgumentException(String.Format("Incorrect usage. Usage is: {0}", Usage));
            }

            return new AddAccount(clientId);
        }
    }
}