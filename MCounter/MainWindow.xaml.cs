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

        public class product
        {
            public product(string t, string a, string m, string c)
            {
                Type = t;
                Additional = a;
                MoneySpend = m;
                Category = c;
            }
            public string Type { get; set; }
            public string Additional { get; set; }
            public string MoneySpend { get; set; }
            public string Category { get; set; } 

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

        private void show_spend_fun(object sender, RoutedEventArgs e)
        {
            mainTextShow.Text = "";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    mainTextShow.Text += line + "\n";
                }
            }


            var tl= new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string word = "";
                int f = 0;
                while (sr.Peek() != -1)
                {
                    char s = (char)sr.Read();
                    if (s != '|' && s!='\n') { word += s;}
                    else {
                        tl.Add(word);
                        word = "";
                    }
                }

                var product_list = new List<product>();

                for(int i = 0; i < tl.Count; i=i+4)
                {
                    Console.WriteLine(tl.Count);
                    product pd = new product(tl[i], tl[i + 1], tl[i + 2], tl[i + 3]);
                    product_list.Add(pd);
                }
                table_data.ItemsSource = product_list;
            }

               

        }
    }
}
