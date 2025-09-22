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
    /// Interaction logic for quanlysinhvien.xaml
    /// </summary>
    public partial class quanlysinhvien : Window
    {
        QuanLySinhViennhom7Entities db = new QuanLySinhViennhom7Entities();
        public quanlysinhvien()
        {
            InitializeComponent();
            loadlop();
        }
        private void  loadlop()
        {
            var dsl = db.Lops.ToList();
            dslopcombox.ItemsSource = dsl;
        }

        private void btnthem_Click(object sender, RoutedEventArgs e)
        {
            string maSV = txtmasv.Text.Trim();

            // 1. Kiểm tra dữ liệu bắt buộc
            if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(txthoten.Text))
            {
                MessageBox.Show("Mã SV và Họ tên không được để trống");
                return;
            }
            if (datengaysinh.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh");
                return;
            }
            if (dslopcombox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn lớp");
                return;
            }

            // 2. Kiểm tra trùng Mã SV
            var svTonTai = db.SinhViens.FirstOrDefault(s => s.MaSV == maSV);
            if (svTonTai != null)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại, vui lòng nhập mã khác!");
                return;
            }

            // 3. Thêm mới
            SinhVien sv = new SinhVien()
            {
                MaSV = maSV,
                HoTen = txthoten.Text.Trim(),
                DiaChi = txtdiachi.Text.Trim(),
                GioiTinh = radionam.IsChecked == true ? "Nam" : "Nữ",
                NgaySinh = datengaysinh.SelectedDate.Value,
                MaLop = (dslopcombox.SelectedItem as Lop).MaLop
            };

            db.SinhViens.Add(sv);

            try
            {
                db.SaveChanges();
                MessageBox.Show("Thêm sinh viên thành công!");
                load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }


        public void load()
        {
            
            datadanhachsinhvien.ItemsSource = db.SinhViens.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }
    }
}
