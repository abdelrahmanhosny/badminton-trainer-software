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

namespace Badminton_SW_v1._0
{
    /// <summary>
    /// Interaction logic for _2ndPage.xaml
    /// </summary>
    public partial class _3rdPage : Window
    {
        public _3rdPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _2ndPage main = new _2ndPage();
            main.Show();
            this.Close();
        }
    }
}
