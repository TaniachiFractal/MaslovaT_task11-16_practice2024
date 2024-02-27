
namespace MaslovaT_task12_practice2024
{
    partial class SudokuFormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SudokuFormMain));
            this.OpenSudokuFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btLoadSudoku = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenSudokuFileDialog
            // 
            this.OpenSudokuFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            this.OpenSudokuFileDialog.Title = "Выберите файл с данными судоку";
            // 
            // btLoadSudoku
            // 
            this.btLoadSudoku.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btLoadSudoku.Location = new System.Drawing.Point(12, 12);
            this.btLoadSudoku.Name = "btLoadSudoku";
            this.btLoadSudoku.Size = new System.Drawing.Size(182, 47);
            this.btLoadSudoku.TabIndex = 0;
            this.btLoadSudoku.Text = "Загрузить судоку";
            this.btLoadSudoku.UseVisualStyleBackColor = false;
            this.btLoadSudoku.Click += new System.EventHandler(this.BtLoadSudoku_Click);
            // 
            // SudokuFormMain
            // 
            this.AcceptButton = this.btLoadSudoku;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(552, 442);
            this.Controls.Add(this.btLoadSudoku);
            this.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MinimizeBox = false;
            this.Name = "SudokuFormMain";
            this.Text = "Судокер";
            this.Load += new System.EventHandler(this.SudokuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenSudokuFileDialog;
        private System.Windows.Forms.Button btLoadSudoku;
    }
}

