using System;
using System.Collections.Generic;
using System.Globalization;
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
            loadmonhoc();
        }
        QuanLySinhViennhom7Entities db = new QuanLySinhViennhom7Entities();
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
        private void QuanLyDiem_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDiem qlk = new QuanLyDiem();
            qlk.Show();
            this.Close();
        }

        private void dgMonHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mh = dgMonHoc.SelectedItem as MonHoc;
            if (mh == null)
            {
                return;
            }
            txtMaMon.Text = mh.MaMH;
            txtTenMon.Text = mh.TenMH;
            txtSoTinChi.Text = mh.SoTinChi.ToString();
        }
        public void loadmonhoc()
        {
            dgMonHoc.ItemsSource = db.MonHocs.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadmonhoc();
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            string mamh = txtMaMon.Text.Trim();
            string tenmh = txtTenMon.Text.Trim();
            string stc = txtSoTinChi.Text.Trim();

           
            if (string.IsNullOrWhiteSpace(mamh) || string.IsNullOrWhiteSpace(tenmh) || string.IsNullOrWhiteSpace(stc))
            {
                MessageBox.Show("Mã môn học, tên môn học và số tín chỉ không được để trống!");
                return;
            }

           
            float SoTinChi;
            if (!float.TryParse(stc, NumberStyles.Any, CultureInfo.InvariantCulture, out SoTinChi))
            {
                MessageBox.Show("Số tín chỉ không hợp lệ!");
                return;
            }

            
            var mhTonTai = db.MonHocs.FirstOrDefault(m => m.MaMH == mamh);

            if (mhTonTai != null)
            {
                MessageBox.Show("Mã môn học đã tồn tại, vui lòng nhập mã khác!");
                return;
            }

         
            MonHoc mh = new MonHoc()
            {
                MaMH = mamh,
                TenMH = tenmh,
                SoTinChi = (int)SoTinChi
            };

            db.MonHocs.Add(mh);
            db.SaveChanges();
            loadmonhoc();

            MessageBox.Show("Thêm môn học thành công!");
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            string stc = txtSoTinChi.Text.Trim();
            float SoTinChi;
            if (!float.TryParse(stc, NumberStyles.Any, CultureInfo.InvariantCulture, out SoTinChi))
            {
                MessageBox.Show("Số tín chỉ không hợp lệ!");
                return;
            }
            MonHoc mhsua = dgMonHoc.SelectedItem as MonHoc;
            if (mhsua == null)
            {
                MessageBox.Show("Chon môn học cần sửa ");

            }
            else
            {
                MonHoc sv = db.MonHocs.Find(mhsua.MaMH);
                sv.TenMH = txtTenMon.Text;
                sv.SoTinChi = (int)SoTinChi;               
                db.SaveChanges();
                MessageBox.Show("Sửa thành công");
                loadmonhoc();
            }

        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            MonHoc mhxoa = dgMonHoc.SelectedItem as MonHoc;
            if (mhxoa == null)
            {
                MessageBox.Show("Chọn môn học để xóa");
                return;
            }

            MonHoc mh = db.MonHocs.Find(mhxoa.MaMH);
            db.MonHocs.Remove(mh);
            db.SaveChanges();
            MessageBox.Show("Xoa môn học thành công");
            loadmonhoc(); 
        }
    }
}
