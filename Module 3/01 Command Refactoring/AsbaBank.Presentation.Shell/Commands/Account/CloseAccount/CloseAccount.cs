using System;
using AsbaBank.Domain.Models;
using AsbaBank.Presentation.Shell.Interfaces;

namespace AsbaBank.Presentation.Shell.Commands.Account.CloseAccount
{
    public class CloseAccount : ICommand
    {
        private readonly int id;
        private readonly int accountNumber;

        public CloseAccount(int id,int accountNumber)
        {
            if (id <= 0)
                throw new ArgumentException("Please provide a valid client id.");
            if (accountNumber <= 0)
                throw new ArgumentException("Please provide a valid account number.");

            this.id = id;
            this.accountNumber = accountNumber;
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

                var account = accountRepository.Get(id);
                account.Close(accountNumber);
                accountRepository.Add(account);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Closed account {0} for {1} {2}", account.AccountNumber, client.Name, client.Surname);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
