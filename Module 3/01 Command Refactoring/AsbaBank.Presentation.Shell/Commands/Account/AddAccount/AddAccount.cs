using System;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands.Account.AddAccount
{
    public class AddAccount : ICommand
    {
        private readonly int id;

        public AddAccount(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Please provide a valid client id.");

            this.id = id;
        }

        public void Execute()
        {
            var unitOfWork = Environment.GetUnitOfWork();
            var accountRepository = unitOfWork.GetRepository<Domain.Models.Account>();
            var clientRepository = unitOfWork.GetRepository<Client>();

            try
            {
                var client = clientRepository.Get(id);
                if (client == null)
                    throw new ArgumentException("Client Id does not exist. Please select another Client Id or register new client");

                var account = Domain.Models.Account.OpenAccount(id);
                accountRepository.Add(account);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Registered account {0} for {1} {2}", account.AccountNumber, client.Name, client.Surname);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
