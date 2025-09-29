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
        private void QuanLyDiem_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDiem qlk = new QuanLyDiem();
            qlk.Show();
            this.Close();
        }
        private void loadlop()
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

        private void btnsua_Click(object sender, RoutedEventArgs e)
        {
            SinhVien svsua = datadanhachsinhvien.SelectedItem as SinhVien;
            if (svsua == null)
            {
                MessageBox.Show("Chọn sinh viên để sửa");
            }
            else
            {
                SinhVien sv = db.SinhViens.Find(svsua.MaSV);
                sv.HoTen = txthoten.Text;
                sv.DiaChi = txtdiachi.Text;
                sv.NgaySinh = datengaysinh.SelectedDate.Value;
                sv.MaLop = (dslopcombox.SelectedItem as Lop).MaLop;
                if (radionam.IsChecked == true)
                {
                    sv.GioiTinh = "Nam";
                }
                else if (raduonu.IsChecked == true)
                {
                    sv.GioiTinh = "Nữ";
                }


                db.SaveChanges();
                MessageBox.Show("Sửa thành công");
                load();
            }
        }

        private void btnxoa_Click(object sender, RoutedEventArgs e)
        {
            SinhVien svxoa = datadanhachsinhvien.SelectedItem as SinhVien;
            if (svxoa == null)
            {
                MessageBox.Show("Hãy chọn 1 sinh viên để xóa");
            }
            else
            {
                SinhVien sv = db.SinhViens.Find(svxoa.MaSV);
                db.SinhViens.Remove(sv);
                db.SaveChanges();
                MessageBox.Show("Xóa thành công");
                load();
            }
        }

        private void btntimkiem_Click(object sender, RoutedEventArgs e)
        {
            string masv = txtmasv.Text.Trim();
            if (string.IsNullOrEmpty(masv))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên cần tìm");
                return;
            }
            SinhVien sv = db.SinhViens.FirstOrDefault(s => s.MaSV == masv);
            if (sv != null)
            {
                txtmasv.Text = sv.MaSV;
                txthoten.Text = sv.HoTen;
                txthoten.Text = sv.HoTen;

                if (sv.NgaySinh != null)
                {
                    datengaysinh.SelectedDate = sv.NgaySinh;
                }
                if (sv.Lop != null)
                {
                    dslopcombox.SelectedValue = sv.Lop.MaLop;
                }
                MessageBox.Show("Đã tìm thấy sinh viên!");
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên với mã: " + masv);
            }
        }

        private void datadanhachsinhvien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sv = datadanhachsinhvien.SelectedItem as SinhVien;
            if (sv == null) return;

            txtmasv.Text = sv.MaSV;
            txthoten.Text = sv.HoTen;
            txtdiachi.Text = sv.DiaChi;
            if (sv.NgaySinh != null)
            {
                datengaysinh.SelectedDate = sv.NgaySinh;
            }
            if (sv.Lop != null)
            {
                dslopcombox.SelectedValue = sv.Lop.MaLop;
            }
            if (!string.IsNullOrEmpty(sv.GioiTinh))
            {
                if (sv.GioiTinh.Equals("Nam", StringComparison.OrdinalIgnoreCase))
                {
                    radionam.IsChecked = true;
                    raduonu.IsChecked = false;
                }
                else
                {
                    radionam.IsChecked = false;
                    raduonu.IsChecked = true;
                }
            }


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            QuanLyKhoa qlk = new QuanLyKhoa();
            qlk.Show();
            this.Close();

        }

        private void quanlymonhoc_Click(object sender, RoutedEventArgs e)
        {

            QuanLyMonHoc qlk = new QuanLyMonHoc();
            qlk.Show();
            this.Close();


        }
    }
}
