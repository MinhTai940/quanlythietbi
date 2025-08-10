using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ĐồÁn
{
    public partial class DonHang : Form
    {
       
        public DonHang()
        {
            InitializeComponent();
           
        }

        public void AddMonAn(MonAnDTO mon)
        {
            int soLuong = 1;
            decimal thanhTien = mon.DonGia * soLuong;

            dgvDonHang.Rows.Add(mon.TenMonAn, soLuong, mon.DonGia, thanhTien);
            CapNhatTongTien();
        }

        private void CapNhatTongTien()
        {
            decimal tong = 0;
            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                if (row.Cells[3].Value != null)
                    tong += Convert.ToDecimal(row.Cells[3].Value);
            }
            txtTongTien.Text = tong.ToString("N0");
        }
        private void LoadDon()
        {
           
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public void LoadDanhSachMonAn(DataTable data)
        {
            dgvDonHang.DataSource = data;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            this.Hide();
            MonAn frm = new MonAn();
            frm.ShowDialog();
            LoadDon(); // cập nhật lại sau khi thêm
            this.Show();
        }

       
    }
}
