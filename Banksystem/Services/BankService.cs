using Banksystem.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Banksystem.Services
{
    public class BankService
    {
        private static BankService? _instance;
        public static BankService Instance => _instance ??= new BankService();

        private readonly object _lock = new object();

        public ObservableCollection<Account> Accounts { get; } = new();

        public BankService()
        {
            Accounts.Add(new Account { Id = 1, Name = "Lønkonto", AccountNumber = "1234-567890", Balance = 12500.50m });
            Accounts.Add(new Account { Id = 2, Name = "Opsparing", AccountNumber = "9876-543210", Balance = 5000.00m });
        }

        public ObservableCollection<Account> GetAllAccounts() => Accounts;

        public bool Transfer(Account from, Account to, decimal amount, string description)
        {
            Monitor.Enter(_lock);
            try
            {
                if (from.Balance < amount)
                    return false;

                from.Balance -= amount;
                to.Balance += amount;

                // Log til TransactionLogger (ReaderWriterLockSlim)
                TransactionLogger.Instance.Log(new Transaction
                {
                    Time = DateTime.Now,
                    FromAccount = from.Name,
                    ToAccount = to.Name,
                    Amount = amount,
                    Description = description
                });

                return true;
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }
    }
}