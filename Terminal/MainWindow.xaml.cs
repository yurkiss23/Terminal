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
using Terminal.Entities;
using Terminal.Models;
using Terminal.Windows;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EFContext _context;
        public int UserID { get; set; }
        public string Source { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _context = new EFContext();

            cbTheme.SelectionChanged += SelectionChanged_Themes;
        }

        private void SelectionChanged_Themes(object sender, SelectionChangedEventArgs e)
        {
            string style = (cbTheme.SelectedItem as ComboBoxItem).Content.ToString();
            var uri = new Uri("Themes/" + style + ".xaml", UriKind.Relative);
            ResourceDictionary dict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        private void mlbd_Click(object sender, MouseButtonEventArgs e)
        {
            LoginSignWindow loginSign = new LoginSignWindow();
            loginSign.Owner = this;
            if (loginSign.ShowDialog() == true)
            {
                this.Source = loginSign.Source;
                switch (Source)
                {
                    case "user":
                        UserID = (loginSign.txtLogin.Text.Contains("@")) ?
                            _context.Users.Where(u => u.Email == loginSign.txtLogin.Text).First().Id :
                            _context.Users.Where(u => u.Phone == loginSign.txtLogin.Text).First().Id;
                        break;
                    case "admin":
                        UserID = (loginSign.txtLogin.Text.Contains("@")) ?
                            _context.Admins.Where(u => u.Email == loginSign.txtLogin.Text).First().Id :
                            _context.Admins.Where(u => u.Phone == loginSign.txtLogin.Text).First().Id;
                        break;
                    case null:
                        break;
                    default:
                        break;
                }
                //MessageBox.Show(UserID.ToString());
            }
            switch (Source)
            {
                case "user":
                    User tmp = _context.Users.Where(u => u.Id == UserID).First();
                    tbAccount.Content = tmp.Fname + " " + tmp.Lname;
                    tbMoney.Content = tmp.Money;
                    break;
                case "admin":
                    IsAdmin();
                    break;
                case null:
                    break;
                default:
                    break;
            }
        }
        public void IsAdmin()
        {
            Admin tmp = _context.Admins.Where(a => a.Id == UserID).First();
            tbAccount.Content = tmp.Fname + " " + tmp.Lname;
            tbMoney.Content = "admin";
            btnCashIn.Visibility = btnCashOut.Visibility = btnCashSend.Visibility
                = btnChat.Visibility = btnArch.Visibility = Convert.Visibility = Visibility.Hidden;
            List<UserModel> userList = new List<UserModel>(
                _context.Users.Select(u => new UserModel()
                {
                    Id = u.Id,
                    Money = u.Money,
                    Fname = u.Fname,
                    Lname = u.Lname,
                    Phone = u.Phone,
                    Email = u.Email
                }).ToList());
        }

        private void BtnCashIn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("cashIn");
            btnCashSend.Visibility = Visibility.Hidden;
            //tbTitleLayout.Content = "Content";
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

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("currency converter");
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            KR cnv = new KR();
            cnv.ShowDialog();
        }
    }
}
