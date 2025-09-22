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
    /// Interaction logic for QuanLyKhoa.xaml
    /// </summary>
    public partial class QuanLyKhoa : Window
    {
        QuanLySinhViennhom7Entities db = new QuanLySinhViennhom7Entities();

        public QuanLyKhoa()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            dgKhoa.ItemsSource = db.Khoas.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoa.Text) || string.IsNullOrWhiteSpace(txtTenKhoa.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            var khoa = new Khoa
            {
                MaKhoa = txtMaKhoa.Text,
                TenKhoa = txtTenKhoa.Text
            };

            db.Khoas.Add(khoa);
            db.SaveChanges();
            LoadData();
            MessageBox.Show("Thêm khoa thành công!");
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgKhoa.SelectedItem is Khoa selectedKhoa)
            {
                var k = db.Khoas.FirstOrDefault(x => x.MaKhoa == selectedKhoa.MaKhoa);
                if (k != null)
                {
                    k.TenKhoa = txtTenKhoa.Text;
                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show("Cập nhật thành công!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khoa cần sửa!");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgKhoa.SelectedItem is Khoa selectedKhoa)
            {
                var k = db.Khoas.FirstOrDefault(x => x.MaKhoa == selectedKhoa.MaKhoa);
                if (k != null)
                {
                    db.Khoas.Remove(k);
                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show("Xóa thành công!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khoa cần xóa!");
            }
        }

        private void DgKhoa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgKhoa.SelectedItem is Khoa selectedKhoa)
            {

                txtMaKhoa.Text = selectedKhoa.MaKhoa;
                txtTenKhoa.Text = selectedKhoa.TenKhoa;
            }
        }
    }
}
