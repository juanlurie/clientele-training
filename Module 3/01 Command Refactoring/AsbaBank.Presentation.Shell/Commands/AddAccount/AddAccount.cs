using System;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands.AddAccount
{
    public class AddAccountByClientId : ICommand
    {
        private readonly int id;

        public AddAccountByClientId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Please provide a valid client id.");
            }
            this.id = id;
        }

        public void Execute()
        {
            var unitOfWork = Environment.GetUnitOfWork();
            var accountRepository = unitOfWork.GetRepository<Account>();
            var clientRepository = unitOfWork.GetRepository<Client>();

            try
            {
                var client = clientRepository.Get(id);
                if (client == null)
                {
                    throw new ArgumentException("Please provide a valid client id.");
                }

                var account = Account.OpenAccount(id);
                accountRepository.Add(account);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Registered account {0} for {1} {2}",account.AccountNumber, client.Name, client.Surname);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
