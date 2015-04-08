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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1086, 417);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // outTextBox
            // 
            this.outTextBox.Location = new System.Drawing.Point(12, 435);
            this.outTextBox.Multiline = true;
            this.outTextBox.Name = "outTextBox";
            this.outTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outTextBox.Size = new System.Drawing.Size(720, 115);
            this.outTextBox.TabIndex = 1;
            this.outTextBox.Text = " ";
            // 
            // STOPBaton
            // 
            this.STOPBaton.Location = new System.Drawing.Point(997, 32);
            this.STOPBaton.Name = "STOPBaton";
            this.STOPBaton.Size = new System.Drawing.Size(75, 23);
            this.STOPBaton.TabIndex = 2;
            this.STOPBaton.Text = "STOP";
            this.STOPBaton.UseVisualStyleBackColor = true;
            this.STOPBaton.Click += new System.EventHandler(this.STOPBaton_Click);
            // 
            // Analizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 562);
            this.Controls.Add(this.STOPBaton);
            this.Controls.Add(this.outTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
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
    }
}

