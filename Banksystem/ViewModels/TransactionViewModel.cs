using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Banksystem.Models;
//using System.Transactions;

namespace Banksystem.ViewModels
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Transaction> transactions { get; } = new ObservableCollection<Transaction>();

        public TransactionViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            transactions.Add(new Transaction { Id = 1, Time = DateTime.Now.AddDays(-1), FromAccount = "Lønkonto", ToAccount = "Opsparingskonto", Amount = 1200m, Description = "Opsparing" });
            transactions.Add(new Transaction { Id = 2, Time = DateTime.Now.AddDays(-3), FromAccount = "Lønkonto", ToAccount = "Apple", Amount = 109m, Description = "Apple Music abonnement" });
            transactions.Add(new Transaction { Id = 3, Time = DateTime.Now.AddDays(-7), FromAccount = "Lønkonto", ToAccount = "Lidl", Amount = 118m, Description = "Lidl Silkeborg" });
        }
    }
}
