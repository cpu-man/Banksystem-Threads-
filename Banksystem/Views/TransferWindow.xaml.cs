using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Banksystem.Views
{
    /// <summary>
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        public TransferWindow()
        {
            InitializeComponent();
        }

        private void AmountBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (System.Windows.Controls.TextBox)sender;
            string newText = textBox.Text + e.Text;
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(newText, @"^\d*[,.]?\d{0,2}$");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}