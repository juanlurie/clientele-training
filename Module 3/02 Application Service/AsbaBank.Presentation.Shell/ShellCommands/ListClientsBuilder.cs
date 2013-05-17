using System;
using AsbaBank.ApplicationService.Commands;
using AsbaBank.Core.Commands;

namespace AsbaBank.Presentation.Shell.ShellCommands
{
    public class ListClientsBuilder : ICommandBuilder
    {
        public string Usage { get { return Key; } }
        public string Key { get { return "ListClients"; } }

        public ICommand Build(string[] args)
        {
            if (args.Length != 0)
            {
                throw new ArgumentException(String.Format("Incorrect number of parameters. Usage is: {0}", Usage));
            }

            return new ListClients();
        }
    }
}