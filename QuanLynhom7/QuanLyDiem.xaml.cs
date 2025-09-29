using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLynhom7
{
    /// <summary>
    /// Interaction logic for QuanLyDiem.xaml
    /// </summary>
    public partial class QuanLyDiem : Window
    {
        public QuanLyDiem()
        {
            InitializeComponent();
            LoadDIem();
        }
        QuanLySinhViennhom7Entities qlsv = new QuanLySinhViennhom7Entities();
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
        private void quanlykhoa_Click(object sender, RoutedEventArgs e)
        {
            QuanLyKhoa qlk = new QuanLyKhoa();
            qlk.Show();
            this.Close();

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string maSV = txtMaSV.Text;
            string maMH = txtMaMonHoc.Text;
            var sv = qlsv.SinhViens.FirstOrDefault(x => x.MaSV == maSV);
            var mh = qlsv.MonHocs.FirstOrDefault(x => x.MaMH == maMH);


            if (sv != null && mh != null)
            {

                Diem diem = new Diem
                {
                    MaSV = sv.MaSV,
                    MaMH = mh.MaMH,
                    DiemCC = float.Parse(txtDiemCC.Text),
                    DiemThaoLuan = float.Parse(txtDiemTL.Text),
                    DiemGK = float.Parse(txtDiemGK.Text),
                    DiemThi = float.Parse(txtDiemCK.Text),
                    DiemTB = (float.Parse(txtDiemCC.Text) + float.Parse(txtDiemTL.Text) + float.Parse(txtDiemGK.Text) * 2 + float.Parse(txtDiemCK.Text) * 3) / 6
                };
                var ktra = qlsv.Diems.FirstOrDefault(d => d.MaSV == diem.MaSV && d.MaMH == diem.MaMH);
                if (ktra == null)
                {
                    qlsv.Diems.Add(diem);
                    try
                    {
                        qlsv.SaveChanges();
                        MessageBox.Show("Thành Công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException?.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Sinh Vien Da Co Diem Mon Nay");
                }



                LoadDIem();



            }
            else
            {
                MessageBox.Show("Khong Tim thay sinh vien hoac mon hoc");
            }



        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            var d_Sua = listDiem.SelectedItem;
            if (d_Sua == null)
            {
                MessageBox.Show("Hay chon bang diem can sua");

            }
            else
            {
                string maSV = d_Sua.GetType().GetProperty("MaSV").GetValue(d_Sua, null).ToString();
                string maMH = d_Sua.GetType().GetProperty("MaMH").GetValue(d_Sua, null).ToString();
                Diem diem = qlsv.Diems.FirstOrDefault(d => d.MaSV == maSV && d.MaMH == maMH);

                if (diem != null)
                {
                    diem.DiemCC = float.Parse(txtDiemCC.Text);
                    diem.DiemThaoLuan = float.Parse(txtDiemTL.Text);
                    diem.DiemGK = float.Parse(txtDiemGK.Text);
                    diem.DiemThi = float.Parse(txtDiemCK.Text);
                    diem.DiemTB = (float.Parse(txtDiemCC.Text) + float.Parse(txtDiemTL.Text) + float.Parse(txtDiemGK.Text) * 2 + float.Parse(txtDiemCK.Text) * 3) / 6;

                    qlsv.SaveChanges();
                    MessageBox.Show(" Sua Diem Thanh Cong");
                    LoadDIem();
                }
                else
                {
                    MessageBox.Show("Khong Tim Thay Diem Can Sua");
                }
            }
        }



        public void LoadDIem()
        {
            var list = qlsv.Diems.Include(d => d.SinhVien).Include(d => d.MonHoc).Select(d => new
            {
                TenSV = d.SinhVien.HoTen,
                TenMH = d.MonHoc.TenMH,
                d.MaSV,
                d.MaMH,
                d.DiemCC,
                d.DiemThaoLuan,
                d.DiemGK,
                d.DiemThi,
                d.DiemTB
            }).ToList();
            listDiem.ItemsSource = list;

        }

        private void listDiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listDiem.SelectedItem == null) return;
            dynamic diem = listDiem.SelectedItem;
            try
            {
                txtTenSV.Text = diem.TenSV.ToString();


                txtTenMonHoc.Text = diem.TenMH.ToString();
                txtDiemCC.Text = diem.DiemCC.ToString();
                txtDiemTL.Text = diem.DiemThaoLuan.ToString();
                txtDiemGK.Text = diem.DiemGK.ToString();
                txtDiemCK.Text = diem.DiemThi.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void btnTimSV_Click(object sender, RoutedEventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            if (!string.IsNullOrEmpty(maSV))
            {
                var sv = qlsv.SinhViens.FirstOrDefault(x => x.MaSV == maSV);
                if (sv != null)
                {
                    txtTenSV.Text = sv.HoTen;
                }
                else
                {
                    txtTenSV.Text = "";
                    MessageBox.Show("Khong Tìm thấy Sinh viên");
                }

            }
        }

        private void btnTimMH_Click(object sender, RoutedEventArgs e)
        {
            string maMH = txtMaMonHoc.Text.Trim();
            if (!string.IsNullOrEmpty(maMH))
            {
                var mh = qlsv.MonHocs.FirstOrDefault(x => x.MaMH == maMH);
                if (mh != null)
                {
                    txtTenMonHoc.Text = mh.TenMH;
                }
                else
                {
                    txtTenMonHoc.Text = "";
                    MessageBox.Show("Khong tìm thay môn học");
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            var diemChon = listDiem.SelectedItem;
            if (diemChon == null)
            {
                MessageBox.Show("Hay chon bang diem can xoa");

            }
            else
            {
                string maSV = diemChon.GetType().GetProperty("MaSV").GetValue(diemChon, null).ToString();
                string maMH = diemChon.GetType().GetProperty("MaMH").GetValue(diemChon, null).ToString();

                Diem diem = qlsv.Diems.FirstOrDefault(d => d.MaMH == maMH && d.MaSV == maSV);
                qlsv.Diems.Remove(diem);
                qlsv.SaveChanges();
                LoadDIem();
            }


        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string maSV = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(maSV))
            {
                var ListDiem = qlsv.Diems.Where(d => d.MaSV == maSV).Select(d => new
                {
                    d.MaSV,
                    TenSV = d.SinhVien.HoTen,
                    d.DiemCC,
                    d.DiemThaoLuan,
                    d.DiemGK,
                    d.DiemThi,
                    d.DiemTB
                })
                    .ToList();
                listDiem.ItemsSource = ListDiem;

                if (ListDiem.Count == 0)
                {
                    MessageBox.Show("Khong tim thay diem cua sinh vien nay");
                }


            }
            else
            {

                LoadDIem();
            }

        }


    }
}


