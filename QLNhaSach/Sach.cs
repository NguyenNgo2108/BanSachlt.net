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

namespace QLNhaSach
{
    public partial class Sach : Form
    {
        private string connectionString = KetNoi.chuoiKN;
        public Sach()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData(string maLoaiSach = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Sach";

                    if (!string.IsNullOrEmpty(maLoaiSach))
                    {
                        query += " WHERE MaLoaiSach = @MaLoaiSach";
                    }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    if (!string.IsNullOrEmpty(maLoaiSach))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@MaLoaiSach", maLoaiSach);
                    }

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void tbMaSach_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbTenSach_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tbTacGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbGiaBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMoTa_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMaLoaiSach_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSach = tbMaSach.Text;
            string tenSach = tbTenSach.Text;
            string tacGia = tbTacGia.Text;
            string moTa = tbMoTa.Text;
            string maLoaiSach = tbMaLoaiSach.Text;

            if (!float.TryParse(tbGiaBan.Text, out float giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ. Vui lòng nhập lại.");
                return; 
            }

            if (!float.TryParse(tbSoLuong.Text, out float soLuong))
            {
                MessageBox.Show("Số lượng không hợp lệ. Vui lòng nhập lại.");
                return;
            }
            if (IsMaLoaiSachValid(maLoaiSach))
            {
                AddSach(maSach, tenSach, tacGia, giaBan, soLuong, moTa, maLoaiSach);
            }
            else
            {
                MessageBox.Show("Mã loại sách không tồn tại. Vui lòng kiểm tra lại.");
            }
        }

        private void AddSach(string maSach, string tenSach, string tacGia, float giaBan, float soLuong, string moTa, string maLoaiSach)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Sach (MaSach, TenSach, TacGia, GiaBan, SoLuong, MoTa, MaLoaiSach) " +
                                   "VALUES (@MaSach, @TenSach, @TacGia, @GiaBan, @SoLuong, @MoTa, @MaLoaiSach)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSach", maSach);
                    cmd.Parameters.AddWithValue("@TenSach", tenSach);
                    cmd.Parameters.AddWithValue("@TacGia", tacGia);
                    cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@MoTa", moTa);
                    cmd.Parameters.AddWithValue("@MaLoaiSach", maLoaiSach);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm sách thành công.");
                        LoadData(); // Tải lại dữ liệu sau khi thêm sách

                        // Làm sạch các TextBox
                        tbMaSach.Clear();
                        tbTenSach.Clear();
                        tbTacGia.Clear();
                        tbGiaBan.Clear();
                        tbSoLuong.Clear();
                        tbMoTa.Clear();
                        tbMaLoaiSach.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi thêm sách.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sách: " + ex.Message);
                }
            }
        }

        private bool IsMaLoaiSachValid(string maLoaiSach)
        {
            bool isValid = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM LoaiSach WHERE MaLoaiSach = @MaLoaiSach";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLoaiSach", maLoaiSach);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        isValid = true; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra mã loại sách: " + ex.Message);
                }
            }

            return isValid;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSach = tbMaSach.Text;

            // Kiểm tra nếu mã sách rỗng
            if (string.IsNullOrEmpty(maSach))
            {
                MessageBox.Show("Vui lòng nhập mã sách cần xóa.");
                return;
            }

            if (IsMaSachExist(maSach))
            {
                DeleteSach(maSach);
            }
            else
            {
                MessageBox.Show("Mã sách không tồn tại. Vui lòng kiểm tra lại.");
            }
        }

        private bool IsMaSachExist(string maSach)
        {
            bool isExist = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Sach WHERE MaSach = @MaSach";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSach", maSach);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        isExist = true; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra mã sách: " + ex.Message);
                }
            }

            return isExist;
        }

        private void DeleteSach(string maSach)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Sach WHERE MaSach = @MaSach";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSach", maSach);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa sách thành công.");
                        LoadData(); 
                        tbMaSach.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi xóa sách.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sách: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSach = tbMaSach.Text; 
            string tenSach = tbTenSach.Text;
            string tacGia = tbTacGia.Text;
            string moTa = tbMoTa.Text;
            string maLoaiSach = tbMaLoaiSach.Text;

            if (!float.TryParse(tbGiaBan.Text, out float giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ. Vui lòng nhập lại.");
                return; 
            }

            if (!float.TryParse(tbSoLuong.Text, out float soLuong))
            {
                MessageBox.Show("Số lượng không hợp lệ. Vui lòng nhập lại.");
                return; 
            }

            if (IsMaSachExist(maSach))
            {
                UpdateSach(maSach, tenSach, tacGia, giaBan, soLuong, moTa, maLoaiSach);
            }
            else
            {
                MessageBox.Show("Mã sách không tồn tại. Vui lòng kiểm tra lại.");
            }
        }

        private void UpdateSach(string maSach, string tenSach, string tacGia, float giaBan, float soLuong, string moTa, string maLoaiSach)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Sach SET TenSach = @TenSach, TacGia = @TacGia, GiaBan = @GiaBan, SoLuong = @SoLuong, MoTa = @MoTa, MaLoaiSach = @MaLoaiSach " +
                                   "WHERE MaSach = @MaSach";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSach", maSach);
                    cmd.Parameters.AddWithValue("@TenSach", tenSach);
                    cmd.Parameters.AddWithValue("@TacGia", tacGia);
                    cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@MoTa", moTa);
                    cmd.Parameters.AddWithValue("@MaLoaiSach", maLoaiSach);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa thông tin sách thành công.");

                        LoadData();

                        tbMaSach.Clear();
                        tbTenSach.Clear();
                        tbTacGia.Clear();
                        tbGiaBan.Clear();
                        tbSoLuong.Clear();
                        tbMoTa.Clear();
                        tbMaLoaiSach.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi sửa thông tin sách.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa thông tin sách: " + ex.Message);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maLoaiSach = tbMaLoaiSach.Text;

            if (!string.IsNullOrEmpty(maLoaiSach))
            {
                LoadData(maLoaiSach);
            }
            else
            {
                LoadData();
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            HeThong HeThongForm = new HeThong();
            this.Hide();
            HeThongForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                tbMaSach.Text = row.Cells["MaSach"].Value.ToString();
                tbTenSach.Text = row.Cells["TenSach"].Value.ToString();
                tbTacGia.Text = row.Cells["TacGia"].Value.ToString();
                tbGiaBan.Text = row.Cells["GiaBan"].Value.ToString();
                tbSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
                tbMoTa.Text = row.Cells["MoTa"].Value.ToString();
                tbMaLoaiSach.Text = row.Cells["MaLoaiSach"].Value.ToString();
            }
        }
    }
}
