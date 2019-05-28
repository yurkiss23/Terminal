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
using Terminal.Entities;

namespace Terminal.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginSignWindow.xaml
    /// </summary>
    public partial class LoginSignWindow : Window
    {
        private EFContext _context;
        public LoginSignWindow()
        {
            InitializeComponent();
            _context = new EFContext();
        }

        public bool IsEmpty(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return false;
            }
            return true;
        }
        public bool IsSymbol(string s, ref int count)
        {
            string example = "!@#$%^&";
            foreach(char c in example)
            {
                foreach(char cc in s)
                {
                    if (cc == c)
                    {
                        count++;
                        if (count == 3)
                        {
                            return true;
                        }
                    }
                }
            }
            if (count == 0)
            {
                return false;
            }
            return true;
        }
        public bool IsEmail(string s)
        {
            if (!s.Contains("@"))
            {
                return false;
            }
            return true;
        }
        public bool IsPhone(string s)
        {
            string example = "0123456789";
            foreach(char c in s)
            {
                if (!example.Contains(c.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsAmount(string s)
        {
            if (s.Count() < 8)
            {
                return false;
            }
            return true;
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("login");
        }

        private void BtnSign_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("signup");
            List<string> fieldList = new List<string> {
                txtFname.Text,txtLname.Text,txtEmail.Text,txtPhone.Text,txtSignPass.Password};
            try
            {
                foreach (string field in fieldList)
                {
                    if (IsEmpty(field))
                    {
                        MessageBox.Show("not all fields are full!");
                        return;
                    }
                }
                LostFocus_Pass(sender, e);
                _context.Users.Add(new User
                {
                    Money = 0,
                    Fname = txtFname.Text,
                    Lname = txtLname.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    Password = txtSignPass.Password
                });
                //_context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LostFocus_Fname(object sender, RoutedEventArgs e)
        {
            if (IsEmpty(txtFname.Text))
            {
                MessageBox.Show("field 'FirstName' is empty!");
            }
        }

        private void LostFocus_Lname(object sender, RoutedEventArgs e)
        {
            if (IsEmpty(txtLname.Text))
            {
                MessageBox.Show("field 'LastName' is empty!");
            }
        }

        private void LostFocus_Phone(object sender, RoutedEventArgs e)
        {
            if (IsEmpty(txtPhone.Text))
            {
                MessageBox.Show("field 'Phone' is empty!");
            }
            if (!IsPhone(txtPhone.Text))
            {
                MessageBox.Show("phone: wrong format!");
            }
        }

        private void LostFocus_Email(object sender, RoutedEventArgs e)
        {
            if (IsEmpty(txtEmail.Text))
            {
                MessageBox.Show("field 'Email' is empty!");
            }
            if (!IsEmail(txtEmail.Text))
            {
                MessageBox.Show("email: wrong format!");
            }
        }

        private void LostFocus_Pass(object sender, RoutedEventArgs e)
        {
            if (IsEmpty(txtSignPass.Password.ToString()))
            {
                MessageBox.Show("field 'Password' is empty!");
                return;
            }
            if (!IsAmount(txtSignPass.Password.ToString()))
            {
                MessageBox.Show("weak password!");
            }
            else
            {
                int count = 0;
                if (IsSymbol(txtSignPass.Password.ToString(), ref count))
                {
                    string pass = null;
                    pass = (count == 3) ? "good password" : "normal password";
                    MessageBox.Show(pass);
                }
            }
        }
    }
}
