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
    /// Interaction logic for Method.xaml
    /// </summary>
    public partial class Method : Page
    {
        public Method()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox.Text))
            {
                try
                {
                    string[] location = Directory.GetDirectories(@"\\" + textBox.Text + @"\C$\Users\");
                    _methodFrame.Navigate(new Userdir(textBox.Text));
                } catch
                {
                    MessageBox.Show("Computer not Found");
                }
            }        
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] location = Directory.GetDirectories(@"\\" + textBox.Text + @"\C$\Users\");
            _methodFrame.Navigate(new Userdir(textBox.Text));
            }
            catch
            {
                MessageBox.Show("Computer not Found");
            }
        }
    }
}
