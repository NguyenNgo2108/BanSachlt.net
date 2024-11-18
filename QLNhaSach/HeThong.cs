using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaSach
{
    public partial class HeThong : Form
    {
        public HeThong()
        {
            InitializeComponent();
        }

        private void btnLoaiSach_Click(object sender, EventArgs e)
        {
            LoaiSach LoaiSachForm = new LoaiSach();
            this.Hide();
            LoaiSachForm.Show();
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            Sach SachForm = new Sach();
            this.Hide();
            SachForm.Show();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            HoaDon HoaDonForm = new HoaDon();
            this.Hide();
            HoaDonForm.Show();
        }
    }
}
