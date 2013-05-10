using System;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands.Account.CreditAccount
{
    public class CreditAccount : ICommand
    {
        private readonly int clientId;
        private readonly string accountNumber;
        private readonly decimal amount;

        public CreditAccount(int clientId,string accountNumber, decimal amount)
        {
            this.clientId = clientId;
            this.accountNumber = accountNumber;
            this.amount = amount;
            if (clientId <= 0)
                throw new ArgumentException("Please provide a valid client id.");

            if (amount <= 0)
                throw new ArgumentException("Please provide an amount larger than 0.");

        }

        public void Execute()
        {
            var unitOfWork = Environment.GetUnitOfWork();
            var accountRepository = unitOfWork.GetRepository<Domain.Models.Account>();
            var clientRepository = unitOfWork.GetRepository<Client>();

            try
            {
                var client = clientRepository.Get(clientId);
                if (client == null)
                    throw new ArgumentException("Client Id does not exist. Please select another Client Id or register new client");

                var account = accountRepository.Get(clientId);
                account.Credit(1, amount);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Credited account {0} with {1}", account.AccountNumber, amount);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
