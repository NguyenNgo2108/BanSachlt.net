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
    public partial class HoaDon : Form
    {
        private string connectionString = KetNoi.chuoiKN;
        public HoaDon()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM HoaDon";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
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

        private void tbMaHoaDon_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMaKhachHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayLap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maHoaDon = tbMaHoaDon.Text;
            string maKhachHang = tbMaKhachHang.Text;
            DateTime ngayLap = dtpNgayLap.Value;

            if (IsMaKhachHangExist(maKhachHang))
            {
                AddHoaDon(maHoaDon, maKhachHang, ngayLap);
            }
            else
            {
                MessageBox.Show("Mã khách hàng không tồn tại. Vui lòng kiểm tra lại.");
            }
        }

        private void AddHoaDon(string maHoaDon, string maKhachHang, DateTime ngayLap)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO HoaDon (MaHoaDon, MaKhachHang, NgayLap) " +
                                   "VALUES (@MaHoaDon, @MaKhachHang, @NgayLap)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    cmd.Parameters.AddWithValue("@NgayLap", ngayLap);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm hóa đơn thành công.");
                        LoadData(); // Tải lại dữ liệu sau khi thêm hóa đơn

                        // Làm sạch các TextBox sau khi thêm thành công
                        tbMaHoaDon.Clear();
                        tbMaKhachHang.Clear();
                        dtpNgayLap.Value = DateTime.Now; // Đặt lại giá trị của DateTimePicker về ngày hiện tại
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi thêm hóa đơn.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm hóa đơn: " + ex.Message);
                }
            }
        }

        private bool IsMaKhachHangExist(string maKhachHang)
        {
            bool isExist = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        isExist = true; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra mã khách hàng: " + ex.Message);
                }
            }

            return isExist;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maHoaDon = tbMaHoaDon.Text;
            string maKhachHang = tbMaKhachHang.Text;
            DateTime ngayLap = dtpNgayLap.Value;

            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng chọn mã hóa đơn để sửa.");
                return;
            }

            UpdateHoaDon(maHoaDon, maKhachHang, ngayLap);
        }

        private void UpdateHoaDon(string maHoaDon, string maKhachHang, DateTime ngayLap)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE HoaDon SET MaKhachHang = @MaKhachHang, NgayLap = @NgayLap " +
                                   "WHERE MaHoaDon = @MaHoaDon";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    cmd.Parameters.AddWithValue("@NgayLap", ngayLap);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa hóa đơn thành công.");
                        LoadData(); 

                        tbMaHoaDon.Clear();
                        tbMaKhachHang.Clear();
                        dtpNgayLap.Value = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi sửa hóa đơn.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa hóa đơn: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maHoaDon = tbMaHoaDon.Text;

            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn cần xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteHoaDon(maHoaDon);
            }
        }

        private void DeleteHoaDon(string maHoaDon)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa hóa đơn thành công.");
                        LoadData(); 

                        tbMaHoaDon.Clear();
                        tbMaKhachHang.Clear();
                        dtpNgayLap.Value = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi xóa hóa đơn.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                tbMaHoaDon.Text = row.Cells["MaHoaDon"].Value.ToString();
                tbMaKhachHang.Text = row.Cells["MaKhachHang"].Value.ToString();
                dtpNgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            HeThong HeThongForm = new HeThong();
            this.Hide();
            HeThongForm.Show();
        }
    }
}
