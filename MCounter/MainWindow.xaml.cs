using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace MCounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string check = "";
        static bool flag = false;
        string path = @"..\..\spend_data.txt";
        public MainWindow()
        {
            InitializeComponent();
            
            //FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
            //DirectoryInfo dirInfo = new DirectoryInfo(path);
        }

        [Serializable]
        public class product
        {
            public string Type { get; set; }
            public string Additional { get; set; }
            public string MoneySpend { get; set; }

        }

        private void add_spend_fun(object sender, RoutedEventArgs e)
        {
            string temp_add = "";
            if (list_spend.Text != "" && Money.Text != "")
            {

                temp_add += list_spend.Text + " | " + Money.Text + " руб.";

                if (adding_list_spend.Text == "")
                {
                    temp_add += " | Без указания доб.информации";
                }
                else
                {
                    temp_add += " | " + adding_list_spend.Text;
                }
                temp_add += " | " + check;

                mainTextShow.Text += temp_add + "\n";

                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(temp_add);
                }
            }
            else
            {
                error_window ew = new error_window();
                ew.error_text.Text += "\nНе указан товар или его стоимость";
                ew.Owner = this;
                ew.ShowDialog();
                if ((list_spend.Text != "" && Money.Text == "") || (list_spend.Text == "" && Money.Text != "")) { flag = true; }
            }

            if (!flag)
            {
                list_spend.Text = "";
                Money.Text = "";
                adding_list_spend.Text = "";
            }

            flag = false;

        }

        private void need_spend_check(object sender, RoutedEventArgs e)
        {
            check = "Категория : нужный";
        }
        private void no_need_spend_check(object sender, RoutedEventArgs e)
        {
            check = "Категория : необязательный";
        }
    }
}
