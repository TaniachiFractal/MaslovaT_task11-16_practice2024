using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaslovaT_task12_practice2024
{
    public partial class SudokuFormMain : Form
    {
        public SudokuFormMain()
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
        static int CELL_SZ = 90;

        /// <summary>
        /// Paddings between elements
        /// </summary>
        static int PADDING_BIG = 10, PADDING_SMALL = 3;

        /// <summary>
        /// Full path to the selected sudoku file
        /// </summary>
        static string sudokuFileLocation;

        #endregion

        #endregion

        #region Form Events

        /// <summary>
        /// Load the form
        /// </summary>
        private void SudokuForm_Load(object sender, EventArgs e)
        {
            CELL_SZ = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 25;
            PADDING_BIG = CELL_SZ / 3;
            PADDING_SMALL = CELL_SZ / 10;

            DrawGameField();
        }

        /// <summary>
        /// Load sudoku
        /// </summary>
        private void BtLoadSudoku_Click(object sender, EventArgs e)
        {
            if (OpenSudokuFileDialog.ShowDialog() == DialogResult.OK)
            {
                sudokuFileLocation = OpenSudokuFileDialog.FileName;
            }
        }

        #endregion

        #region Render

        /// <summary>
        /// Output the sudoku
        /// </summary>
        private void DrawGameField()
        {
            RenderOtherControls();

            int paddingY = 0;
            for (int i = 0; i < FLD_SZ; i++)
            {
                int paddingX = 0;
                for (int j = 0; j < FLD_SZ; j++)
                {
                    buttonArray[i, j] = new Button();
                    var currBt = buttonArray[i, j];

                    currBt.Name = "bt" + i + '_' + j;
                    currBt.Location = new System.Drawing.Point(j * CELL_SZ + PADDING_BIG + paddingX, i * CELL_SZ + PADDING_BIG + btLoadSudoku.Bottom + paddingY);
                    currBt.Size = new System.Drawing.Size(CELL_SZ, CELL_SZ);

                    currBt.FlatStyle = FlatStyle.Flat;
                    currBt.FlatAppearance.BorderColor = Color.LightGray;
                    currBt.FlatAppearance.BorderSize = 1;

                    currBt.BackColor = Color.White;

                    currBt.Text = i.ToString();
                    currBt.ForeColor = Color.Black;
                    currBt.Font = new Font(this.Font.FontFamily,(int)Math.Floor(CELL_SZ/2.3)  , FontStyle.Regular);

                    Controls.Add(buttonArray[i,j]);

                    if (j % 3 == 2) paddingX += PADDING_SMALL;
                }
                if (i % 3 == 2) paddingY += PADDING_SMALL;
            }


        }

        /// <summary>
        /// Draw other controls based on cell size
        /// </summary>
        private void RenderOtherControls()
        {
            this.Size = new System.Drawing.Size(CELL_SZ*FLD_SZ+PADDING_BIG*4, CELL_SZ * FLD_SZ + PADDING_BIG*6 + btLoadSudoku.Bottom);
            btLoadSudoku.Location = new System.Drawing.Point( PADDING_BIG ,  PADDING_BIG );
            btLoadSudoku.Width = CELL_SZ*FLD_SZ+PADDING_SMALL*2;
        }

        #endregion

        #region Working with data



        #endregion

       
    }
}
