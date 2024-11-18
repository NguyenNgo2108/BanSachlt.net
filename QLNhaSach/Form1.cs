using System.Data;
using System.Data.SqlClient;

namespace QLNhaSach
{
    public partial class Form1 : Form
    {
        private string connectionString = KetNoi.chuoiKN;
        public Form1()
        {
            InitializeComponent();
        }

        private void tbTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = tbTaiKhoan.Text;
            string matKhau = tbMatKhau.Text;

            if (DangNhap(taiKhoan, matKhau))
            {
                MessageBox.Show("Đăng nhập thành công!");
                HeThong HeThongForm = new HeThong();
                this.Hide();
                HeThongForm.Show(); 
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại! Tài khoản hoặc mật khẩu không đúng.");
            }
        }

        private bool DangNhap(string taiKhoan, string matKhau)
        {
            using (SqlConnection conn = new SqlConnection(KetNoi.chuoiKN))
            {
                string query = "SELECT COUNT(*) FROM DangNhap WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                try
                {
                    conn.Open();
                    int result = (int)cmd.ExecuteScalar();  
                    return result > 0; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
