namespace QLNhaSach
{
    partial class HeThong
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
            btnLoaiSach = new Button();
            btnSach = new Button();
            btnHoaDon = new Button();
            SuspendLayout();
            // 
            // btnLoaiSach
            // 
            btnLoaiSach.Location = new Point(38, 39);
            btnLoaiSach.Name = "btnLoaiSach";
            btnLoaiSach.Size = new Size(94, 82);
            btnLoaiSach.TabIndex = 0;
            btnLoaiSach.Text = "Loại sách";
            btnLoaiSach.UseVisualStyleBackColor = true;
            btnLoaiSach.Click += btnLoaiSach_Click;
            // 
            // btnSach
            // 
            btnSach.Location = new Point(38, 177);
            btnSach.Name = "btnSach";
            btnSach.Size = new Size(94, 82);
            btnSach.TabIndex = 1;
            btnSach.Text = "Sách";
            btnSach.UseVisualStyleBackColor = true;
            btnSach.Click += btnSach_Click;
            // 
            // btnHoaDon
            // 
            btnHoaDon.Location = new Point(38, 309);
            btnHoaDon.Name = "btnHoaDon";
            btnHoaDon.Size = new Size(94, 82);
            btnHoaDon.TabIndex = 2;
            btnHoaDon.Text = "Hóa đơn";
            btnHoaDon.UseVisualStyleBackColor = true;
            btnHoaDon.Click += btnHoaDon_Click;
            // 
            // HeThong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnHoaDon);
            Controls.Add(btnSach);
            Controls.Add(btnLoaiSach);
            Name = "HeThong";
            Text = "HeThong";
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoaiSach;
        private Button btnSach;
        private Button btnHoaDon;
    }
}