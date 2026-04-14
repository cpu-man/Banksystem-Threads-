using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Banksystem.Models
{
    public class Account : INotifyPropertyChanged
    {
        private decimal _balance;

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;

        public decimal Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BalanceColor));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        // Bruges i XAML — grøn hvis positiv, rød hvis negativ
        public Brush BalanceColor =>
            Balance >= 0 ? Brushes.Green : Brushes.Red;

        // Bruges i dropdown i TransferWindow
        public string DisplayName => $"{Name} ({AccountNumber}) — {Balance:N2} kr.";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}