using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Terminal.Windows
{
    /// <summary>
    /// Логика взаимодействия для KR.xaml
    /// </summary>
      public class Bablo
    {
        public string ccy { get; set; }
    public string base_ccy { get; set; }
    public string buy { get; set; }
    public string sale { get; set; }
}
public partial class KR : Window
    {
        List<Bablo> babki;
        public KR()
        {
            InitializeComponent();
            using (WebClient wc = new WebClient())
            {
                string url = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";
                string res = wc.DownloadString(url);
                babki = JsonConvert.DeserializeObject<List<Bablo>>(res);
            }
            currency.SelectedIndex = 0;
        }
        public void Exchange()
        {
            if (!string.IsNullOrEmpty(number.Text) && number.Text.All(char.IsDigit))
            {
                switch (currency.SelectedIndex)
                {
                    case 0:
                        double digit1 = Convert.ToDouble(number.Text) / double.Parse(babki[0].buy, System.Globalization.CultureInfo.InvariantCulture);
                        result.Content = Convert.ToString(digit1);
                        break;
                    case 1:
                        double digit2 = Convert.ToDouble(number.Text) / double.Parse(babki[1].buy, System.Globalization.CultureInfo.InvariantCulture);
                        result.Content = Convert.ToString(digit2);
                        break;
                    case 2:
                        double digit3 = Convert.ToDouble(number.Text) / double.Parse(babki[2].buy, System.Globalization.CultureInfo.InvariantCulture);
                        result.Content = Convert.ToString(digit3);
                        break;
                }
            }
            else
                result.Content = "...";
        }

        private void currency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Exchange();
        }

        private void number_TextChanged(object sender, TextChangedEventArgs e)
        {
            Exchange();
        }
    }
}
