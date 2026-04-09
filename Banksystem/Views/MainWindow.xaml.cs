using System.Windows;
using Banksystem.Services;
using Banksystem.ViewModels;

namespace Banksystem.Views
{
    public partial class MainWindow : Window
    {
        private readonly AccountViewModel _viewModel;

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
    }
}