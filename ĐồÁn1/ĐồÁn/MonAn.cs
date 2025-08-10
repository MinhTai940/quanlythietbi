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

namespace ĐồÁn
{
    public partial class MonAn : Form
    {
        string connectionString = "Data Source=DESKTOP-NE4CJ0L\\SQLEXPRESS;Initial Catalog=QuanLyBanDoAn;Integrated Security = True";


        public MonAn()
        {
            InitializeComponent();
            HienThiDanhSachMonAn();
        }
        private void HienThiDanhSachMonAn()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM MonAn";


                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        dgvMonAn.DataSource = dataSet.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin món ăn: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
           
        }


        private void btnGuiDonHang_Click(object sender, EventArgs e)
        {
            DonHang frmDonHang = Application.OpenForms["DonHang"] as DonHang;

            if (frmDonHang == null)
            {
                frmDonHang = new DonHang();
                frmDonHang.Show();
            }

            // Chuyển toàn bộ dữ liệu của dgvMonAn sang DonHang
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn col in dgvMonAn.Columns)
            {
                dt.Columns.Add(col.Name, col.ValueType ?? typeof(string));
            }

            foreach (DataGridViewRow row in dgvMonAn.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow newRow = dt.NewRow();
                    foreach (DataGridViewColumn col in dgvMonAn.Columns)
                    {
                        newRow[col.Name] = row.Cells[col.Name].Value ?? DBNull.Value;
                    }
                    dt.Rows.Add(newRow);
                }
            }

            frmDonHang.LoadDanhSachMonAn(dt);
        }
    }
}
