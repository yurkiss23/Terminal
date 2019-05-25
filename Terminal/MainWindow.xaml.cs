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

            //ThemeChange();
            List<string> themesList = new List<string> { "YellowGrayTheme", "VioletTheme" };
            cbTheme.SelectionChanged += SelectionChanged_Themes;
            //cbTheme.Items.Clear();
            cbTheme.ItemsSource = themesList;
            //cbTheme.SelectedItem = "YellowGrayTheme";
        }

        private void SelectionChanged_Themes(object sender, SelectionChangedEventArgs e)
        {
            string style = cbTheme.SelectedItem as string;
            var uri = new Uri("Themes/" + style + ".xaml", UriKind.Relative);
            ResourceDictionary dict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        //private void ThemeChange()
        //{
        //    string style = "Themes/YellowGrayTheme";
        //    var uri = new Uri(style + ".xaml", UriKind.Relative);
        //    ResourceDictionary dict = Application.LoadComponent(uri) as ResourceDictionary;
        //    Application.Current.Resources.Clear();
        //    Application.Current.Resources.MergedDictionaries.Add(dict);
        //}

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

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("currency converter");
        }
    }
}
