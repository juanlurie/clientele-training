using System;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands.Account.StopBankCard
{
    public class StopBankCard : ICommand
    {
        private readonly int id;

        public StopBankCard(int id)
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
                account.StopBankCard();
                accountRepository.Add(account);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Bank card stopper for {0} -- {1} {2}", account.AccountNumber, client.Name, client.Surname);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
