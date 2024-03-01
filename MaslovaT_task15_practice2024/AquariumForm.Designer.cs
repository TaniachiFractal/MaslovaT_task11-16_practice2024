
namespace MaslovaT_task15_practice2024
{
    partial class AquariumForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AquariumForm));
            mainTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // mainTimer
            // 
            mainTimer.Tick += MainTimer_Tick;
            // 
            // AquariumForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.SteelBlue;
            ClientSize = new System.Drawing.Size(776, 520);
            Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "AquariumForm";
            Text = "Аквариум";
            Load += AquariumForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer mainTimer;
    }
}

