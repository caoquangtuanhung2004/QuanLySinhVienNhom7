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
    /// Interaction logic for QuanLyMonHoc.xaml
    /// </summary>
    public partial class QuanLyMonHoc : Window
    {
        public QuanLyMonHoc()
        {
            InitializeComponent();
        }

        private void quanlysinhvien_Click(object sender, RoutedEventArgs e)
        {
            quanlysinhvien qlk = new quanlysinhvien();
            qlk.Show();
            this.Close();
        }

        private void quanlykhoa_Click(object sender, RoutedEventArgs e)
        {
            QuanLyKhoa qlk = new QuanLyKhoa();
            qlk.Show();
            this.Close();
        }
    }
}
