using System;
using AsbaBank.Domain.Models;
using AsbaBank.Presentation.Shell.Interfaces;

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
            var accountRepository = unitOfWork.GetRepository<Domain.Models.Account>();

            try
            {
                var account = accountRepository.Get(id);
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
