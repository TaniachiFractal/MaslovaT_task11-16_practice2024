namespace MaslovaT_task13_practice2024
{
    partial class YouWonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YouWonForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btRestart = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 66);
            this.label1.TabIndex = 3;
            this.label1.Text = "ПОБЕДА! \r\n9 Куриц добралось до курятника!";
            // 
            // btRestart
            // 
            this.btRestart.BackColor = System.Drawing.Color.White;
            this.btRestart.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btRestart.Location = new System.Drawing.Point(394, 8);
            this.btRestart.Name = "btRestart";
            this.btRestart.Size = new System.Drawing.Size(130, 67);
            this.btRestart.TabIndex = 4;
            this.btRestart.Text = "Заново";
            this.btRestart.UseVisualStyleBackColor = false;
            this.btRestart.Click += new System.EventHandler(this.BtRestart_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MaslovaT_task13_practice2024.Properties.Resources._9chickens;
            this.pictureBox1.Location = new System.Drawing.Point(12, 91);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(504, 387);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // YouWonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(543, 491);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btRestart);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YouWonForm";
            this.Text = "Победа!";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btRestart;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}