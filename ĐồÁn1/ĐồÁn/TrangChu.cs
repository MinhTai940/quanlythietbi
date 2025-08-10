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
    public partial class TrangChu : Form
    { 
         public class NguoiDungHienTai
        {
            public static string TenDangNhap { get; set; }
            public static string VaiTro { get; set; }
        }

        private string tenDangNhap;
        private string chucVu;
        public TrangChu(string tenDangNhap, string chucVu)
        {
            InitializeComponent();

            this.tenDangNhap = tenDangNhap;
            this.chucVu = chucVu;

        }
        private void TrangChu_Load(object sender, EventArgs e)
        {

            lblTen.Text =tenDangNhap;
            lblChucVu.Text =chucVu;
        }
        // Hàm cho phép form con gọi để hiển thị 1 form bất kỳ lên panel_Body
        public void HienThiFormTrongPanel(Form frm)
        {
            panel_Body.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(frm);
            frm.Show();
        }

        private void btnMonAn_Click(object sender, EventArgs e)
        {
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            // Tạo mới form MonAn
            NhanVien frm = new NhanVien();

            // Xoá nội dung cũ trong panel_Body
            panel_Body.Controls.Clear();

            // Thiết lập để nhúng form chứ không mở cửa sổ mới
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Thêm form vào panel_Body
            panel_Body.Controls.Add(frm);

            // Hiển thị form
            frm.Show();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Tạo mới form MonAn
             DanhSachBan frm = new DanhSachBan();

            // Xoá nội dung cũ trong panel_Body
            panel_Body.Controls.Clear();

            // Thiết lập để nhúng form chứ không mở cửa sổ mới
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Thêm form vào panel_Body
            panel_Body.Controls.Add(frm);

            // Hiển thị form
            frm.Show();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // Mở form DanhSachBan khi khởi động
            ThongKe dsBan = new ThongKe();
            dsBan.TopLevel = false;
            dsBan.FormBorderStyle = FormBorderStyle.None;
            dsBan.Dock = DockStyle.Fill;

            panel_Body.Controls.Clear();
            panel_Body.Controls.Add(dsBan);
            dsBan.Show();
            if (NguoiDungHienTai.VaiTro != "Admin")
            {
                MessageBox.Show("Bạn không có quyền xem thống kê!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng không cho mở thống kê
            }

            // Nếu là Admin mới cho mở form thống kê
            ThongKe frm = new ThongKe();
            frm.ShowDialog();
        }
        
    }
}
