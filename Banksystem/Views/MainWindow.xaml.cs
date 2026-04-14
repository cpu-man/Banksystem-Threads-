using Banksystem.Models;
using Banksystem.Services;
using Banksystem.ViewModels;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;

namespace Banksystem.Views
{
    public partial class MainWindow : Window
    {
        private readonly AccountViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new AccountViewModel(BankService.Instance);
            DataContext = _viewModel;
        }

        private void Overfor_Click(object sender, RoutedEventArgs e)
        {
            var transferWindow = new TransferWindow();
            transferWindow.Owner = this;
            transferWindow.ShowDialog();
        }

        private void Menu_Click(object sender, RoutedEventArgs e) { }


        private void AccountRow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var rectangle = (Rectangle)sender;
                var account = (Account)rectangle.DataContext;

                var transactionView = new TransactionView(TransactionLogger.Instance, account);
                transactionView.Show();
            }
        }
    }
}