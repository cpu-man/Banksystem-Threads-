using System.Collections.Generic;
using Banksystem.Models;

namespace Banksystem.Services
{
    public class BankService
    {
        private readonly List<Account> _accounts = new()
        {
            new Account { Id = 1, Name = "Lønkonto",  AccountNumber = "1234-567890", Balance =  12500.50m },
            new Account { Id = 2, Name = "Opsparing", AccountNumber = "9876-543210", Balance =   -670.55m },
        };

        public List<Account> GetAllAccounts() => _accounts;
    }
}