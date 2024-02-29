namespace MaslovaT_task16_practice2024
{
    partial class UserSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.webNUD = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.webNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, -3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 35);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 35);
            this.label2.TabIndex = 3;
            this.label2.Text = "Макс. длина паутинки";
            // 
            // webNUD
            // 
            this.webNUD.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.webNUD.Location = new System.Drawing.Point(289, 18);
            this.webNUD.Margin = new System.Windows.Forms.Padding(4);
            this.webNUD.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.webNUD.Name = "webNUD";
            this.webNUD.Size = new System.Drawing.Size(80, 42);
            this.webNUD.TabIndex = 2;
            this.webNUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.webNUD.ValueChanged += new System.EventHandler(this.webNUD_ValueChanged);
            // 
            // UserSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(383, 80);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.webNUD);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserSettingsForm";
            this.Text = "Паучки - настройки";
            this.Load += new System.EventHandler(this.UserSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown webNUD;
    }
}