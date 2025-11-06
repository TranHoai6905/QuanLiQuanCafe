namespace QuanLiQuanCafe
{
    partial class frmTrangChu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMon = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMon
            // 
            this.btnMon.Location = new System.Drawing.Point(78, 116);
            this.btnMon.Name = "btnMon";
            this.btnMon.Size = new System.Drawing.Size(75, 23);
            this.btnMon.TabIndex = 0;
            this.btnMon.Text = "Quản lý món";
            this.btnMon.UseVisualStyleBackColor = true;
            this.btnMon.Click += new System.EventHandler(this.btnMon_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.Location = new System.Drawing.Point(78, 177);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(75, 23);
            this.btnHoaDon.TabIndex = 1;
            this.btnHoaDon.Text = "Hóa Đơn";
            this.btnHoaDon.UseVisualStyleBackColor = true;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // frmTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnHoaDon);
            this.Controls.Add(this.btnMon);
            this.Name = "frmTrangChu";
            this.Text = "frmTrangChu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMon;
        private System.Windows.Forms.Button btnHoaDon;
    }
}