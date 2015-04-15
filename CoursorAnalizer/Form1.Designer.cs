namespace CoursorAnalizer
{
    partial class Analizer
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.outTextBox = new System.Windows.Forms.TextBox();
            this.STOPBaton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RegBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.NameLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1113, 427);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // outTextBox
            // 
            this.outTextBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.outTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.outTextBox.Location = new System.Drawing.Point(574, 435);
            this.outTextBox.Multiline = true;
            this.outTextBox.Name = "outTextBox";
            this.outTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outTextBox.Size = new System.Drawing.Size(524, 115);
            this.outTextBox.TabIndex = 1;
            // 
            // STOPBaton
            // 
            this.STOPBaton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.STOPBaton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.STOPBaton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.STOPBaton.Location = new System.Drawing.Point(1064, 2);
            this.STOPBaton.Name = "STOPBaton";
            this.STOPBaton.Size = new System.Drawing.Size(48, 23);
            this.STOPBaton.TabIndex = 2;
            this.STOPBaton.Text = "STOP";
            this.STOPBaton.UseVisualStyleBackColor = false;
            this.STOPBaton.Click += new System.EventHandler(this.STOPBaton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.nameTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.nameTextBox.Location = new System.Drawing.Point(76, 435);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(231, 23);
            this.nameTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(3, 438);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Your name";
            // 
            // RegBtn
            // 
            this.RegBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RegBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.RegBtn.Location = new System.Drawing.Point(313, 435);
            this.RegBtn.Name = "RegBtn";
            this.RegBtn.Size = new System.Drawing.Size(75, 23);
            this.RegBtn.TabIndex = 5;
            this.RegBtn.Text = "Reg";
            this.RegBtn.UseVisualStyleBackColor = false;
            this.RegBtn.Click += new System.EventHandler(this.RegBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ExitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExitBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ExitBtn.Location = new System.Drawing.Point(-1, 2);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(25, 25);
            this.ExitBtn.TabIndex = 6;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.NameLbl.Location = new System.Drawing.Point(73, 467);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(0, 19);
            this.NameLbl.TabIndex = 8;
            // 
            // Analizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1110, 562);
            this.Controls.Add(this.NameLbl);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.RegBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.STOPBaton);
            this.Controls.Add(this.outTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Analizer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analizer";
            this.Load += new System.EventHandler(this.Analizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox outTextBox;
        private System.Windows.Forms.Button STOPBaton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RegBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Label NameLbl;
    }
}

