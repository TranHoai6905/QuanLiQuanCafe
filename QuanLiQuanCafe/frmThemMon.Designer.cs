// frmThemMon.Designer.cs
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    partial class frmThemMon
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemMon));
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThemLoai = new Guna.UI2.WinForms.Guna2Button();
            this.cmbLoai = new System.Windows.Forms.ComboBox();
            this.txtGia = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenMon = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSuaMon = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaMon = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemMonMoi = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvMon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.flpLoaiMon = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTimMon = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).BeginInit();
            this.guna2Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.label3.Location = new System.Drawing.Point(28, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 22);
            this.label3.TabIndex = 18;
            this.label3.Text = "Loại";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.label1.Location = new System.Drawing.Point(28, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 22);
            this.label1.TabIndex = 16;
            this.label1.Text = "Tên món";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.label2.Location = new System.Drawing.Point(28, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 22);
            this.label2.TabIndex = 17;
            this.label2.Text = "Giá";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.guna2Panel1.Controls.Add(this.btnThemLoai);
            this.guna2Panel1.Controls.Add(this.cmbLoai);
            this.guna2Panel1.Controls.Add(this.txtGia);
            this.guna2Panel1.Controls.Add(this.txtTenMon);
            this.guna2Panel1.Controls.Add(this.btnSuaMon);
            this.guna2Panel1.Controls.Add(this.btnXoaMon);
            this.guna2Panel1.Controls.Add(this.btnThemMonMoi);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Location = new System.Drawing.Point(51, 130);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(420, 364);
            this.guna2Panel1.TabIndex = 21;
            // 
            // btnThemLoai
            // 
            this.btnThemLoai.BorderRadius = 23;
            this.btnThemLoai.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemLoai.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemLoai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemLoai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemLoai.FillColor = System.Drawing.Color.Teal;
            this.btnThemLoai.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnThemLoai.ForeColor = System.Drawing.Color.White;
            this.btnThemLoai.Image = global::QuanLiQuanCafe.Resource1.notepad;
            this.btnThemLoai.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThemLoai.Location = new System.Drawing.Point(269, 96);
            this.btnThemLoai.Name = "btnThemLoai";
            this.btnThemLoai.Size = new System.Drawing.Size(133, 45);
            this.btnThemLoai.TabIndex = 26;
            this.btnThemLoai.Text = "Thêm loại ";
            this.btnThemLoai.Click += new System.EventHandler(this.btnThemLoai_Click);
            // 
            // cmbLoai
            // 
            this.cmbLoai.FormattingEnabled = true;
            this.cmbLoai.Location = new System.Drawing.Point(114, 109);
            this.cmbLoai.Name = "cmbLoai";
            this.cmbLoai.Size = new System.Drawing.Size(139, 21);
            this.cmbLoai.TabIndex = 25;
            // 
            // txtGia
            // 
            this.txtGia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGia.DefaultText = "";
            this.txtGia.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtGia.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtGia.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGia.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGia.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGia.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGia.Location = new System.Drawing.Point(114, 224);
            this.txtGia.Name = "txtGia";
            this.txtGia.PlaceholderText = "";
            this.txtGia.SelectedText = "";
            this.txtGia.Size = new System.Drawing.Size(288, 36);
            this.txtGia.TabIndex = 24;
            // 
            // txtTenMon
            // 
            this.txtTenMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenMon.DefaultText = "";
            this.txtTenMon.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenMon.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenMon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenMon.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenMon.Location = new System.Drawing.Point(114, 159);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.PlaceholderText = "";
            this.txtTenMon.SelectedText = "";
            this.txtTenMon.Size = new System.Drawing.Size(288, 36);
            this.txtTenMon.TabIndex = 23;
            // 
            // btnSuaMon
            // 
            this.btnSuaMon.BorderRadius = 23;
            this.btnSuaMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSuaMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSuaMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSuaMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSuaMon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnSuaMon.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnSuaMon.ForeColor = System.Drawing.Color.White;
            this.btnSuaMon.Image = global::QuanLiQuanCafe.Resource1.office_material;
            this.btnSuaMon.ImageSize = new System.Drawing.Size(30, 30);
            this.btnSuaMon.Location = new System.Drawing.Point(150, 290);
            this.btnSuaMon.Name = "btnSuaMon";
            this.btnSuaMon.Size = new System.Drawing.Size(129, 45);
            this.btnSuaMon.TabIndex = 22;
            this.btnSuaMon.Text = "Sửa món ";
            this.btnSuaMon.Click += new System.EventHandler(this.btnSuaMon_Click);
            // 
            // btnXoaMon
            // 
            this.btnXoaMon.BorderRadius = 23;
            this.btnXoaMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoaMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoaMon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnXoaMon.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnXoaMon.ForeColor = System.Drawing.Color.White;
            this.btnXoaMon.Image = global::QuanLiQuanCafe.Resource1.cross;
            this.btnXoaMon.ImageSize = new System.Drawing.Size(30, 30);
            this.btnXoaMon.Location = new System.Drawing.Point(285, 290);
            this.btnXoaMon.Name = "btnXoaMon";
            this.btnXoaMon.Size = new System.Drawing.Size(132, 45);
            this.btnXoaMon.TabIndex = 21;
            this.btnXoaMon.Text = "Xóa món";
            this.btnXoaMon.Click += new System.EventHandler(this.btnXoaMon_Click);
            // 
            // btnThemMonMoi
            // 
            this.btnThemMonMoi.BorderRadius = 23;
            this.btnThemMonMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMonMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMonMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemMonMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemMonMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThemMonMoi.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnThemMonMoi.ForeColor = System.Drawing.Color.White;
            this.btnThemMonMoi.Image = global::QuanLiQuanCafe.Resource1.plus;
            this.btnThemMonMoi.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThemMonMoi.Location = new System.Drawing.Point(3, 290);
            this.btnThemMonMoi.Name = "btnThemMonMoi";
            this.btnThemMonMoi.Size = new System.Drawing.Size(145, 45);
            this.btnThemMonMoi.TabIndex = 20;
            this.btnThemMonMoi.Text = "Thêm món mới ";
            this.btnThemMonMoi.Click += new System.EventHandler(this.btnThemMonMoi_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.guna2Panel2.Controls.Add(this.label4);
            this.guna2Panel2.Location = new System.Drawing.Point(51, 130);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(420, 56);
            this.guna2Panel2.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.label4.Location = new System.Drawing.Point(157, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 22);
            this.label4.TabIndex = 23;
            this.label4.Text = "Chi tiết món ";
            // 
            // dgvMon
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvMon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMon.ColumnHeadersHeight = 30;
            this.dgvMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMon.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMon.Location = new System.Drawing.Point(489, 192);
            this.dgvMon.MultiSelect = false;
            this.dgvMon.Name = "dgvMon";
            this.dgvMon.ReadOnly = true;
            this.dgvMon.RowHeadersVisible = false;
            this.dgvMon.Size = new System.Drawing.Size(364, 451);
            this.dgvMon.TabIndex = 23;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvMon.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvMon.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMon.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvMon.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMon.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMon.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMon.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvMon.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvMon.ThemeStyle.ReadOnly = true;
            this.dgvMon.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMon.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMon.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMon.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvMon.ThemeStyle.RowsStyle.Height = 22;
            this.dgvMon.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMon.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvMon.SelectionChanged += new System.EventHandler(this.dgvMon_SelectionChanged);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderColor = System.Drawing.Color.Black;
            this.txtTimKiem.BorderRadius = 20;
            this.txtTimKiem.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(144, 62);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(160, 43);
            this.txtTimKiem.TabIndex = 25;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.label5.Location = new System.Drawing.Point(48, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 22);
            this.label5.TabIndex = 24;
            this.label5.Text = "Tìm kiếm";
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.IndianRed;
            this.guna2Panel3.Controls.Add(this.guna2HtmlLabel2);
            this.guna2Panel3.ForeColor = System.Drawing.SystemColors.Control;
            this.guna2Panel3.Location = new System.Drawing.Point(489, 130);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(364, 56);
            this.guna2Panel3.TabIndex = 29;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.guna2HtmlLabel2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(125, 24);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(133, 24);
            this.guna2HtmlLabel2.TabIndex = 0;
            this.guna2HtmlLabel2.Text = "Danh sách món ";
            // 
            // flpLoaiMon
            // 
            this.flpLoaiMon.AutoScroll = true;
            this.flpLoaiMon.Location = new System.Drawing.Point(363, 12);
            this.flpLoaiMon.Name = "flpLoaiMon";
            this.flpLoaiMon.Size = new System.Drawing.Size(490, 93);
            this.flpLoaiMon.TabIndex = 30;
            // 
            // btnTimMon
            // 
            this.btnTimMon.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.Image = ((System.Drawing.Image)(resources.GetObject("btnTimMon.Image")));
            this.btnTimMon.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnTimMon.ImageRotate = 0F;
            this.btnTimMon.ImageSize = new System.Drawing.Size(32, 32);
            this.btnTimMon.Location = new System.Drawing.Point(310, 69);
            this.btnTimMon.Name = "btnTimMon";
            this.btnTimMon.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.Size = new System.Drawing.Size(38, 36);
            this.btnTimMon.TabIndex = 26;
            this.btnTimMon.Click += new System.EventHandler(this.btnTimMon_Click);
            // 
            // frmThemMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(910, 679);
            this.Controls.Add(this.flpLoaiMon);
            this.Controls.Add(this.guna2Panel3);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.btnTimMon);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvMon);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "frmThemMon";
            this.Text = "Quản lý món - Admin";
            this.Load += new System.EventHandler(this.frmThemMon_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).EndInit();
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2DataGridView dgvMon;
        private Guna.UI2.WinForms.Guna2ImageButton btnTimMon;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2Button btnSuaMon;
        private Guna.UI2.WinForms.Guna2Button btnXoaMon;
        private Guna.UI2.WinForms.Guna2Button btnThemMonMoi;
        private Guna.UI2.WinForms.Guna2TextBox txtGia;
        private Guna.UI2.WinForms.Guna2TextBox txtTenMon;
        private System.Windows.Forms.FlowLayoutPanel flpLoaiMon;
        private ComboBox cmbLoai;
        private Guna.UI2.WinForms.Guna2Button btnThemLoai;
    }
}