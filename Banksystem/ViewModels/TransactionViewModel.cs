using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Banksystem.Models;
using Banksystem.Services;
//using System.Transactions;

namespace Banksystem.ViewModels
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly TransactionLogger _logger;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Transaction> Transactions { get; } = new ObservableCollection<Transaction>();

        public TransactionViewModel(TransactionLogger logger)
        {
            _logger = logger;
            Refresh();
        }

        public void Refresh()
        {
            Transactions.Clear();
            foreach (var x in _logger.GetAll())
                Transactions.Add(x); 
        }

        //public void TempData()
        //{
        //    transactions.Add(new Transaction { Id = 1, Time = DateTime.Now.AddDays(-1), FromAccount = "Lønkonto", ToAccount = "Opsparingskonto", Amount = 1200m, Description = "Opsparing" });
        //    transactions.Add(new Transaction { Id = 2, Time = DateTime.Now.AddDays(-3), FromAccount = "Lønkonto", ToAccount = "Apple", Amount = 109m, Description = "Apple Music abonnement" });
        //    transactions.Add(new Transaction { Id = 3, Time = DateTime.Now.AddDays(-7), FromAccount = "Lønkonto", ToAccount = "Lidl", Amount = 118m, Description = "Lidl Silkeborg" });
        //    transactions.Add(new Transaction { Id = 4, Time = DateTime.Now.AddDays(-8), FromAccount = "Lønkonto", ToAccount = "Molslinjen", Amount = 249m, Description = "Billet" });
        //}
    }
}
