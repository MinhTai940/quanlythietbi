using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static ĐồÁn.TrangChu;

namespace ĐồÁn
{
    public partial class DangNhap : Form
    {
        string connectionString = "Data Source=DESKTOP-NE4CJ0L\\SQLEXPRESS;Initial Catalog=QuanLyBanDoAn;Integrated Security = True";
        public DangNhap()
        {
            InitializeComponent();

        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
           
           
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtTen.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT nv.TenNhanVien, tk.VaiTro
            FROM TaiKhoan tk
            JOIN NhanVien nv ON tk.MaNhanVien = nv.MaNhanVien
            WHERE tk.TenDangNhap = @user AND tk.MatKhau = @pass AND tk.TrangThai = N'Hoạt động'";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string ten = reader["TenNhanVien"].ToString();
                    string chucVu = reader["VaiTro"].ToString();

                    this.Hide();
                    TrangChu main = new TrangChu(ten, chucVu);
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc bị khóa!", "Lỗi đăng nhập");
                }
            }
        }

      

       
    

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
           

        }
    }
}
