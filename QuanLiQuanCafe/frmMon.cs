using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmMon : Form
    {
        public frmMon()
        {
            InitializeComponent();
        }

        private void frmMon_Load(object sender, EventArgs e)
        {
            dgvMon.Columns.Clear();
            dgvMon.AutoGenerateColumns = true;
            dgvMon.DataSource = DataManager.LoadMon();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Mon m = new Mon()
                {
                    TenMon = txtTenMon.Text,
                    Gia = decimal.Parse(txtGia.Text),
                    Loai = txtLoai.Text
                };
                DataManager.ThemMon(m);
                dgvMon.DataSource = DataManager.LoadMon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvMon.CurrentRow == null) return;
            Mon m = new Mon()
            {
                Id = (int)dgvMon.CurrentRow.Cells["Id"].Value,
                TenMon = txtTenMon.Text,
                Gia = decimal.Parse(txtGia.Text),
                Loai = txtLoai.Text
            };
            DataManager.SuaMon(m);
            dgvMon.DataSource = DataManager.LoadMon();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvMon.CurrentRow == null) return;
            int id = (int)dgvMon.CurrentRow.Cells["Id"].Value;
            DataManager.XoaMon(id);
            dgvMon.DataSource = DataManager.LoadMon();
        }

        private void dgvMon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMon.CurrentRow != null)
            {
                txtTenMon.Text = dgvMon.CurrentRow.Cells["TenMon"].Value.ToString();
                txtGia.Text = dgvMon.CurrentRow.Cells["Gia"].Value.ToString();
                txtLoai.Text = dgvMon.CurrentRow.Cells["Loai"].Value.ToString();
            }
        }
    }
}
