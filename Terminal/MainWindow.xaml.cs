using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
        List<ViewUserModel> _userList;
        private string url = null;

        public int UserID { get; set; }
        public string Source { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _context = new EFContext();
            //_baseUrl = ConfigurationManager.AppSettings["baseUrl"];

            url = $"{ConfigurationManager.AppSettings["baseUrl"]}api/users";
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
                        UserID = loginSign.txtLogin.Text.Contains("@") ?
                            loginSign._userList.Where(u=>u.Email==loginSign.txtEmail.Text).First().Id:
                            loginSign._userList.Where(u => u.Phone == loginSign.txtLogin.Text).First().Id;
                        break;
                    case "admin":
                        UserID = loginSign.txtLogin.Text.Contains("@") ?
                            loginSign._adminList.Where(u => u.Email == loginSign.txtLogin.Text).First().Id:
                            loginSign._adminList.Where(u => u.Phone == loginSign.txtLogin.Text).First().Id;
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
                    User tmp = null;// _context.Users.Where(u => u.Id == UserID).First();
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        var result = client.DownloadString(url);
                        List<UserModel> tmpList = JsonConvert.DeserializeObject<List<UserModel>>(result);
                        MessageBox.Show(UserID.ToString());
                        tmp = tmpList.Where(u => u.Id == UserID).First();
                    }
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
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                _userList = JsonConvert.DeserializeObject<List<ViewUserModel>>(
                    client.DownloadString(url)).Select(u => new ViewUserModel()
                    {
                        Id = u.Id,
                        Fname = u.Fname,
                        Lname = u.Lname
                    }).ToList();
            }
            dgUsers.ItemsSource = _userList.DefaultIfEmpty();
            dgUsers.Visibility = Visibility.Visible;
            spUser.Visibility = Visibility.Visible;
            spUserBtn.Visibility = Visibility.Visible;
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

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            KR cnv = new KR();
            cnv.ShowDialog();
        }

        private void SelectionChanged_User(object sender, SelectionChangedEventArgs e)
        {
            //int id = (dgUsers.SelectedItem as ViewUserModel).Id;
            try
            {
                if(dgUsers.SelectedItem != null)
                {
                    ViewUserModel tmp = dgUsers.SelectedItem as ViewUserModel;
                    User select = null;// _context.Users.Where(u => u.Id == tmp.Id).First();
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        select = JsonConvert.DeserializeObject<List<UserModel>>(client.DownloadString(url))
                            .Where(u => u.Id == tmp.Id).First();
                    }
                    lblName.Content = select.Fname + " " + select.Lname;
                    lblPhone.Content = select.Phone;
                    lblEmail.Content = select.Email;
                    lblMoney.Content = select.Money;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            int id = (dgUsers.SelectedItem as ViewUserModel).Id;
            try
            {
                //using (WebClient client = new WebClient())
                //{
                //    client.Encoding = Encoding.UTF8;
                //    client.Headers.Add("Content-Type", "application/json");
                //    string method = "POST";
                //    var result = client.UploadString(url, method, id.ToString());
                //    MessageBox.Show("!!!");
                //}
                _context.Users.Remove(_context.Users.Where(u => u.Id == id).First());
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            UserID = (dgUsers.SelectedItem as ViewUserModel).Id;
            try
            {
                User select = _context.Users.Where(u => u.Id == UserID).First();
                MessageBoxResult result =
                    MessageBox.Show("update phone or email?", "update contacts", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        UpdateWindow update = new UpdateWindow();
                        update.Owner = this;
                        if (update.ShowDialog() == true)
                        {
                            select.Phone = update.txtUpdPhone.Text;
                            select.Email = update.txtUpdEmail.Text;
                            _context.SaveChanges();
                        }
                        break;
                    case MessageBoxResult.No:
                        return;
                    default:
                        break;
                }
                result =
                    MessageBox.Show("update name?", "update name", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        UpdateNameWindow updateName = new UpdateNameWindow();
                        updateName.Owner = this;
                        if (updateName.ShowDialog() == true)
                        {
                            select.Fname = updateName.txtUpdFname.Text;
                            select.Lname = updateName.txtUpdLname.Text;
                            _context.SaveChanges();
                        }
                        break;
                    case MessageBoxResult.No:
                        return;
                    default:
                        break;
                }
                //_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User select = null;// _context.Users.Where(u => u.Id == UserID).First();
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    select = JsonConvert.DeserializeObject<List<UserModel>>(client.DownloadString(url))
                        .Where(u => u.Id == UserID).First();
                }
                lblName.Content = select.Fname + " " + select.Lname;
                lblPhone.Content = select.Phone;
                lblEmail.Content = select.Email;
                lblMoney.Content = select.Money;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                dgUsers.ItemsSource = 
                    JsonConvert.DeserializeObject<List<ViewUserModel>>(client.DownloadString(url))
                    .Select(u => new ViewUserModel()
                    {
                        Id = u.Id,
                        Fname = u.Fname,
                        Lname = u.Lname
                    }).ToList();
            }
            
        }
    }
}
