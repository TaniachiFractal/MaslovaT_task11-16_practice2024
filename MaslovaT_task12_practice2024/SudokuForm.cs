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

        /// <summary>
        /// The slash in the sudoku
        /// </summary>
        const byte NEWSTRING = 255;
        /// <summary>
        /// Other symbols in the sudoku file
        /// </summary>
        const byte OTHERSYMBOL = 127;
        /// <summary>
        /// Empty cells in the sudoku
        /// </summary>
        const byte EMPTY_DIGIT = 0;

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
        static string sudokuFileLocation = string.Empty;

        /// <summary>
        /// Path to the file that saves previous state
        /// </summary>
        static string initFileLocation = string.Empty;

        #endregion

        #endregion

        #region Form Events

        /// <summary>
        /// Load the previous game or do nothing if the file doesn't exist
        /// </summary>
        static bool LoadPreviousGame()
        {
            if (!File.Exists(initFileLocation)) return false;

            try
            {
                StreamReader streamReader = new StreamReader(initFileLocation);
                string newlyReadLine;

                sudokuFileLocation = streamReader.ReadLine();
                ReadSudoku();

                while ((newlyReadLine = streamReader.ReadLine()) != null)
                {
                    int stringIterator = 0;
                    if (newlyReadLine != string.Empty)
                    {
                        for (int j = 0; j < FLD_SZ; j++)
                        {
                            sudoku[stringIterator, j].digit = (byte)(int.Parse(newlyReadLine[j].ToString()));
                            stringIterator++;
                        }
                    }
                }
                streamReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла состояния предыдущей игры: \n" + ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sudokuFileLocation = string.Empty;
                return false;
            }

            UpdateGameField();
            return true;

        }

        /// <summary>
        /// Load the form
        /// </summary>
        private void SudokuForm_Load(object sender, EventArgs e)
        {
            InitSizeVars();

            initFileLocation = Directory.GetCurrentDirectory() + @"/init.txt";

            if (!LoadPreviousGame())
            {
                InitEmptySudoku();
            }

            OpenSudokuFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            DrawGameField();
        }

        /// <summary>
        /// Save previous data
        /// </summary>
        private void SudokuFormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sudokuFileLocation!=string.Empty)
            {

                DialogResult result = MessageBox.Show("Хотите сохранить состояние игры?", "ВНИМАНИЕ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SaveState();
                }
            }
        }
        /// <summary>
        /// Save game state into an init file
        /// </summary>
        static void SaveState()
        {
            string initFileText = string.Empty;

            initFileText += sudokuFileLocation + Environment.NewLine;

            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    initFileText += sudoku[i, j];
                }
                initFileText += Environment.NewLine;
            }

            FileInfo initFileInfo = new FileInfo(initFileLocation);
            try
            {
                StreamWriter sw = initFileInfo.CreateText();
                sw.Write(initFileText);
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи файла состояния: \n" + ex.ToString(), "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            ReadSudoku();
            UpdateGameField();
        }

        /// <summary>
        /// Show how to format the sudoku file
        /// </summary>
        private void BtHelp_FileFormat_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        /// <summary>
        /// Input Check
        /// </summary>
        private void TBdigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if ((sender as TextBox).Text.Length > 0)
            {
                e.Handled = true;
            }

            if (!char.IsDigit(e.KeyChar) || e.KeyChar=='0')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Main update
        /// </summary>
        private void TBdigit_TextChanged(object sender, EventArgs e)
        {
            UpdateSudoku();
            UpdateGameField();
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

                    currTB.Font = new Font(this.Font.FontFamily, (int)Math.Floor(CELL_SZ / 1.9), FontStyle.Regular);
                    currTB.TextAlign = HorizontalAlignment.Center;

                    currTB.ReadOnly = sudoku[i, j].locked;
                    currTB.Text = sudoku[i, j].ToString();

                    currTB.KeyPress += TBdigit_KeyPress;

                    Controls.Add(textBoxArray[i, j]);

                    if (j % 3 == 2) paddingX += PADDING_SMALL;
                }
                if (i % 3 == 2) paddingY += PADDING_SMALL + 2;
            }

        }

        /// <summary>
        /// Draw other controls based on cell size
        /// </summary>
        private void RenderOtherControls()
        {
            btLoadSudoku.Location = new System.Drawing.Point(PADDING_BIG, PADDING_BIG);
            btLoadSudoku.Size = new System.Drawing.Size((CELL_SZ * FLD_SZ + PADDING_SMALL * 2) / 2 - PADDING_BIG, (int)(CELL_SZ * 1.5));
            btLoadSudoku.Font = new Font(this.Font.FontFamily, (int)Math.Floor(CELL_SZ /2.5), FontStyle.Regular);

            btHelp_FileFormat.Location = new System.Drawing.Point(btLoadSudoku.Right + PADDING_BIG*2, PADDING_BIG);
            btHelp_FileFormat.Size = btLoadSudoku.Size;
            btHelp_FileFormat.Font = btLoadSudoku.Font;

            this.Size = new System.Drawing.Size(CELL_SZ * FLD_SZ + PADDING_BIG * 4,
                30 + PADDING_BIG * 4 + PADDING_SMALL * 3 + CELL_SZ * FLD_SZ + btLoadSudoku.Height);
        }

        /// <summary>
        /// Read the sudoku array and update the text boxes
        /// </summary>
        static void UpdateGameField()
        {
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    textBoxArray[i, j].Text = sudoku[i, j].ToString();
                    textBoxArray[i, j].ReadOnly = sudoku[i, j].locked;

                    if (textBoxArray[i, j].ReadOnly)
                        textBoxArray[i, j].BackColor = Color.Gainsboro;
                }
            }
        }

        /// <summary>
        /// Make incorrect numbers red, fully correct - green
        /// </summary>
        private void CheckInputAndColor()
        {

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

        /// <summary>
        /// Read sudoku file and put into the data array
        /// </summary>
        static bool ReadSudoku()
        {
            try
            {
                StreamReader streamReader = new StreamReader(sudokuFileLocation);
                string newlyReadLine;
                int stringCounter = 0;
                byte sudokuIterator;
                while ((newlyReadLine = streamReader.ReadLine()) != null)
                {
                ReadNewLine:
                    if (newlyReadLine != string.Empty)
                    {
                        sudokuIterator = 0;
                        for (int stringIterator = 0; stringIterator < newlyReadLine.Length; stringIterator++)
                        {
                            byte newlyReadDigit = CharToDigit(newlyReadLine[stringIterator]);
                            if (newlyReadDigit == OTHERSYMBOL)
                            {
                                continue;
                            }
                            else if (newlyReadDigit == NEWSTRING)
                            {
                                newlyReadLine = streamReader.ReadLine();
                                goto ReadNewLine;
                            }
                            else
                            {
                                sudoku[stringCounter, sudokuIterator] = new SudokuElement(newlyReadDigit, newlyReadDigit != 0);
                                sudokuIterator++;
                            }
                        }
                        stringCounter++;
                        if (stringCounter > 8) break;

                    }
                }
                streamReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла судоку: \n" + ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

        /// <summary>
        /// Return digit or other symbol ID
        /// </summary>
        static byte CharToDigit(char ch)
        {
            if (ch == '/' || ch == '\\') return NEWSTRING;
            if (ch == '-') return EMPTY_DIGIT;
            if (char.IsDigit(ch)) return (byte)(ch - 0x30);
            return OTHERSYMBOL;
        }

        /// <summary>
        /// Put textbox data into sudoku
        /// </summary>
        static void UpdateSudoku()
        {
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    sudoku[i, j].digit = (byte)int.Parse(textBoxArray[i, j].Text);
                }
            }
        }

        #endregion

        #region sudoku check

        /// <summary>
        /// Returns the start row/cell of a cell by any digit coords in it: (5,2)->(0,3)  (1,4)->(3,0)
        /// </summary>
        static byte StartOfCell(byte row_cell)
        {
            return (byte)(row_cell - row_cell % 3);
        }

        /// <summary>
        /// True if the newly generated digit is already in the row
        /// </summary>
        static bool DigitAlreadyIn_Row(byte[,] sudoku, byte row, byte digit)
        {
            for (byte i = 0; i < 9; i++)
            {
                if (sudoku[row, i] == digit)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the col
        /// </summary>
        static bool DigitAlreadyIn_Col(byte[,] sudoku, byte col, byte digit)
        {
            for (byte i = 0; i < 9; i++)
            {
                if (sudoku[i, col] == digit)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the cell
        /// </summary>
        static bool DigitAlreadyIn_Cell(byte[,] sudoku, byte row, byte col, byte digit)
        {
            byte startCol = StartOfCell(col);
            byte startRow = StartOfCell(row);

            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                    if (sudoku[i, j] == digit)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the row, col or cell
        /// </summary>
        static bool DigitIsWrong(byte[,] sudoku, byte row, byte col, byte digit)
        {
            return DigitAlreadyIn_Cell(sudoku, row, col, digit) || DigitAlreadyIn_Col(sudoku, col, digit) || DigitAlreadyIn_Row(sudoku, row, digit);
        }

        #endregion
    }
}
