﻿
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
            this.btHelp_FileFormat = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
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
            this.btLoadSudoku.Size = new System.Drawing.Size(103, 47);
            this.btLoadSudoku.TabIndex = 0;
            this.btLoadSudoku.Text = "Загрузить судоку";
            this.btLoadSudoku.UseVisualStyleBackColor = false;
            this.btLoadSudoku.Click += new System.EventHandler(this.BtLoadSudoku_Click);
            // 
            // btHelp_FileFormat
            // 
            this.btHelp_FileFormat.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btHelp_FileFormat.Location = new System.Drawing.Point(121, 12);
            this.btHelp_FileFormat.Name = "btHelp_FileFormat";
            this.btHelp_FileFormat.Size = new System.Drawing.Size(96, 47);
            this.btHelp_FileFormat.TabIndex = 1;
            this.btHelp_FileFormat.Text = "Помощь";
            this.btHelp_FileFormat.UseVisualStyleBackColor = false;
            this.btHelp_FileFormat.Click += new System.EventHandler(this.BtHelp_FileFormat_Click);
            // 
            // btReset
            // 
            this.btReset.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btReset.Location = new System.Drawing.Point(223, 12);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(86, 47);
            this.btReset.TabIndex = 2;
            this.btReset.Text = "Заново";
            this.btReset.UseVisualStyleBackColor = false;
            this.btReset.Click += new System.EventHandler(this.BtReset_Click);
            // 
            // SudokuFormMain
            // 
            this.AcceptButton = this.btHelp_FileFormat;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1075, 535);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.btHelp_FileFormat);
            this.Controls.Add(this.btLoadSudoku);
            this.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.Name = "SudokuFormMain";
            this.Text = "Судокер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SudokuFormMain_FormClosing);
            this.Load += new System.EventHandler(this.SudokuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenSudokuFileDialog;
        private System.Windows.Forms.Button btLoadSudoku;
        private System.Windows.Forms.Button btHelp_FileFormat;
        private System.Windows.Forms.Button btReset;
    }
}

