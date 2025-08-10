using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ĐồÁn
{
    public class Connect
    {
        // 🧠 Chuỗi kết nối SQL – điều chỉnh theo cấu hình của bạn
        private readonly string connectionString = "Data Source=DESKTOP-NE4CJ0L\\SQLEXPRESS;Initial Catalog=QuanLyBanDoAn;Integrated Security=True";


        private SqlConnection conn;

        // 🔹 Hàm mở kết nối
        public void Open()
        {
            if (conn == null)
                conn = new SqlConnection(connectionString);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        // 🔹 Hàm đóng kết nối
        public void Close()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        // 🔹 Trả về đối tượng SqlConnection đang dùng
        public SqlConnection GetConnection()
        {
            return conn;
        }

        // 🔹 Thực thi câu lệnh SELECT → trả về DataTable
        public DataTable ExecuteQuery(string sql)
        {
            Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Close();
            return dt;
        }

        // 🔹 Thực thi INSERT, UPDATE, DELETE → trả về số dòng ảnh hưởng
        public int ExecuteNonQuery(string sql)
        {
            Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rows = cmd.ExecuteNonQuery();
            Close();
            return rows;
        }

        // 🔹 Thực thi câu lệnh trả về 1 giá trị (COUNT, SUM, v.v.)
        public object ExecuteScalar(string sql)
        {
            Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            Close();
            return result;
        }
    }
}
