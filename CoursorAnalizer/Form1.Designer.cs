namespace CursorAnalyzer
{
    partial class Analyzer
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
            this.STOPBtn = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.RegBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.NameLbl = new System.Windows.Forms.Label();
            this.counterLbl = new System.Windows.Forms.Label();
            this.DBCheckBox = new System.Windows.Forms.CheckBox();
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
            // STOPBtn
            // 
            this.STOPBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.STOPBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.STOPBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.STOPBtn.Location = new System.Drawing.Point(1064, 2);
            this.STOPBtn.Name = "STOPBtn";
            this.STOPBtn.Size = new System.Drawing.Size(48, 23);
            this.STOPBtn.TabIndex = 2;
            this.STOPBtn.Text = "STOP";
            this.STOPBtn.UseVisualStyleBackColor = false;
            this.STOPBtn.Click += new System.EventHandler(this.STOPBtn_Click);
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
            this.nameTextBox.Text = "Your name";
            this.nameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nameTextBox.GotFocus += new System.EventHandler(this.NameTextBox_onFocus);
            this.nameTextBox.LostFocus += new System.EventHandler(this.NameTextBox_onBlure);
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
            // counterLbl
            // 
            this.counterLbl.AutoSize = true;
            this.counterLbl.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.counterLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.counterLbl.Location = new System.Drawing.Point(488, 438);
            this.counterLbl.Name = "counterLbl";
            this.counterLbl.Size = new System.Drawing.Size(80, 22);
            this.counterLbl.TabIndex = 9;
            this.counterLbl.Text = "Counter";
            // 
            // DBCheckBox
            // 
            this.DBCheckBox.AutoSize = true;
            this.DBCheckBox.Checked = true;
            this.DBCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DBCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DBCheckBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.DBCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.DBCheckBox.Location = new System.Drawing.Point(79, 467);
            this.DBCheckBox.Name = "DBCheckBox";
            this.DBCheckBox.Size = new System.Drawing.Size(107, 21);
            this.DBCheckBox.TabIndex = 11;
            this.DBCheckBox.Text = "Save in DB";
            this.DBCheckBox.UseVisualStyleBackColor = true;
            // 
            // Analyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1110, 562);
            this.Controls.Add(this.DBCheckBox);
            this.Controls.Add(this.counterLbl);
            this.Controls.Add(this.NameLbl);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.RegBtn);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.STOPBtn);
            this.Controls.Add(this.outTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Analyzer";
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
        private System.Windows.Forms.Button STOPBtn;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button RegBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.Label counterLbl;
        private System.Windows.Forms.CheckBox DBCheckBox;
    }
}

