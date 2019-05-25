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

namespace Terminal.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginSignWindow.xaml
    /// </summary>
    public partial class LoginSignWindow : Window
    {
        public LoginSignWindow()
        {
            InitializeComponent();
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("login");
        }

        private void BtnSign_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("signup");
        }
    }
}
