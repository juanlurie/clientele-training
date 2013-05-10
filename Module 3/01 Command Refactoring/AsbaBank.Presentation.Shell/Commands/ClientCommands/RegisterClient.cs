﻿using System;
using AsbaBank.Domain;

namespace AsbaBank.Presentation.Shell.Commands.ClientCommands
{
    public class RegisterClient : ICommand
    {
        public string ClientName { get; private set; }
        public string ClientSurname { get; private set; }
        public string PhoneNumber { get; private set; }

        public RegisterClient(string clientName, string clientSurname, string phoneNumber)
        {
            if (String.IsNullOrEmpty(clientName) || clientName.Length < 3)
            {
                throw new ArgumentException("Please provide a valid client name of at least three characters.");
            }

            if (String.IsNullOrEmpty(clientSurname) || clientSurname.Length < 3)
            {
                throw new ArgumentException("Please provide a valid client name of at least three characters.");
            }

            if (String.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 10 || !phoneNumber.IsDigitsOnly())
            {
                throw new ArgumentException("Please provide a valid phone number that is 10 digits long.");
            }

            ClientName = clientName;
            ClientSurname = clientSurname;
            PhoneNumber = phoneNumber;
        }

        public void Execute()
        {
            //getting the unit of work like this is not ideal. Think of ways that we could solve this.
            var unitOfWork = Environment.GetUnitOfWork(); 
            var clientRepository = unitOfWork.GetRepository<Domain.Models.Client>();

            try
            {
                var client = new Domain.Models.Client(ClientName, ClientSurname, PhoneNumber);
                clientRepository.Add(client);
                unitOfWork.Commit();

                Environment.Logger.Verbose("Registered client {0} {1} with Id {2}", client.Name, client.Surname, client.Id);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
