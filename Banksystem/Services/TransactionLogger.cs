using Banksystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Transactions;

namespace Banksystem.Services
{
    public class TransactionLogger
    {
        private static TransactionLogger? _instance;
        public static TransactionLogger Instance => _instance ??= new TransactionLogger();
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly List<Transaction> _transactions = new List<Transaction>();
        private int _newId = 1;


        public TransactionLogger() //Temp data
        {
           Log(new Transaction { Id = 1, Time = DateTime.Now.AddDays(-1), FromAccount = "Lønkonto", ToAccount = "Opsparingskonto", Amount = 1200m, Description = "Opsparing" });
           Log(new Transaction { Id = 2, Time = DateTime.Now.AddDays(-3), FromAccount = "Lønkonto", ToAccount = "Apple", Amount = 109m, Description = "Apple Music abonnement" });
           Log(new Transaction { Id = 3, Time = DateTime.Now.AddDays(-7), FromAccount = "Lønkonto", ToAccount = "Lidl", Amount = 118m, Description = "Lidl Silkeborg" });
           Log(new Transaction { Id = 4, Time = DateTime.Now.AddDays(-8), FromAccount = "Lønkonto", ToAccount = "Molslinjen", Amount = 249m, Description = "Billet" });
        }

        public List<Transaction> GetAll()//Returnerer kopi af listen 
        {
            _lock.EnterReadLock();
            try
            {
                return new List<Transaction>(_transactions);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Log(Transaction transaction)
        {
            _lock.EnterWriteLock();
            try
            {
                transaction.Id = _newId++;
                _transactions.Add(transaction);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
