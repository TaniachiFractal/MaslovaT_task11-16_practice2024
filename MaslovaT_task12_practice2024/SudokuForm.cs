using System;
using System.Drawing;
using System.IO;
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
        static SudokuElement[,] sudoku = new SudokuElement[FLD_SZ, FLD_SZ];
        /// <summary>
        /// Matrix that contains TextBoxes to click on
        /// </summary>
        static private TextBox[,] textBoxArray = new TextBox[FLD_SZ, FLD_SZ];

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
            InitSizeVars();
            InitEmptySudoku();

            OpenSudokuFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

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
                    textBoxArray[i, j] = new TextBox();
                    var currTB = textBoxArray[i, j];

                    currTB.Name = "tb" + i + '_' + j;
                    currTB.Location = new System.Drawing.Point(j * CELL_SZ + PADDING_BIG + paddingX, i * CELL_SZ + PADDING_BIG + btLoadSudoku.Bottom + paddingY);
                    
                    currTB.Size = new System.Drawing.Size(CELL_SZ, CELL_SZ);

                    currTB.BackColor = Color.White;

                    currTB.Text = sudoku[i,j].ToString();
                    currTB.ReadOnly = sudoku[i, j].locked;

                    currTB.ForeColor = Color.Black;
                    currTB.Font = new Font(this.Font.FontFamily, (int)Math.Floor(CELL_SZ/1.9), FontStyle.Regular);
                    currTB.TextAlign = HorizontalAlignment.Center;

                    Controls.Add(textBoxArray[i,j]);

                    if (j % 3 == 2) paddingX += PADDING_SMALL;
                }
                if (i % 3 == 2) paddingY += PADDING_SMALL+2;
            }


        }

        /// <summary>
        /// Draw other controls based on cell size
        /// </summary>
        private void RenderOtherControls()
        {       
            btLoadSudoku.Location = new System.Drawing.Point(PADDING_BIG, PADDING_BIG);
            btLoadSudoku.Size = new System.Drawing.Size(CELL_SZ * FLD_SZ + PADDING_SMALL*2, (int)(CELL_SZ*1.5));
            btLoadSudoku.Font = new Font(this.Font.FontFamily, (int)Math.Floor(CELL_SZ / 1.6), FontStyle.Regular);

            this.Size = new System.Drawing.Size(CELL_SZ * FLD_SZ + PADDING_BIG * 4, 
                30 + PADDING_BIG * 4 + PADDING_SMALL * 3 + CELL_SZ * FLD_SZ + btLoadSudoku.Height);
        }

        /// <summary>
        /// Read the sudoku array and update the text boxes
        /// </summary>
        private void UpdateGameField()
        {
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    textBoxArray[i, j].Text = sudoku[i, j].ToString();
                    textBoxArray[i, j].ReadOnly = sudoku[i, j].locked;
                }
            }
        }

        #endregion

        #region Working with data

        /// <summary>
        /// Change the dimens vars based on screen size
        /// </summary>
        static void InitSizeVars()
        {
            CELL_SZ = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 25;
            PADDING_BIG = CELL_SZ / 3;
            PADDING_SMALL = CELL_SZ / 10;
        }

        /// <summary>
        /// Initialize an empty sudoku
        /// </summary>
        static void InitEmptySudoku()
        {
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    sudoku[i, j] = new SudokuElement();
                }
            }
        }

        static bool ReadSudoku()
        {
            try
            {
                StreamReader sr = new StreamReader(sudokuFileLocation);
                string @iterator;
                while ((@iterator = sr.ReadLine()) != null)
                {
                    if (@iterator != string.Empty)
                    {

                    }
                }
                sr.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Ошибка чтения файла лабиринта " + ex.Message);
                return false;
            }
            return true;

        }

        #endregion

       
    }
}
