using System.Windows;
using Banksystem.Services;
using Banksystem.ViewModels;

namespace Banksystem.Views
{
    public partial class MainWindow : Window
    {
        private readonly AccountViewModel _viewModel;
        TransactionLogger _logger = new TransactionLogger();

        public MainWindow()
        {
            InitializeComponent();

            var bankService = new BankService();
            _viewModel = new AccountViewModel(bankService);
            DataContext = _viewModel;
        }

        private void Overfor_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Åbn TransferWindow
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Åbn menu
        }

        private void Transactions_Click(object sender, RoutedEventArgs e)
        {
            TransactionView transaction = new TransactionView(_logger);
            transaction.Show();
        }
    }
}