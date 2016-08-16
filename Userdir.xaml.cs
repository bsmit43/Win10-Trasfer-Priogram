using System;
using System.Collections.Generic;
using System.IO;
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

namespace Windows10FileplusSettingsTransfer
{
    /// <summary>
    /// Interaction logic for Userdir.xaml
    /// </summary>
    public partial class Userdir : Page
    {
        public Userdir(string textbx)
        {
            InitializeComponent();
            try {
                //MessageBox.Show(computername);
                string[] location = Directory.GetDirectories(@"\\" + textbx + @"\C$\Users\");

                foreach (string folder in location)
                {
                    CheckBox chb = new CheckBox();
                    chb.Content = System.IO.Path.GetFileName(folder);
                    chb.IsChecked = true;
                    chb.FontSize = 15;
                    chb.Height = 25;
                    listBox.Items.Add(chb);

                    Button btn = new Button();
                    btn.Click += delegate
                    {
                        DocTree tree = new DocTree(textbx);
                        //tree.Title = System.IO.Path.GetPathRoot(folder);
                        tree.Show();
                    };

                    btn.Foreground = Brushes.Blue;
                    btn.Background = Brushes.White;
                    btn.BorderBrush = Brushes.White;
                    btn.Height = 35;
                    btn.Content = System.IO.Path.GetFileName(folder) + "    Advanced Selection";
                    listBox.Items.Add(btn);
                }
            }
            catch
            {
                MessageBox.Show("Computer not found.");
            }
        }

        private void CreateDynamicCheckBox()
        {

        }


        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != "")
            {
                Button btn = sender as Button;
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

        