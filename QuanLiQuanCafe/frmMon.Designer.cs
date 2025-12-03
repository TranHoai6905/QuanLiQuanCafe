namespace QuanLiQuanCafe
{
    partial class frmMon
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMon));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvMon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTimMon = new Guna.UI2.WinForms.Guna2ImageButton();
            this.dgvHoaDonChuaThanhToan = new Guna.UI2.WinForms.Guna2DataGridView();
            this.btnXoaHoaDon = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemHoaDonMoi = new Guna.UI2.WinForms.Guna2Button();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnThemMonVaoDon = new Guna.UI2.WinForms.Guna2Button();
            this.flpLoaiMon = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDonChuaThanhToan)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.label4.Location = new System.Drawing.Point(36, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Tìm kiếm";
            // 
            // dgvMon
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.dgvMon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvMon.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvMon.ColumnHeadersHeight = 30;
            this.dgvMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMon.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMon.Location = new System.Drawing.Point(39, 195);
            this.dgvMon.MultiSelect = false;
            this.dgvMon.Name = "dgvMon";
            this.dgvMon.ReadOnly = true;
            this.dgvMon.RowHeadersVisible = false;
            this.dgvMon.Size = new System.Drawing.Size(353, 359);
            this.dgvMon.TabIndex = 14;
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
            this.dgvMon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMon_CellClick);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 20;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(132, 61);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(180, 44);
            this.txtTimKiem.TabIndex = 15;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // btnTimMon
            // 
            this.btnTimMon.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.Image = ((System.Drawing.Image)(resources.GetObject("btnTimMon.Image")));
            this.btnTimMon.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnTimMon.ImageRotate = 0F;
            this.btnTimMon.ImageSize = new System.Drawing.Size(32, 32);
            this.btnTimMon.Location = new System.Drawing.Point(318, 59);
            this.btnTimMon.Name = "btnTimMon";
            this.btnTimMon.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.Size = new System.Drawing.Size(43, 46);
            this.btnTimMon.TabIndex = 16;
            this.btnTimMon.Click += new System.EventHandler(this.btnTimMon_Click);
            // 
            // dgvHoaDonChuaThanhToan
            // 
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.dgvHoaDonChuaThanhToan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvHoaDonChuaThanhToan.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvHoaDonChuaThanhToan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHoaDonChuaThanhToan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvHoaDonChuaThanhToan.ColumnHeadersHeight = 30;
            this.dgvHoaDonChuaThanhToan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHoaDonChuaThanhToan.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvHoaDonChuaThanhToan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHoaDonChuaThanhToan.Location = new System.Drawing.Point(16, 82);
            this.dgvHoaDonChuaThanhToan.MultiSelect = false;
            this.dgvHoaDonChuaThanhToan.Name = "dgvHoaDonChuaThanhToan";
            this.dgvHoaDonChuaThanhToan.ReadOnly = true;
            this.dgvHoaDonChuaThanhToan.RowHeadersVisible = false;
            this.dgvHoaDonChuaThanhToan.Size = new System.Drawing.Size(412, 343);
            this.dgvHoaDonChuaThanhToan.TabIndex = 19;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHoaDonChuaThanhToan.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvHoaDonChuaThanhToan.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvHoaDonChuaThanhToan.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.ReadOnly = true;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvHoaDonChuaThanhToan.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvHoaDonChuaThanhToan.ThemeStyle.RowsStyle.Height = 22;
            this.dgvHoaDonChuaThanhToan.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHoaDonChuaThanhToan.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvHoaDonChuaThanhToan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoaDonChuaThanhToan_CellClick);
            // 
            // btnXoaHoaDon
            // 
            this.btnXoaHoaDon.BorderRadius = 25;
            this.btnXoaHoaDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaHoaDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaHoaDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoaHoaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoaHoaDon.FillColor = System.Drawing.Color.Tomato;
            this.btnXoaHoaDon.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnXoaHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnXoaHoaDon.Image = global::QuanLiQuanCafe.Resource1.cross;
            this.btnXoaHoaDon.ImageSize = new System.Drawing.Size(30, 30);
            this.btnXoaHoaDon.Location = new System.Drawing.Point(245, 440);
            this.btnXoaHoaDon.Name = "btnXoaHoaDon";
            this.btnXoaHoaDon.Size = new System.Drawing.Size(162, 54);
            this.btnXoaHoaDon.TabIndex = 20;
            this.btnXoaHoaDon.Text = "Xóa hóa đơn";
            this.btnXoaHoaDon.Click += new System.EventHandler(this.btnXoaHoaDon_Click);
            // 
            // btnThemHoaDonMoi
            // 
            this.btnThemHoaDonMoi.BorderRadius = 25;
            this.btnThemHoaDonMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemHoaDonMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemHoaDonMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemHoaDonMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemHoaDonMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnThemHoaDonMoi.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnThemHoaDonMoi.ForeColor = System.Drawing.Color.White;
            this.btnThemHoaDonMoi.Image = global::QuanLiQuanCafe.Resource1.plus;
            this.btnThemHoaDonMoi.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThemHoaDonMoi.Location = new System.Drawing.Point(44, 440);
            this.btnThemHoaDonMoi.Name = "btnThemHoaDonMoi";
            this.btnThemHoaDonMoi.Size = new System.Drawing.Size(177, 54);
            this.btnThemHoaDonMoi.TabIndex = 21;
            this.btnThemHoaDonMoi.Text = "Thêm hóa đơn mới ";
            this.btnThemHoaDonMoi.Click += new System.EventHandler(this.btnThemHoaDonMoi_Click);
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.LightBlue;
            this.guna2Panel1.Controls.Add(this.guna2Panel2);
            this.guna2Panel1.Controls.Add(this.dgvHoaDonChuaThanhToan);
            this.guna2Panel1.Controls.Add(this.btnThemHoaDonMoi);
            this.guna2Panel1.Controls.Add(this.btnXoaHoaDon);
            this.guna2Panel1.Location = new System.Drawing.Point(418, 129);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(445, 515);
            this.guna2Panel1.TabIndex = 22;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Teal;
            this.guna2Panel2.Controls.Add(this.label1);
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(445, 60);
            this.guna2Panel2.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(96, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hóa đơn chưa thanh toán ";
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.guna2Panel3.BorderColor = System.Drawing.Color.White;
            this.guna2Panel3.Controls.Add(this.guna2HtmlLabel2);
            this.guna2Panel3.Location = new System.Drawing.Point(39, 129);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(353, 60);
            this.guna2Panel3.TabIndex = 23;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(103, 25);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(133, 24);
            this.guna2HtmlLabel2.TabIndex = 0;
            this.guna2HtmlLabel2.Text = "Danh sách món ";
            // 
            // btnThemMonVaoDon
            // 
            this.btnThemMonVaoDon.BorderRadius = 25;
            this.btnThemMonVaoDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMonVaoDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMonVaoDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemMonVaoDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemMonVaoDon.FillColor = System.Drawing.Color.LimeGreen;
            this.btnThemMonVaoDon.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnThemMonVaoDon.ForeColor = System.Drawing.Color.White;
            this.btnThemMonVaoDon.Image = global::QuanLiQuanCafe.Resource1.plus;
            this.btnThemMonVaoDon.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThemMonVaoDon.Location = new System.Drawing.Point(123, 569);
            this.btnThemMonVaoDon.Name = "btnThemMonVaoDon";
            this.btnThemMonVaoDon.Size = new System.Drawing.Size(219, 54);
            this.btnThemMonVaoDon.TabIndex = 24;
            this.btnThemMonVaoDon.Text = "Thêm món vào hóa đơn ";
            this.btnThemMonVaoDon.Click += new System.EventHandler(this.btnThemMonVaoDon_Click);
            // 
            // flpLoaiMon
            // 
            this.flpLoaiMon.AutoScroll = true;
            this.flpLoaiMon.Location = new System.Drawing.Point(367, 12);
            this.flpLoaiMon.Name = "flpLoaiMon";
            this.flpLoaiMon.Size = new System.Drawing.Size(496, 95);
            this.flpLoaiMon.TabIndex = 25;
            // 
            // frmMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(901, 687);
            this.Controls.Add(this.flpLoaiMon);
            this.Controls.Add(this.btnThemMonVaoDon);
            this.Controls.Add(this.guna2Panel3);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnTimMon);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.dgvMon);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMon";
            this.Load += new System.EventHandler(this.frmMon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDonChuaThanhToan)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private Guna.UI2.WinForms.Guna2DataGridView dgvMon;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2ImageButton btnTimMon;
        private Guna.UI2.WinForms.Guna2DataGridView dgvHoaDonChuaThanhToan;
        private Guna.UI2.WinForms.Guna2Button btnXoaHoaDon;
        private Guna.UI2.WinForms.Guna2Button btnThemHoaDonMoi;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2Button btnThemMonVaoDon;
        private System.Windows.Forms.FlowLayoutPanel flpLoaiMon;
    }
}
