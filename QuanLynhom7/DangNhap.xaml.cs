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
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserId.Text.Trim();
            string password = pwdBox.Password.Trim();

            if (username == "admin" && password == "admin")
            {
                
                menuchu menu = new menuchu(); 
                menu.Show();

                this.Close(); 
            }
            else
            {
                txtError.Text = "Sai tên đăng nhập hoặc mật khẩu!";
            }
        }
    }
}
