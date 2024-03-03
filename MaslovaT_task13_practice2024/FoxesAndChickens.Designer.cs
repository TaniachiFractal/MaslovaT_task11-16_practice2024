namespace MaslovaT_task13_practice2024
{
    partial class FoxesAndChickens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FoxesAndChickens));
            this.btHelp = new System.Windows.Forms.Button();
            this.lbTextChicksLeft = new System.Windows.Forms.Label();
            this.lbChickenCount = new System.Windows.Forms.Label();
            this.pbChick = new System.Windows.Forms.PictureBox();
            this.pbFox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbChick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFox)).BeginInit();
            this.SuspendLayout();
            // 
            // btHelp
            // 
            this.btHelp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btHelp.Location = new System.Drawing.Point(20, 20);
            this.btHelp.Name = "btHelp";
            this.btHelp.Size = new System.Drawing.Size(100, 100);
            this.btHelp.TabIndex = 0;
            this.btHelp.Text = "Правила Игры";
            this.btHelp.UseVisualStyleBackColor = true;
            this.btHelp.Click += new System.EventHandler(this.BtHelp_Click);
            // 
            // lbTextChicksLeft
            // 
            this.lbTextChicksLeft.AutoSize = true;
            this.lbTextChicksLeft.ForeColor = System.Drawing.Color.White;
            this.lbTextChicksLeft.Location = new System.Drawing.Point(15, 492);
            this.lbTextChicksLeft.Name = "lbTextChicksLeft";
            this.lbTextChicksLeft.Size = new System.Drawing.Size(149, 25);
            this.lbTextChicksLeft.TabIndex = 1;
            this.lbTextChicksLeft.Text = "Осталось Куриц:";
            // 
            // lbChickenCount
            // 
            this.lbChickenCount.AutoSize = true;
            this.lbChickenCount.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbChickenCount.ForeColor = System.Drawing.Color.Gainsboro;
            this.lbChickenCount.Location = new System.Drawing.Point(10, 517);
            this.lbChickenCount.Name = "lbChickenCount";
            this.lbChickenCount.Size = new System.Drawing.Size(70, 58);
            this.lbChickenCount.TabIndex = 2;
            this.lbChickenCount.Text = "00";
            // 
            // pbChick
            // 
            this.pbChick.Image = global::MaslovaT_task13_practice2024.Properties.Resources.chicken;
            this.pbChick.Location = new System.Drawing.Point(593, 12);
            this.pbChick.Name = "pbChick";
            this.pbChick.Size = new System.Drawing.Size(134, 136);
            this.pbChick.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbChick.TabIndex = 3;
            this.pbChick.TabStop = false;
            // 
            // pbFox
            // 
            this.pbFox.Image = global::MaslovaT_task13_practice2024.Properties.Resources.fox;
            this.pbFox.Location = new System.Drawing.Point(593, 512);
            this.pbFox.Name = "pbFox";
            this.pbFox.Size = new System.Drawing.Size(134, 136);
            this.pbFox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFox.TabIndex = 4;
            this.pbFox.TabStop = false;
            // 
            // FoxesAndChickens
            // 
            this.AcceptButton = this.btHelp;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(739, 660);
            this.Controls.Add(this.pbFox);
            this.Controls.Add(this.pbChick);
            this.Controls.Add(this.lbChickenCount);
            this.Controls.Add(this.lbTextChicksLeft);
            this.Controls.Add(this.btHelp);
            this.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.Name = "FoxesAndChickens";
            this.Text = "Лисы и Куры";
            this.Load += new System.EventHandler(this.FoxesAndChickens_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbChick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btHelp;
        private System.Windows.Forms.Label lbTextChicksLeft;
        private System.Windows.Forms.Label lbChickenCount;
        private System.Windows.Forms.PictureBox pbChick;
        private System.Windows.Forms.PictureBox pbFox;
    }
}

