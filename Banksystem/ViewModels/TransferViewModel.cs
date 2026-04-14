using Banksystem.Models;
using Banksystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Banksystem.ViewModels
{
    public class TransferViewModel : INotifyPropertyChanged
    {
        private readonly BankService _bankService;

        private Account? _selectedFromAccount;
        private Account? _selectedToAccount;
        private string _amountText = string.Empty;
        private string _statusMessage = string.Empty;
        private bool _statusVisible = false;
        private bool _isSuccess = false;
        private bool _isBusy = false;

        public TransferViewModel()
        {
            _bankService = BankService.Instance;
            TransferCommand = new RelayCommand(ExecuteTransfer, CanExecuteTransfer);

            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName is nameof(AmountText)
                    or nameof(SelectedFromAccount)
                    or nameof(SelectedToAccount)
                    or nameof(IsBusy))
                {
                    ((RelayCommand)TransferCommand).RaiseCanExecuteChanged();
                }
            };
        }

        public ObservableCollection<Account> Accounts => _bankService.Accounts;

        public Account? SelectedFromAccount
        {
            get => _selectedFromAccount;
            set { _selectedFromAccount = value; OnPropertyChanged(); }
        }

        public Account? SelectedToAccount
        {
            get => _selectedToAccount;
            set { _selectedToAccount = value; OnPropertyChanged(); }
        }

        public string AmountText
        {
            get => _amountText;
            set { _amountText = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            private set { _statusMessage = value; OnPropertyChanged(); }
        }

        public Visibility StatusVisibility =>
            _statusVisible ? Visibility.Visible : Visibility.Collapsed;

        public bool IsSuccess
        {
            get => _isSuccess;
            private set { _isSuccess = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            private set { _isBusy = value; OnPropertyChanged(); }
        }

        public ICommand TransferCommand { get; }

        private bool CanExecuteTransfer(object? _)
        {
            if (IsBusy) return false;
            if (SelectedFromAccount == null || SelectedToAccount == null) return false;
            if (SelectedFromAccount == SelectedToAccount) return false;
            if (!TryParseAmount(out decimal amount)) return false;
            return amount > 0;
        }

        private void ExecuteTransfer(object? _)
        {
            if (!TryParseAmount(out decimal amount)) return;

            var from = SelectedFromAccount!;
            var to = SelectedToAccount!;
            string description = $"Transfer from {from.AccountNumber} to {to.AccountNumber}";

            IsBusy = true;
            HideStatus();

            ThreadPoolManager.Instance.QueueWork(() =>
            {
                bool success = _bankService.Transfer(from, to, amount, description);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    IsBusy = false;
                    if (success)
                    {
                        ShowStatus($"Transfer of {amount:N2} kr. completed!", true);
                        AmountText = string.Empty;
                    }
                    else
                    {
                        ShowStatus("Transfer failed. Insufficient funds.", false);
                    }
                });
            });
        }

        private bool TryParseAmount(out decimal amount)
        {
            string normalized = AmountText.Replace(',', '.');
            return decimal.TryParse(normalized,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out amount) && amount > 0;
        }

        private void ShowStatus(string message, bool success)
        {
            StatusMessage = message;
            IsSuccess = success;
            _statusVisible = true;
            OnPropertyChanged(nameof(StatusVisibility));
        }

        private void HideStatus()
        {
            _statusVisible = false;
            OnPropertyChanged(nameof(StatusVisibility));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);
        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}