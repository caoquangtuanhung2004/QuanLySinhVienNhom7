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

namespace QuanLynhom7
{
    /// <summary>
    /// Interaction logic for menuchu.xaml
    /// </summary>
    public partial class menuchu : Window
    {
        public menuchu()
        {
            InitializeComponent();
        }

        private void quanlysinhvien_Click(object sender, RoutedEventArgs e)
        {
            quanlysinhvien sinhvienql = new quanlysinhvien();
            sinhvienql.Show();
            this.Close();

        }
    }
}
