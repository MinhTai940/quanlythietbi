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
using ĐồÁn;

namespace ĐồÁn
{
    public partial class NhanVien : Form
    {
        string connectionString = "Data Source=DESKTOP-NE4CJ0L\\SQLEXPRESS;Initial Catalog=QuanLyBanDoAn;Integrated Security = True";

        public NhanVien()
        {
            InitializeComponent();
            HienThiDanhSachNhanVien();
        }

        private void HienThiDanhSachNhanVien()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM NhanVien";


                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        dgvNhanVien.DataSource = dataSet.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin nhân viên: " + ex.Message);
            }
        }




        private void NhanVien_Load(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maNhanVien = txtMaNhanVien.Text.Trim();
                string tenNhanVien = txtTenNV.Text.Trim();
                string gioiTinh = cboGioiTinh.SelectedItem?.ToString();
                DateTime ngaySinh = dtpNgaySinh.Value;
               string chucVu = cboChucVu.SelectedItem?.ToString(); 
                string sdt = txtSDT.Text.Trim();
                


                if (string.IsNullOrEmpty(tenNhanVien) || string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại! Vui lòng nhập mã khác.");
                            return;
                        }
                    }

                    string query = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, GioiTinh, ChucVu, SoDienThoai, NgaySinh) " +
                                   "VALUES (@MaNhanVien, @TenNhanVien, @GioiTinh, @ChucVu, @SoDienThoai, @NgaySinh)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        command.Parameters.AddWithValue("@TenNhanVien", tenNhanVien);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        command.Parameters.AddWithValue("@ChucVu", chucVu);
                        command.Parameters.AddWithValue("@SoDienThoai", sdt);
                        command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSachNhanVien();
                    ClearFormNhanVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFormNhanVien()
        {
            txtMaNhanVien.Text = "";
            txtTenNV.Text = "";
            txtSDT.Text = "";
            cboChucVu.SelectedIndex = -1;
            cboGioiTinh.SelectedIndex = -1;

        }
    }
}

