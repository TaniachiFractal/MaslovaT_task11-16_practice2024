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
            this.tbDebug = new System.Windows.Forms.TextBox();
            this.tbDebug2 = new System.Windows.Forms.TextBox();
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
            // tbDebug
            // 
            this.tbDebug.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbDebug.Location = new System.Drawing.Point(547, 20);
            this.tbDebug.Multiline = true;
            this.tbDebug.Name = "tbDebug";
            this.tbDebug.ReadOnly = true;
            this.tbDebug.Size = new System.Drawing.Size(180, 180);
            this.tbDebug.TabIndex = 3;
            // 
            // tbDebug2
            // 
            this.tbDebug2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbDebug2.Location = new System.Drawing.Point(547, 468);
            this.tbDebug2.Multiline = true;
            this.tbDebug2.Name = "tbDebug2";
            this.tbDebug2.ReadOnly = true;
            this.tbDebug2.Size = new System.Drawing.Size(180, 180);
            this.tbDebug2.TabIndex = 4;
            // 
            // FoxesAndChickens
            // 
            this.AcceptButton = this.btHelp;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(739, 660);
            this.Controls.Add(this.tbDebug2);
            this.Controls.Add(this.lbChickenCount);
            this.Controls.Add(this.lbTextChicksLeft);
            this.Controls.Add(this.tbDebug);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btHelp;
        private System.Windows.Forms.Label lbTextChicksLeft;
        private System.Windows.Forms.Label lbChickenCount;
        private System.Windows.Forms.TextBox tbDebug;
        private System.Windows.Forms.TextBox tbDebug2;
    }
}

