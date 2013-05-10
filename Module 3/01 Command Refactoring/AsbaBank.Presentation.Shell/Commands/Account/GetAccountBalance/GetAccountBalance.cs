using System;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands.Account.GetAccountBalance
{
    public class GetAccountBalance : ICommand
    {
        private readonly int id;

        public GetAccountBalance(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Please provide a valid client id.");

            this.id = id;
        }

        public void Execute()
        {
            var unitOfWork = Environment.GetUnitOfWork();
            var clientRepository = unitOfWork.GetRepository<Client>();

            try
            {
                var client = clientRepository.Get(id);
                if (client == null)
                    throw new ArgumentException("Client Id does not exist. Please select another Client Id or register new client");

                var account = Domain.Models.Account.OpenAccount(id);
                var amount = account.GetAccountBalance();

                Environment.Logger.Verbose("Balance for {0} is {1}", account.AccountNumber, amount);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
