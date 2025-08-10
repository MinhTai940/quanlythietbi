using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ĐồÁn
{
    class PhanQuyen
    {
        public void main()
        {
            string maNv = "nv_a"; // ví dụ mã nhân viên cần kiểm tra và lấy thống kê

            try
            {
                string quyen = LayQuyenNhanVien(maNv);
                if (quyen == "NhanVien")
                {
                    Console.WriteLine("Bạn không có quyền xem thống kê");
                }
                else
                {
                    DataTable dt = ThongKeTheoQuyen(maNv);
                    if (dt != null)
                    {
                        Console.WriteLine("Kết quả thống kê:");
                        foreach (DataRow row in dt.Rows)
                        {
                            Console.WriteLine(string.Join(", ", row.ItemArray));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không có dữ liệu trả về hoặc có lỗi.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }

        static string LayQuyenNhanVien(string maNv)
        {
            string quyen = null;
            string connectionString = "Data Source=SERVERNAME;Initial Catalog=DBNAME;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT quyen FROM NhanVien WHERE maNv = @maNv";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maNv", maNv);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        quyen = result.ToString();
                    }
                }
            }
            return quyen;
        }

        static DataTable ThongKeTheoQuyen(string maNv)
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=SERVERNAME;Initial Catalog=DBNAME;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_ThongKeTheoQuyen", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maNv", maNv);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Lỗi SQL: " + ex.Message);
                    return null;
                }
            }
            return dt;
        }
    }
}
