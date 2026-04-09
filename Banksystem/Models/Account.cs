using System.Windows.Media;

namespace Banksystem.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        // Bruges direkte i XAML binding — grøn hvis positiv, rød hvis negativ
        public Brush BalanceColor =>
            Balance >= 0 ? Brushes.Green : Brushes.Red;
    }
}