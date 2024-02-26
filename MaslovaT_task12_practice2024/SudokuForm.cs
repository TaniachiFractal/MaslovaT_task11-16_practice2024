using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaslovaT_task12_practice2024
{
    public partial class SudokuForm : Form
    {
        public SudokuForm()
        {
            InitializeComponent();
        }

        #region data

        #region Consts

        /// <summary>
        /// Size of the field
        /// </summary>
        const byte FLD_SZ = 9;

        #endregion

        #region Arrays

        /// <summary>
        /// Main array
        /// </summary>
        static byte[,] sudoku = new byte[FLD_SZ, FLD_SZ];
        /// <summary>
        /// Matrix that contains buttons to click on
        /// </summary>
        static private Button[,] buttonArray = new Button[FLD_SZ, FLD_SZ];

        #endregion

        #region Vars

        /// <summary>
        /// Size of the cell on screen
        /// </summary>
        static int CELL_SIZE = 90;

        #endregion

        #endregion

        private void DrawGameField()
        {
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    buttonArray[i, j] = new Button();
                    var currBt = buttonArray[i, j];

                    currBt.Name = "bt" + i + '_' + j;
                    currBt.Location = new System.Drawing.Point(j * CELL_SIZE + 20, i * CELL_SIZE + 20);
                    currBt.Size = new System.Drawing.Size(CELL_SIZE, CELL_SIZE);
                    currBt.FlatStyle = FlatStyle.Flat;
                    currBt.FlatAppearance.BorderColor = Color.White;
                    currBt.FlatAppearance.BorderSize = 1;

                    currBt.Text = i + "_" + j;
                    currBt.ForeColor = Color.Black;

                    Controls.Add(buttonArray[i,j]);
                }
            }
            RenderOtherControls();
        }

        private void RenderOtherControls()
        {
            this.Size = new System.Drawing.Size(CELL_SIZE*FLD_SZ+20, CELL_SIZE * FLD_SZ + 20);
        }
    }
}
