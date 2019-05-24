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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Terminal.Windows;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mlbd_Click(object sender, MouseButtonEventArgs e)
        {
            LoginSignWindow loginSign = new LoginSignWindow();
            loginSign.ShowDialog();
        }

        private void BtnCashIn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("cashIn");
        }

        private void BtnCashSend_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("cashSend");
        }

        private void BtnCashOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("cashOut");
        }

        private void BtnArch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("archive");
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            ChatWindow chat = new ChatWindow();
            chat.ShowDialog();
        }
    }
}
