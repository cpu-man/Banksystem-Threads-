using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Banksystem.Models;
using Banksystem.Services;

namespace Banksystem.ViewModels
{
    /// <summary>
    /// ViewModel for kontooversigt.
    /// INotifyPropertyChanged sørger for at UI automatisk opdateres
    /// når properties ændrer sig — ingen manuel UI-opdatering nødvendig.
    /// </summary>
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly BankService _bankService;

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Properties med auto-notifikation
        private string _selectedAccountName = string.Empty;
        public string SelectedAccountName
        {
            get => _selectedAccountName;
            set
            {
                if (_selectedAccountName == value) return;
                _selectedAccountName = value;
                OnPropertyChanged(); // Fortæller UI: "denne property er ændret"
            }
        }

        private decimal _selectedAccountBalance;
        public decimal SelectedAccountBalance
        {
            get => _selectedAccountBalance;
            set
            {
                if (_selectedAccountBalance == value) return;
                _selectedAccountBalance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BalanceFormatted)); // Opdater også den formatterede version
            }
        }

        // Read-only property beregnet fra SelectedAccountBalance
        public string BalanceFormatted =>
            $"{_selectedAccountBalance:N2} kr.";

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        // ObservableCollection notificerer automatisk UI når elementer tilføjes/fjernes
        public ObservableCollection<Account> Accounts { get; } = new();

        // Constructor
        public AccountViewModel(BankService bankService)
        {
            _bankService = bankService;
            LoadAccounts();
        }

        // Metoder
        private void LoadAccounts()
        {
            IsLoading = true;
            Accounts.Clear();

            foreach (var account in _bankService.GetAllAccounts())
            {
                Accounts.Add(account);
            }

            IsLoading = false;
        }

        public void SelectAccount(Account account)
        {
            SelectedAccountName = account.Name;
            SelectedAccountBalance = account.Balance;
        }
    }
}