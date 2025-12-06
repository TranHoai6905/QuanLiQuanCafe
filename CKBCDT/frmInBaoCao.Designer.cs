using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CKBCDT
{
    partial class frmInBaoCao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblTenQuan = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.lblTongDT = new System.Windows.Forms.Label();
            this.lblTongHD = new System.Windows.Forms.Label();
            this.lblTongCa = new System.Windows.Forms.Label();
            this.dgvCa = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoanhThu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartIn = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelCaSang = new System.Windows.Forms.Panel();
            this.lblTieuDeCaSang = new System.Windows.Forms.Label();
            this.lblDTSang = new System.Windows.Forms.Label();
            this.lblHDSang = new System.Windows.Forms.Label();
            this.lblMonSang = new System.Windows.Forms.Label();
            this.panelCaToi = new System.Windows.Forms.Panel();
            this.lblTieuDeCaToi = new System.Windows.Forms.Label();
            this.lblDTToi = new System.Windows.Forms.Label();
            this.lblHDToi = new System.Windows.Forms.Label();
            this.lblMonToi = new System.Windows.Forms.Label();
            this.lblNhanXet = new System.Windows.Forms.Label();
            this.lblNguoiLap = new System.Windows.Forms.Label();
            this.lblQuanLy = new System.Windows.Forms.Label();
            this.btnXuatPDF = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartIn)).BeginInit();
            this.panelCaSang.SuspendLayout();
            this.panelCaToi.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.lblTenQuan);
            this.panelMain.Controls.Add(this.lblTieuDe);
            this.panelMain.Controls.Add(this.lblThoiGian);
            this.panelMain.Controls.Add(this.lblTongDT);
            this.panelMain.Controls.Add(this.lblTongHD);
            this.panelMain.Controls.Add(this.lblTongCa);
            this.panelMain.Controls.Add(this.dgvCa);
            this.panelMain.Controls.Add(this.chartIn);
            this.panelMain.Controls.Add(this.panelCaSang);
            this.panelMain.Controls.Add(this.panelCaToi);
            this.panelMain.Controls.Add(this.lblNhanXet);
            this.panelMain.Controls.Add(this.lblNguoiLap);
            this.panelMain.Controls.Add(this.lblQuanLy);
            this.panelMain.Controls.Add(this.btnXuatPDF);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(958, 937);
            this.panelMain.TabIndex = 0;
            
            // 
            // lblTenQuan
            // 
            this.lblTenQuan.AutoSize = true;
            this.lblTenQuan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.lblTenQuan.ForeColor = System.Drawing.Color.Maroon;
            this.lblTenQuan.Location = new System.Drawing.Point(420, 9);
            this.lblTenQuan.Name = "lblTenQuan";
            this.lblTenQuan.Size = new System.Drawing.Size(113, 25);
            this.lblTenQuan.TabIndex = 0;
            this.lblTenQuan.Text = "MIU COFFEE";
            this.lblTenQuan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(280, 40);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(403, 30);
            this.lblTieuDe.TabIndex = 1;
            this.lblTieuDe.Text = "BÁO CÁO DOANH THU THEO CA BÁN";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.AutoSize = true;
            this.lblThoiGian.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblThoiGian.Location = new System.Drawing.Point(360, 75);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(248, 20);
            this.lblThoiGian.TabIndex = 2;
            this.lblThoiGian.Text = "Từ ngày 01/11/2025 đến 30/11/2025";
            this.lblThoiGian.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTongDT
            // 
            this.lblTongDT.AutoSize = true;
            this.lblTongDT.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongDT.Location = new System.Drawing.Point(50, 105);
            this.lblTongDT.Name = "lblTongDT";
            this.lblTongDT.Size = new System.Drawing.Size(325, 23);
            this.lblTongDT.TabIndex = 3;
            this.lblTongDT.Text = "Tổng doanh thu toàn kỳ: 15.000.000 VND";
            // 
            // lblTongHD
            // 
            this.lblTongHD.AutoSize = true;
            this.lblTongHD.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongHD.Location = new System.Drawing.Point(50, 130);
            this.lblTongHD.Name = "lblTongHD";
            this.lblTongHD.Size = new System.Drawing.Size(245, 23);
            this.lblTongHD.TabIndex = 4;
            this.lblTongHD.Text = "Tổng số hóa đơn: 200 hóa đơn";
            // 
            // lblTongCa
            // 
            this.lblTongCa.AutoSize = true;
            this.lblTongCa.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongCa.Location = new System.Drawing.Point(50, 155);
            this.lblTongCa.Name = "lblTongCa";
            this.lblTongCa.Size = new System.Drawing.Size(175, 23);
            this.lblTongCa.TabIndex = 5;
            this.lblTongCa.Text = "Tổng số ca làm: 60 ca";
            // 
            // dgvCa
            // 
            this.dgvCa.AllowUserToAddRows = false;
            this.dgvCa.AllowUserToDeleteRows = false;
            this.dgvCa.AllowUserToResizeColumns = false;
            this.dgvCa.AllowUserToResizeRows = false;
            this.dgvCa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCa.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCa.ColumnHeadersHeight = 35;
            this.dgvCa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.Ca,
            this.ThoiGian,
            this.SoHD,
            this.DoanhThu});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCa.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCa.EnableHeadersVisualStyles = false;
            this.dgvCa.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.dgvCa.Location = new System.Drawing.Point(50, 191);
            this.dgvCa.Name = "dgvCa";
            this.dgvCa.ReadOnly = true;
            this.dgvCa.RowHeadersVisible = false;
            this.dgvCa.RowHeadersWidth = 51;
            this.dgvCa.RowTemplate.Height = 30;
            this.dgvCa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCa.Size = new System.Drawing.Size(861, 122);
            this.dgvCa.TabIndex = 6;
            // 
            // STT
            // 
            this.STT.FillWeight = 10F;
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            // 
            // Ca
            // 
            this.Ca.FillWeight = 15F;
            this.Ca.MinimumWidth = 6;
            this.Ca.Name = "Ca";
            this.Ca.ReadOnly = true;
            // 
            // ThoiGian
            // 
            this.ThoiGian.FillWeight = 25F;
            this.ThoiGian.MinimumWidth = 6;
            this.ThoiGian.Name = "ThoiGian";
            this.ThoiGian.ReadOnly = true;
            // 
            // SoHD
            // 
            this.SoHD.FillWeight = 20F;
            this.SoHD.MinimumWidth = 6;
            this.SoHD.Name = "SoHD";
            this.SoHD.ReadOnly = true;
            // 
            // DoanhThu
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.DoanhThu.DefaultCellStyle = dataGridViewCellStyle2;
            this.DoanhThu.FillWeight = 30F;
            this.DoanhThu.MinimumWidth = 6;
            this.DoanhThu.Name = "DoanhThu";
            this.DoanhThu.ReadOnly = true;
            // 
            // chartIn
            // 
            chartArea1.AxisX.Title = "Đơn";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 8F);
            chartArea1.AxisY.Interval = 2D;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chartIn.ChartAreas.Add(chartArea1);
            this.chartIn.Location = new System.Drawing.Point(80, 322);
            this.chartIn.Name = "chartIn";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.IsValueShownAsLabel = true;
            series1.Name = "DoanhThu";
            this.chartIn.Series.Add(series1);
            this.chartIn.Size = new System.Drawing.Size(793, 230);
            this.chartIn.TabIndex = 7;
            title1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "Biểu đồ doanh thu theo ca";
            this.chartIn.Titles.Add(title1);
            // 
            // panelCaSang
            // 
            this.panelCaSang.BackColor = System.Drawing.Color.White;
            this.panelCaSang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCaSang.Controls.Add(this.lblTieuDeCaSang);
            this.panelCaSang.Controls.Add(this.lblDTSang);
            this.panelCaSang.Controls.Add(this.lblHDSang);
            this.panelCaSang.Controls.Add(this.lblMonSang);
            this.panelCaSang.Location = new System.Drawing.Point(158, 558);
            this.panelCaSang.Name = "panelCaSang";
            this.panelCaSang.Size = new System.Drawing.Size(320, 105);
            this.panelCaSang.TabIndex = 8;
            // 
            // lblTieuDeCaSang
            // 
            this.lblTieuDeCaSang.AutoSize = true;
            this.lblTieuDeCaSang.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeCaSang.Location = new System.Drawing.Point(10, 8);
            this.lblTieuDeCaSang.Name = "lblTieuDeCaSang";
            this.lblTieuDeCaSang.Size = new System.Drawing.Size(81, 25);
            this.lblTieuDeCaSang.TabIndex = 0;
            this.lblTieuDeCaSang.Text = "Ca sáng";
            // 
            // lblDTSang
            // 
            this.lblDTSang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDTSang.Location = new System.Drawing.Point(10, 33);
            this.lblDTSang.Name = "lblDTSang";
            this.lblDTSang.Size = new System.Drawing.Size(293, 22);
            this.lblDTSang.TabIndex = 1;
            // 
            // lblHDSang
            // 
            this.lblHDSang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHDSang.Location = new System.Drawing.Point(10, 55);
            this.lblHDSang.Name = "lblHDSang";
            this.lblHDSang.Size = new System.Drawing.Size(293, 20);
            this.lblHDSang.TabIndex = 2;
            // 
            // lblMonSang
            // 
            this.lblMonSang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMonSang.Location = new System.Drawing.Point(10, 75);
            this.lblMonSang.Name = "lblMonSang";
            this.lblMonSang.Size = new System.Drawing.Size(305, 20);
            this.lblMonSang.TabIndex = 3;
            // 
            // panelCaToi
            // 
            this.panelCaToi.BackColor = System.Drawing.Color.White;
            this.panelCaToi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCaToi.Controls.Add(this.lblTieuDeCaToi);
            this.panelCaToi.Controls.Add(this.lblDTToi);
            this.panelCaToi.Controls.Add(this.lblHDToi);
            this.panelCaToi.Controls.Add(this.lblMonToi);
            this.panelCaToi.Location = new System.Drawing.Point(517, 558);
            this.panelCaToi.Name = "panelCaToi";
            this.panelCaToi.Size = new System.Drawing.Size(320, 105);
            this.panelCaToi.TabIndex = 9;
            // 
            // lblTieuDeCaToi
            // 
            this.lblTieuDeCaToi.AutoSize = true;
            this.lblTieuDeCaToi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeCaToi.Location = new System.Drawing.Point(10, 8);
            this.lblTieuDeCaToi.Name = "lblTieuDeCaToi";
            this.lblTieuDeCaToi.Size = new System.Drawing.Size(63, 25);
            this.lblTieuDeCaToi.TabIndex = 0;
            this.lblTieuDeCaToi.Text = "Ca tối";
            // 
            // lblDTToi
            // 
            this.lblDTToi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDTToi.Location = new System.Drawing.Point(10, 35);
            this.lblDTToi.Name = "lblDTToi";
            this.lblDTToi.Size = new System.Drawing.Size(305, 20);
            this.lblDTToi.TabIndex = 1;
            // 
            // lblHDToi
            // 
            this.lblHDToi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHDToi.Location = new System.Drawing.Point(10, 55);
            this.lblHDToi.Name = "lblHDToi";
            this.lblHDToi.Size = new System.Drawing.Size(291, 20);
            this.lblHDToi.TabIndex = 2;
            // 
            // lblMonToi
            // 
            this.lblMonToi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMonToi.Location = new System.Drawing.Point(10, 75);
            this.lblMonToi.Name = "lblMonToi";
            this.lblMonToi.Size = new System.Drawing.Size(291, 20);
            this.lblMonToi.TabIndex = 3;
            // 
            // lblNhanXet
            // 
            this.lblNhanXet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNhanXet.Location = new System.Drawing.Point(154, 675);
            this.lblNhanXet.Name = "lblNhanXet";
            this.lblNhanXet.Size = new System.Drawing.Size(792, 70);
            this.lblNhanXet.TabIndex = 10;
            this.lblNhanXet.Text = "Nhận xét: [Dữ liệu động]";
            // 
            // lblNguoiLap
            // 
            this.lblNguoiLap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblNguoiLap.Location = new System.Drawing.Point(169, 764);
            this.lblNguoiLap.Name = "lblNguoiLap";
            this.lblNguoiLap.Size = new System.Drawing.Size(150, 58);
            this.lblNguoiLap.TabIndex = 11;
            this.lblNguoiLap.Text = "Người lập báo cáo\n(Ký và ghi rõ họ tên)";
            this.lblNguoiLap.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblQuanLy
            // 
            this.lblQuanLy.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblQuanLy.Location = new System.Drawing.Point(699, 764);
            this.lblQuanLy.Name = "lblQuanLy";
            this.lblQuanLy.Size = new System.Drawing.Size(150, 58);
            this.lblQuanLy.TabIndex = 12;
            this.lblQuanLy.Text = "Quản lý cửa hàng\n(Ký và ghi rõ họ tên)";
            this.lblQuanLy.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnXuatPDF
            // 
            this.btnXuatPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnXuatPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatPDF.FlatAppearance.BorderSize = 0;
            this.btnXuatPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatPDF.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXuatPDF.ForeColor = System.Drawing.Color.White;
            this.btnXuatPDF.Location = new System.Drawing.Point(384, 801);
            this.btnXuatPDF.Name = "btnXuatPDF";
            this.btnXuatPDF.Size = new System.Drawing.Size(170, 40);
            this.btnXuatPDF.TabIndex = 13;
            this.btnXuatPDF.Text = "📄 Xuất PDF";
            this.btnXuatPDF.UseVisualStyleBackColor = false;
            // 
            // frmInBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 937);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmInBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO DOANH THU THEO CA BÁN - MTU COFFEE";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartIn)).EndInit();
            this.panelCaSang.ResumeLayout(false);
            this.panelCaSang.PerformLayout();
            this.panelCaToi.ResumeLayout(false);
            this.panelCaToi.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelMain;
        private Label lblTenQuan;
        private Label lblTieuDe;
        private Label lblThoiGian;
        private Label lblTongDT;
        private Label lblTongHD;
        private Label lblTongCa;
        private DataGridView dgvCa;

        private Chart chartIn;
        private Panel panelCaSang;
        private Label lblTieuDeCaSang;
        private Label lblDTSang;
        private Label lblHDSang;
        private Label lblMonSang;
        private Panel panelCaToi;
        private Label lblTieuDeCaToi;
        private Label lblDTToi;
        private Label lblHDToi;
        private Label lblMonToi;
        private Label lblNhanXet;
        private Label lblNguoiLap;
        private Label lblQuanLy;
        private Button btnXuatPDF;
        private DataGridViewTextBoxColumn STT;
        private DataGridViewTextBoxColumn Ca;
        private DataGridViewTextBoxColumn ThoiGian;
        private DataGridViewTextBoxColumn SoHD;
        private DataGridViewTextBoxColumn DoanhThu;
    }
}