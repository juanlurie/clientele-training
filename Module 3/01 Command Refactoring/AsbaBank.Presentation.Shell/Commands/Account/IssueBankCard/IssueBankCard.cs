using System;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands.Account.IssueBankCard
{
    public class IssueBankCard : ICommand
    {
        private readonly int id;
        private readonly int accountNumber;

        public IssueBankCard(int id,int accountNumber)
        {
            if (id <= 0)
                throw new ArgumentException("Please provide a valid client id.");

            if (id <= accountNumber)
                throw new ArgumentException("Please provide a valid client id.");

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
                account.IssueBankCard(accountNumber);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Bank card issued for {0} -- {1} {2}", account.AccountNumber, client.Name, client.Surname);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
