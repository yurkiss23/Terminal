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
using System.Windows.Controls.Primitives;
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

namespace Terminal.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public class MyItem
        {
            public string mess { get; set; }
        }

        public ChatWindow()
        {
            InitializeComponent();
        }

        private void btnSendMes_Click(object sender, RoutedEventArgs e)
        {
            string mesagge = txtYourMes.Text;

            textToReceive.Items.Add(new MyItem { mess = txtYourMes.Text });
            txtYourMes.Clear();
            MessageBox.Show("Nice!");
        }
    }
}
