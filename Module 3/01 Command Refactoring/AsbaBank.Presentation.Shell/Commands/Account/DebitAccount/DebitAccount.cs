﻿using System;
using AsbaBank.Domain.Models;

namespace AsbaBank.Presentation.Shell.Commands.Account.DebitAccount
{
    public class DebitAccount : ICommand
    {
        private readonly int clientId;
        private readonly int accountNumber;
        private readonly decimal amount;

        public DebitAccount(int clientId, int accountNumber, decimal amount)
        {
            this.clientId = clientId;
            this.accountNumber = accountNumber;
            this.amount = amount;
            if (clientId <= 0)
                throw new ArgumentException("Please provide a valid client id.");

            if (amount <= 0)
                throw new ArgumentException("Please provide an amount larger than 0.");

            if (accountNumber <= 0)
                throw new ArgumentException("Please provide a valid account number.");
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

                var account = Domain.Models.Account.OpenAccount(clientId);
                account.Credit(accountNumber, amount);
                accountRepository.Add(account);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Debited account {0} with {1}", account.AccountNumber, amount);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}


