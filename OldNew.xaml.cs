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

namespace Windows10FileplusSettingsTransfer
{
    /// <summary>
    /// Interaction logic for OldNew.xaml
    /// </summary>
    public partial class OldNew : Page
    {
        public OldNew()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _oldNewFrame.Navigate(new Method());
           
            //filesizes.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _oldNewFrame.Navigate(new Method());         
        }
    }
}
