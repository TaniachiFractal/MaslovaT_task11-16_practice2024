namespace MaslovaT_task16_practice2024
{
    partial class SpideysForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpideysForm));
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.btSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 30;
            this.mainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // btSettings
            // 
            this.btSettings.Location = new System.Drawing.Point(12, 12);
            this.btSettings.Name = "btSettings";
            this.btSettings.Size = new System.Drawing.Size(55, 44);
            this.btSettings.TabIndex = 0;
            this.btSettings.Text = "set";
            this.btSettings.UseVisualStyleBackColor = true;
            this.btSettings.Click += new System.EventHandler(this.BtSettings_Click);
            // 
            // SpideysForm
            // 
            this.AcceptButton = this.btSettings;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(606, 599);
            this.Controls.Add(this.btSettings);
            this.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "SpideysForm";
            this.Text = "Паучки";
            this.Load += new System.EventHandler(this.SpideysForm_Load);
            this.ResizeBegin += new System.EventHandler(this.SpideysForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.SpideysForm_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.Button btSettings;
    }
}

