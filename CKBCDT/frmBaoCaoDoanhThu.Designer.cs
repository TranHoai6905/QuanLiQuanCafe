using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CKBCDT
{
    partial class frmBaoCaoDoanhThu
    {
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnNgay = new System.Windows.Forms.Button();
            this.btnThang = new System.Windows.Forms.Button();
            this.btnNam = new System.Windows.Forms.Button();
            this.btnInBaoCao = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.labelTongHDText = new System.Windows.Forms.Label();
            this.labelTongDTText = new System.Windows.Forms.Label();
            this.lblTongHD = new System.Windows.Forms.Label();
            this.lblTongDT = new System.Windows.Forms.Label();
            this.chartDoanhThuCa = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMonBanChay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTyLe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuCa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonBanChay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTyLe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNgay
            // 
            this.btnNgay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(205)))), ((int)(((byte)(176)))));
            this.btnNgay.FlatAppearance.BorderSize = 0;
            this.btnNgay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNgay.ForeColor = System.Drawing.Color.Black;
            this.btnNgay.Location = new System.Drawing.Point(18, 12);
            this.btnNgay.Name = "btnNgay";
            this.btnNgay.Size = new System.Drawing.Size(92, 36);
            this.btnNgay.TabIndex = 0;
            this.btnNgay.Text = "Ngày";
            this.btnNgay.UseVisualStyleBackColor = false;
            // 
            // btnThang
            // 
            this.btnThang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(205)))), ((int)(((byte)(176)))));
            this.btnThang.FlatAppearance.BorderSize = 0;
            this.btnThang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThang.ForeColor = System.Drawing.Color.Black;
            this.btnThang.Location = new System.Drawing.Point(126, 12);
            this.btnThang.Name = "btnThang";
            this.btnThang.Size = new System.Drawing.Size(92, 36);
            this.btnThang.TabIndex = 2;
            this.btnThang.Text = "Tháng";
            this.btnThang.UseVisualStyleBackColor = false;
            // 
            // btnNam
            // 
            this.btnNam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(205)))), ((int)(((byte)(176)))));
            this.btnNam.FlatAppearance.BorderSize = 0;
            this.btnNam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNam.ForeColor = System.Drawing.Color.Black;
            this.btnNam.Location = new System.Drawing.Point(234, 12);
            this.btnNam.Name = "btnNam";
            this.btnNam.Size = new System.Drawing.Size(92, 36);
            this.btnNam.TabIndex = 3;
            this.btnNam.Text = "Năm";
            this.btnNam.UseVisualStyleBackColor = false;
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.BackColor = System.Drawing.Color.Green;
            this.btnInBaoCao.FlatAppearance.BorderSize = 0;
            this.btnInBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnInBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnInBaoCao.Location = new System.Drawing.Point(723, 12);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(120, 40);
            this.btnInBaoCao.TabIndex = 4;
            this.btnInBaoCao.Text = "In báo cáo";
            this.btnInBaoCao.UseVisualStyleBackColor = false;

            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(864, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(41, 40);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "⟳";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // labelTongHDText
            // 
            this.labelTongHDText.AutoSize = true;
            this.labelTongHDText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelTongHDText.Location = new System.Drawing.Point(18, 58);
            this.labelTongHDText.Name = "labelTongHDText";
            this.labelTongHDText.Size = new System.Drawing.Size(148, 25);
            this.labelTongHDText.TabIndex = 6;
            this.labelTongHDText.Text = "Tổng hóa đơn :";
            // 
            // labelTongDTText
            // 
            this.labelTongDTText.AutoSize = true;
            this.labelTongDTText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelTongDTText.Location = new System.Drawing.Point(260, 58);
            this.labelTongDTText.Name = "labelTongDTText";
            this.labelTongDTText.Size = new System.Drawing.Size(166, 25);
            this.labelTongDTText.TabIndex = 8;
            this.labelTongDTText.Text = "Tổng doanh thu :";
            // 
            // lblTongHD
            // 
            this.lblTongHD.BackColor = System.Drawing.Color.White;
            this.lblTongHD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTongHD.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongHD.Location = new System.Drawing.Point(142, 54);
            this.lblTongHD.Name = "lblTongHD";
            this.lblTongHD.Size = new System.Drawing.Size(96, 28);
            this.lblTongHD.TabIndex = 7;
            this.lblTongHD.Text = "0";
            this.lblTongHD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTongDT
            // 
            this.lblTongDT.BackColor = System.Drawing.Color.White;
            this.lblTongDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTongDT.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDT.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTongDT.Location = new System.Drawing.Point(394, 54);
            this.lblTongDT.Name = "lblTongDT";
            this.lblTongDT.Size = new System.Drawing.Size(160, 28);
            this.lblTongDT.TabIndex = 9;
            this.lblTongDT.Text = "0";
            this.lblTongDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chartDoanhThuCa
            // 
            this.chartDoanhThuCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
            this.chartDoanhThuCa.BorderlineColor = System.Drawing.Color.Black;
            this.chartDoanhThuCa.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.Interval = 1D;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.BorderWidth = 0;
            chartArea1.Name = "CArea";
            this.chartDoanhThuCa.ChartAreas.Add(chartArea1);
            this.chartDoanhThuCa.Location = new System.Drawing.Point(18, 96);
            this.chartDoanhThuCa.Name = "chartDoanhThuCa";
            this.chartDoanhThuCa.Size = new System.Drawing.Size(560, 300);
            this.chartDoanhThuCa.TabIndex = 10;
            title1.BackColor = System.Drawing.Color.Thistle;
            title1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            title1.ForeColor = System.Drawing.Color.SaddleBrown;
            title1.Name = "TitleDoanhThuCa";
            title1.Text = "Biểu đồ doanh thu theo ca";
            this.chartDoanhThuCa.Titles.Add(title1);
            // 
            // chartMonBanChay
            // 
            this.chartMonBanChay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
            chartArea2.AxisX.Interval = 1D;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.Interval = 20D;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.Maximum = 120D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.BorderWidth = 0;
            chartArea2.Name = "CArea2";
            this.chartMonBanChay.ChartAreas.Add(chartArea2);
            this.chartMonBanChay.Location = new System.Drawing.Point(589, 96);
            this.chartMonBanChay.Name = "chartMonBanChay";
            this.chartMonBanChay.Size = new System.Drawing.Size(331, 242);
            this.chartMonBanChay.TabIndex = 11;
            // 
            // chartTyLe
            // 
            this.chartTyLe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
            this.chartTyLe.BorderlineColor = System.Drawing.Color.Black;
            this.chartTyLe.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartTyLe.BorderlineWidth = 2;
            chartArea3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.BorderWidth = 0;
            chartArea3.InnerPlotPosition.Auto = false;
            chartArea3.InnerPlotPosition.Height = 90F;
            chartArea3.InnerPlotPosition.Width = 60F;
            chartArea3.InnerPlotPosition.X = 5F;
            chartArea3.InnerPlotPosition.Y = 5F;
            chartArea3.Name = "CArea3";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 85F;
            chartArea3.Position.Width = 100F;
            chartArea3.Position.Y = 15F;
            this.chartTyLe.ChartAreas.Add(chartArea3);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Font = new System.Drawing.Font("Segoe UI", 9F);
            legend1.IsDockedInsideChartArea = false;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend3";
            this.chartTyLe.Legends.Add(legend1);
            this.chartTyLe.Location = new System.Drawing.Point(589, 344);
            this.chartTyLe.Name = "chartTyLe";
            this.chartTyLe.Size = new System.Drawing.Size(340, 245);
            this.chartTyLe.TabIndex = 12;
            title2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            title2.BorderColor = System.Drawing.Color.Black;
            title2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            title2.ForeColor = System.Drawing.Color.White;
            title2.Name = "TitleTyLe";
            title2.Text = "Tỷ lệ món bán chạy";
            this.chartTyLe.Titles.Add(title2);
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colMaHD,
            this.colBan,
            this.colNV,
            this.colTongTien,
            this.colNgay});
            this.dgvHoaDon.Location = new System.Drawing.Point(18, 408);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.RowHeadersVisible = false;
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.RowTemplate.Height = 28;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(560, 152);
            this.dgvHoaDon.TabIndex = 13;
            // 
            // colSTT
            // 
            this.colSTT.MinimumWidth = 6;
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 125;
            // 
            // colMaHD
            // 
            this.colMaHD.MinimumWidth = 6;
            this.colMaHD.Name = "colMaHD";
            this.colMaHD.Width = 125;
            // 
            // colBan
            // 
            this.colBan.MinimumWidth = 6;
            this.colBan.Name = "colBan";
            this.colBan.Width = 125;
            // 
            // colNV
            // 
            this.colNV.MinimumWidth = 6;
            this.colNV.Name = "colNV";
            this.colNV.Width = 125;
            // 
            // colTongTien
            // 
            this.colTongTien.MinimumWidth = 6;
            this.colTongTien.Name = "colTongTien";
            this.colTongTien.Width = 125;
            // 
            // colNgay
            // 
            this.colNgay.MinimumWidth = 6;
            this.colNgay.Name = "colNgay";
            this.colNgay.Width = 125;
            // 
            // frmBaoCaoDoanhThu
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(941, 601);
            this.Controls.Add(this.btnNgay);
            this.Controls.Add(this.btnThang);
            this.Controls.Add(this.btnNam);
            this.Controls.Add(this.btnInBaoCao);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.labelTongHDText);
            this.Controls.Add(this.lblTongHD);
            this.Controls.Add(this.labelTongDTText);
            this.Controls.Add(this.lblTongDT);
            this.Controls.Add(this.chartDoanhThuCa);
            this.Controls.Add(this.chartMonBanChay);
            this.Controls.Add(this.chartTyLe);
            this.Controls.Add(this.dgvHoaDon);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmBaoCaoDoanhThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo doanh thu";
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuCa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonBanChay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTyLe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Controls (declared here so Designer and your code-behind share same names)
        private System.Windows.Forms.Button btnNgay;

        private System.Windows.Forms.Button btnThang;
        private System.Windows.Forms.Button btnNam;
        private System.Windows.Forms.Button btnInBaoCao;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Label labelTongHDText;
        private System.Windows.Forms.Label labelTongDTText;
        private System.Windows.Forms.Label lblTongHD;
        private System.Windows.Forms.Label lblTongDT;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThuCa;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMonBanChay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTyLe;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colMaHD;
        private DataGridViewTextBoxColumn colBan;
        private DataGridViewTextBoxColumn colNV;
        private DataGridViewTextBoxColumn colTongTien;
        private DataGridViewTextBoxColumn colNgay;
    }
}
