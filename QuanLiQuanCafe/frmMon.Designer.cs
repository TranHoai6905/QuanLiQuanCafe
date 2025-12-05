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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMon));
            this.label4 = new System.Windows.Forms.Label();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgvMon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtSoLuongMon = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTongTien = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboHoaDon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.flpLoaiMon = new System.Windows.Forms.FlowLayoutPanel();
            this.flpMon = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnThemMonVaoDon = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnThanhToan = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnThemHoaDonMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaHoaDon = new Guna.UI2.WinForms.Guna2Button();
            this.btnTimMon = new Guna.UI2.WinForms.Guna2ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.label4.Location = new System.Drawing.Point(139, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Tìm kiếm";
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
            this.txtTimKiem.Location = new System.Drawing.Point(235, 25);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(242, 45);
            this.txtTimKiem.TabIndex = 15;
            // 
            // dgvMon
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvMon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMon.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMon.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMon.Location = new System.Drawing.Point(6, 195);
            this.dgvMon.MultiSelect = false;
            this.dgvMon.Name = "dgvMon";
            this.dgvMon.ReadOnly = true;
            this.dgvMon.RowHeadersVisible = false;
            this.dgvMon.Size = new System.Drawing.Size(300, 137);
            this.dgvMon.TabIndex = 19;
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
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel4);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel3);
            this.guna2Panel1.Controls.Add(this.txtSoLuongMon);
            this.guna2Panel1.Controls.Add(this.txtTongTien);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.Controls.Add(this.cboHoaDon);
            this.guna2Panel1.Controls.Add(this.btnThanhToan);
            this.guna2Panel1.Controls.Add(this.guna2Panel2);
            this.guna2Panel1.Controls.Add(this.dgvMon);
            this.guna2Panel1.Controls.Add(this.btnThemHoaDonMoi);
            this.guna2Panel1.Controls.Add(this.btnXoaHoaDon);
            this.guna2Panel1.Location = new System.Drawing.Point(576, 129);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(313, 546);
            this.guna2Panel1.TabIndex = 22;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(16, 362);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(123, 24);
            this.guna2HtmlLabel4.TabIndex = 29;
            this.guna2HtmlLabel4.Text = "Số lượng món";
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(16, 428);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(93, 24);
            this.guna2HtmlLabel3.TabIndex = 28;
            this.guna2HtmlLabel3.Text = "Tổng tiền ";
            // 
            // txtSoLuongMon
            // 
            this.txtSoLuongMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoLuongMon.DefaultText = "";
            this.txtSoLuongMon.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSoLuongMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSoLuongMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoLuongMon.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoLuongMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoLuongMon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoLuongMon.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoLuongMon.Location = new System.Drawing.Point(161, 350);
            this.txtSoLuongMon.Name = "txtSoLuongMon";
            this.txtSoLuongMon.PlaceholderText = "";
            this.txtSoLuongMon.SelectedText = "";
            this.txtSoLuongMon.Size = new System.Drawing.Size(145, 36);
            this.txtSoLuongMon.TabIndex = 27;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTongTien.DefaultText = "";
            this.txtTongTien.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTongTien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTongTien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTongTien.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTongTien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTongTien.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTongTien.Location = new System.Drawing.Point(129, 416);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.PlaceholderText = "";
            this.txtTongTien.SelectedText = "";
            this.txtTongTien.Size = new System.Drawing.Size(177, 36);
            this.txtTongTien.TabIndex = 26;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(16, 155);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(73, 24);
            this.guna2HtmlLabel1.TabIndex = 25;
            this.guna2HtmlLabel1.Text = "Hóa đơn";
            // 
            // cboHoaDon
            // 
            this.cboHoaDon.BackColor = System.Drawing.Color.Transparent;
            this.cboHoaDon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHoaDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoaDon.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboHoaDon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboHoaDon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboHoaDon.ItemHeight = 30;
            this.cboHoaDon.Location = new System.Drawing.Point(106, 143);
            this.cboHoaDon.Name = "cboHoaDon";
            this.cboHoaDon.Size = new System.Drawing.Size(200, 36);
            this.cboHoaDon.TabIndex = 24;
            this.cboHoaDon.SelectedIndexChanged += new System.EventHandler(this.cboHoaDon_SelectedIndexChanged);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Teal;
            this.guna2Panel2.Controls.Add(this.guna2PictureBox1);
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
            this.label1.Location = new System.Drawing.Point(64, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hóa đơn chưa thanh toán ";
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.guna2Panel3.BorderColor = System.Drawing.Color.White;
            this.guna2Panel3.Controls.Add(this.guna2PictureBox3);
            this.guna2Panel3.Controls.Add(this.guna2HtmlLabel2);
            this.guna2Panel3.Location = new System.Drawing.Point(12, 129);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(558, 60);
            this.guna2Panel3.TabIndex = 23;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(249, 25);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(133, 24);
            this.guna2HtmlLabel2.TabIndex = 0;
            this.guna2HtmlLabel2.Text = "Danh sách món ";
            // 
            // flpLoaiMon
            // 
            this.flpLoaiMon.AutoScroll = true;
            this.flpLoaiMon.Location = new System.Drawing.Point(143, 77);
            this.flpLoaiMon.Name = "flpLoaiMon";
            this.flpLoaiMon.Size = new System.Drawing.Size(739, 46);
            this.flpLoaiMon.TabIndex = 25;
            // 
            // flpMon
            // 
            this.flpMon.Location = new System.Drawing.Point(12, 192);
            this.flpMon.Name = "flpMon";
            this.flpMon.Size = new System.Drawing.Size(558, 407);
            this.flpMon.TabIndex = 27;
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Image = global::QuanLiQuanCafe.Resource1.cafe;
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(12, 12);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(125, 111);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox2.TabIndex = 26;
            this.guna2PictureBox2.TabStop = false;
            // 
            // btnThemMonVaoDon
            // 
            this.btnThemMonVaoDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.btnThemMonVaoDon.BorderRadius = 28;
            this.btnThemMonVaoDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMonVaoDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMonVaoDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemMonVaoDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemMonVaoDon.FillColor = System.Drawing.Color.LimeGreen;
            this.btnThemMonVaoDon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnThemMonVaoDon.ForeColor = System.Drawing.Color.White;
            this.btnThemMonVaoDon.Image = global::QuanLiQuanCafe.Resource1.add;
            this.btnThemMonVaoDon.ImageSize = new System.Drawing.Size(35, 35);
            this.btnThemMonVaoDon.Location = new System.Drawing.Point(175, 612);
            this.btnThemMonVaoDon.Name = "btnThemMonVaoDon";
            this.btnThemMonVaoDon.Size = new System.Drawing.Size(228, 54);
            this.btnThemMonVaoDon.TabIndex = 24;
            this.btnThemMonVaoDon.Text = "Thêm món vào hóa đơn ";
            this.btnThemMonVaoDon.Click += new System.EventHandler(this.btnThemMonVaoDon_Click);
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.Image = global::QuanLiQuanCafe.Resource1.menu__1_;
            this.guna2PictureBox3.ImageRotate = 0F;
            this.guna2PictureBox3.Location = new System.Drawing.Point(194, 5);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.Size = new System.Drawing.Size(49, 54);
            this.guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox3.TabIndex = 27;
            this.guna2PictureBox3.TabStop = false;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.LightBlue;
            this.btnThanhToan.BorderRadius = 23;
            this.btnThanhToan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThanhToan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThanhToan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThanhToan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThanhToan.FillColor = System.Drawing.Color.IndianRed;
            this.btnThanhToan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Image = global::QuanLiQuanCafe.Resource1.pay;
            this.btnThanhToan.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThanhToan.Location = new System.Drawing.Point(68, 483);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(180, 45);
            this.btnThanhToan.TabIndex = 23;
            this.btnThanhToan.Text = "Thanh toán ";
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::QuanLiQuanCafe.Resource1.payment;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(6, 13);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(52, 44);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox1.TabIndex = 28;
            this.guna2PictureBox1.TabStop = false;
            // 
            // btnThemHoaDonMoi
            // 
            this.btnThemHoaDonMoi.BorderRadius = 25;
            this.btnThemHoaDonMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemHoaDonMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemHoaDonMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemHoaDonMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemHoaDonMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnThemHoaDonMoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnThemHoaDonMoi.ForeColor = System.Drawing.Color.White;
            this.btnThemHoaDonMoi.Image = global::QuanLiQuanCafe.Resource1.plus;
            this.btnThemHoaDonMoi.ImageSize = new System.Drawing.Size(30, 30);
            this.btnThemHoaDonMoi.Location = new System.Drawing.Point(6, 73);
            this.btnThemHoaDonMoi.Name = "btnThemHoaDonMoi";
            this.btnThemHoaDonMoi.Size = new System.Drawing.Size(149, 54);
            this.btnThemHoaDonMoi.TabIndex = 21;
            this.btnThemHoaDonMoi.Text = "Thêm hóa đơn";
            this.btnThemHoaDonMoi.Click += new System.EventHandler(this.btnThemHoaDonMoi_Click);
            // 
            // btnXoaHoaDon
            // 
            this.btnXoaHoaDon.BorderRadius = 25;
            this.btnXoaHoaDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaHoaDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaHoaDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoaHoaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoaHoaDon.FillColor = System.Drawing.Color.Tomato;
            this.btnXoaHoaDon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnXoaHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnXoaHoaDon.Image = global::QuanLiQuanCafe.Resource1.cross;
            this.btnXoaHoaDon.ImageSize = new System.Drawing.Size(30, 30);
            this.btnXoaHoaDon.Location = new System.Drawing.Point(161, 73);
            this.btnXoaHoaDon.Name = "btnXoaHoaDon";
            this.btnXoaHoaDon.Size = new System.Drawing.Size(145, 54);
            this.btnXoaHoaDon.TabIndex = 20;
            this.btnXoaHoaDon.Text = "Xóa hóa đơn";
            this.btnXoaHoaDon.Click += new System.EventHandler(this.btnXoaHoaDon_Click);
            // 
            // btnTimMon
            // 
            this.btnTimMon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.btnTimMon.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.Image = ((System.Drawing.Image)(resources.GetObject("btnTimMon.Image")));
            this.btnTimMon.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnTimMon.ImageRotate = 0F;
            this.btnTimMon.ImageSize = new System.Drawing.Size(32, 32);
            this.btnTimMon.Location = new System.Drawing.Point(483, 30);
            this.btnTimMon.Name = "btnTimMon";
            this.btnTimMon.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTimMon.Size = new System.Drawing.Size(38, 40);
            this.btnTimMon.TabIndex = 16;
            this.btnTimMon.Click += new System.EventHandler(this.btnTimMon_Click);
            // 
            // frmMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(901, 687);
            this.Controls.Add(this.flpMon);
            this.Controls.Add(this.guna2PictureBox2);
            this.Controls.Add(this.flpLoaiMon);
            this.Controls.Add(this.btnThemMonVaoDon);
            this.Controls.Add(this.guna2Panel3);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnTimMon);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMon";
            this.Load += new System.EventHandler(this.frmMon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2ImageButton btnTimMon;
        private Guna.UI2.WinForms.Guna2DataGridView dgvMon;
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
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private System.Windows.Forms.FlowLayoutPanel flpMon;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2ComboBox cboHoaDon;
        private Guna.UI2.WinForms.Guna2Button btnThanhToan;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2TextBox txtSoLuongMon;
        private Guna.UI2.WinForms.Guna2TextBox txtTongTien;
    }
}
