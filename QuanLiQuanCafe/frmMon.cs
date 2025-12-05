using Guna.UI2.WinForms;
using QuanLiQuanCafe.BUS;
using QuanLiQuanCafe.DAL;
using QuanLiQuanCafe.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmMon : Form
    {
        private readonly string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";
        private MonBUS monBUS;
        private HoaDonBUS hoaDonBUS;

        private List<int> danhSachMonDaChon = new List<int>();
        private int? hoaDonDangChonId = null; // hóa đơn đang chọn

        public event Action<int> OnMonDaDuocThem;

        public frmMon()
        {
            InitializeComponent();
            this.Load += frmMon_Load;

            cboHoaDon.SelectedIndexChanged += cboHoaDon_SelectedIndexChanged;
            btnThemMonVaoDon.Click += btnThemMonVaoDon_Click;
            btnTimMon.Click += btnTimMon_Click;
            btnThanhToan.Click += btnThanhToan_Click;
            btnThemHoaDonMoi.Click += btnThemHoaDonMoi_Click;
            btnXoaHoaDon.Click += btnXoaHoaDon_Click;

            // Style dgvMon
            DataGridViewHelper.SetBrownTheme(dgvMon);
        }

        public frmMon(int hoaDonId) : this()
        {
            hoaDonDangChonId = hoaDonId;
        }

        private void frmMon_Load(object sender, EventArgs e)
        {
            monBUS = new MonBUS(connStr);
            hoaDonBUS = new HoaDonBUS();

            LoadCboHoaDon();
            LoadLoaiMon();
            LoadMon(); // load tất cả món mặc định
        }

        #region Hóa đơn

        private void LoadCboHoaDon()
        {
            DataTable dt = hoaDonBUS.GetHoaDonChuaThanhToan();

            // Thêm row “Chọn hóa đơn”
            DataRow allRow = dt.NewRow();
            allRow["Id"] = 0;
            allRow["NgayTao"] = DBNull.Value;
            allRow["TongTien"] = 0;
            allRow["TrangThai"] = "";
            dt.Rows.InsertAt(allRow, 0);

            cboHoaDon.DisplayMember = "NgayTao";
            cboHoaDon.ValueMember = "Id";
            cboHoaDon.DataSource = dt;

            cboHoaDon.Format += (s, e) =>
            {
                if (e.ListItem is DataRowView drv)
                {
                    if (drv["Id"].ToString() == "0")
                        e.Value = "Chọn hóa đơn";
                    else
                        e.Value = Convert.ToDateTime(drv["NgayTao"]).ToString("dd/MM/yyyy");
                }
            };

            cboHoaDon.SelectedValue = 0;
        }

        private void cboHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(cboHoaDon.SelectedValue);
            hoaDonDangChonId = val > 0 ? val : (int?)null;
            UpdateButtonStatus();
            LoadChiTietHoaDon();
        }

        private void LoadChiTietHoaDon()
        {
            if (!hoaDonDangChonId.HasValue)
            {
                dgvMon.DataSource = null;
                txtTongTien.Text = "0";
                txtSoLuongMon.Text = "0";
                return;
            }

            try
            {
                DataTable dt = hoaDonBUS.GetChiTietHoaDon(hoaDonDangChonId.Value);

                var dtShow = new DataTable();
                dtShow.Columns.Add("Tên món");
                dtShow.Columns.Add("Giá", typeof(decimal));

                decimal tongTien = 0;
                int soLuongMon = 0;

                foreach (DataRow row in dt.Rows)
                {
                    string tenMon = row["TenMon"].ToString();
                    decimal gia = Convert.ToDecimal(row["Gia"]);
                    int sl = Convert.ToInt32(row["SoLuong"]);

                    dtShow.Rows.Add(tenMon, gia);
                    tongTien += gia * sl;
                    soLuongMon += sl;
                }

                dgvMon.DataSource = dtShow;
                txtTongTien.Text = tongTien.ToString("N0");
                txtSoLuongMon.Text = soLuongMon.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            try
            {
                int nhanVienId = 1;
                int newId = hoaDonBUS.ThemHoaDonMoi(nhanVienId);
                MessageBox.Show($"Đã tạo hóa đơn mới #{newId}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCboHoaDon();

                foreach (var item in cboHoaDon.Items)
                {
                    if (item is DataRowView drv && Convert.ToInt32(drv["Id"]) == newId)
                    {
                        cboHoaDon.SelectedItem = drv;
                        hoaDonDangChonId = newId;
                        UpdateButtonStatus();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hóa đơn mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (!hoaDonDangChonId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn 1 hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThai = ((DataRowView)cboHoaDon.SelectedItem)["TrangThai"].ToString().Trim();
            if (trangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không thể xóa hóa đơn đã thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn #{hoaDonDangChonId}?\nTất cả món sẽ bị xóa!",
                                          "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    hoaDonBUS.XoaHoaDon(hoaDonDangChonId.Value);
                    MessageBox.Show($"Đã xóa hóa đơn #{hoaDonDangChonId}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadCboHoaDon();
                    hoaDonDangChonId = null;
                    UpdateButtonStatus();
                    dgvMon.DataSource = null;
                    txtTongTien.Text = "0";
                    txtSoLuongMon.Text = "0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (!hoaDonDangChonId.HasValue) return;

            try
            {
                string sql = $"UPDATE HoaDon SET TrangThai=N'Đã thanh toán' WHERE Id={hoaDonDangChonId.Value}";
                DataAccess.ExecuteNonQuery(sql);

                MessageBox.Show($"Hóa đơn #{hoaDonDangChonId.Value} đã thanh toán!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCboHoaDon();
                dgvMon.DataSource = null;
                txtTongTien.Text = "0";
                txtSoLuongMon.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Món

        private void LoadMon(string loai = "Tất cả")
        {
            flpMon.Controls.Clear();
            DataTable dtMon = monBUS.GetMon("", loai);

            foreach (DataRow row in dtMon.Rows)
            {
                var card = new MonCard(
                    Convert.ToInt32(row["Id"]),
                    row["TenMon"].ToString(),
                    Convert.ToDecimal(row["Gia"]),
                    row["Loai"]?.ToString() ?? ""
                );

                card.SetSelected(danhSachMonDaChon.Contains(card.Id));
                card.OnSelect += (c) => ToggleMonCard(c);
                flpMon.Controls.Add(card);
            }
        }

        private void ToggleMonCard(MonCard card)
        {
            if (danhSachMonDaChon.Contains(card.Id))
            {
                danhSachMonDaChon.Remove(card.Id);
                card.SetSelected(false);
            }
            else
            {
                danhSachMonDaChon.Add(card.Id);
                card.SetSelected(true);
            }

            UpdateButtonStatus();
        }

        private void btnThemMonVaoDon_Click(object sender, EventArgs e)
        {
            if (!hoaDonDangChonId.HasValue || danhSachMonDaChon.Count == 0) return;

            try
            {
                monBUS.ThemMonVaoHoaDon(hoaDonDangChonId.Value, danhSachMonDaChon);
                OnMonDaDuocThem?.Invoke(hoaDonDangChonId.Value);
                danhSachMonDaChon.Clear();
                LoadMon();
                LoadChiTietHoaDon();
                MessageBox.Show("Đã thêm món vào hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimMon_Click(object sender, EventArgs e)
        {
            LoadMon(txtTimKiem.Text.Trim());
        }

        #endregion

        private void UpdateButtonStatus()
        {
            btnThemMonVaoDon.Enabled = danhSachMonDaChon.Count > 0 && hoaDonDangChonId.HasValue;
            btnThanhToan.Enabled = hoaDonDangChonId.HasValue;
        }

        #region Loại món

        private void LoadLoaiMon()
        {
            DataTable dtLoai = monBUS.GetLoaiMon();

            LoaiMonHelper.LoadLoaiMon(flpLoaiMon, dtLoai, (loai) =>
            {
                LoadMon(loai);
            });
        }
        #endregion
    }
}
