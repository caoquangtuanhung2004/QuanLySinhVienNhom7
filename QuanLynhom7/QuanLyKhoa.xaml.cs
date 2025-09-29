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
            LoadData();
        }
        private void LoadData()
        {
            dgKhoa.ItemsSource = db.Khoas.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string makhoa = txtMaKhoa.Text.Trim();

            // 1. Kiểm tra dữ liệu bắt buộc
            if (string.IsNullOrWhiteSpace(makhoa) || string.IsNullOrWhiteSpace(txtTenKhoa.Text))
            {
                MessageBox.Show("Mã khoa và tên khoa không được để trống");
                return;
            }
            

            // 2. Kiểm tra trùng Mã SV
            var svTonTai = db.Khoas.FirstOrDefault(s => s.MaKhoa == makhoa);
            if (svTonTai != null)
            {
                MessageBox.Show("Mã khoa đã tồn tại, vui lòng nhập mã khác!");
                return;
            }

            // 3. Thêm mới
            Khoa sv = new Khoa()
            {
                MaKhoa = makhoa,
                TenKhoa = txtTenKhoa.Text.Trim(),
               
            };

            db.Khoas.Add(sv);

            try
            {
                db.SaveChanges();
                MessageBox.Show("Thêm khoa thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void quanlysinhvien_Click(object sender, RoutedEventArgs e)
        {
            quanlysinhvien sinhvienql = new quanlysinhvien();
            sinhvienql.Show();
            this.Close();

        }
        private void quanlymonhoc_Click(object sender, RoutedEventArgs e)
        {
            QuanLyMonHoc qlk = new QuanLyMonHoc();
            qlk.Show();
            this.Close();

        }
        private void QuanLyDiem_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDiem qlk = new QuanLyDiem();
            qlk.Show();
            this.Close();
        }


    }
}
