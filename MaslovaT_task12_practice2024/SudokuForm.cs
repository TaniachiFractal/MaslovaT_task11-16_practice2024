﻿using System;
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

        #region Save State

        /// <summary>
        /// Load the previous game or do nothing if the file doesn't exist
        /// </summary>
        private bool LoadPreviousGame()
        {
            if (!File.Exists(initFileLocation)) return false;

            int stringIterator = 0;
            try
            {
                StreamReader streamReader = new StreamReader(initFileLocation);
                string newlyReadLine;

                sudokuFileLocation = streamReader.ReadLine();
                ReadSudoku();

                while ((newlyReadLine = streamReader.ReadLine()) != null)
                {
                    if (newlyReadLine != string.Empty)
                    {
                        for (int j = 0; j < FLD_SZ; j++)
                        {
                            sudoku[stringIterator, j].digit = (byte)(int.Parse(newlyReadLine[j].ToString()));
                        }
                        stringIterator++;
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

            return true;

        }

        /// <summary>
        /// Save previous data
        /// </summary>
        private void SudokuFormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sudokuFileLocation != string.Empty)
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
                    initFileText += sudoku[i, j].digit;
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

        #endregion

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
            UpdateGameField();
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
        /// Reset the game and delete init file
        /// </summary>
        private void BtReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Точно удалить файл сохранения игры и очистить поле? " +
                "Это действие нельзя будет отменить.", "ВНИМАНИЕ", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.OK)
            {
                File.Delete(initFileLocation);
                sudokuFileLocation = string.Empty;
                Application.Restart();
            }
        }

        #region TBdigit events

        /// <summary>
        /// Input Check and sudoku update
        /// </summary>
        private void TBdigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            char digit = e.KeyChar;

            if (char.IsControl(digit)) return;

            if (tb.Text.Length > 0)
            {
                e.Handled = true;
            }

            if (!char.IsDigit(digit) || digit == '0')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Update sudoku
        /// </summary>
        private void TBdigit_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            byte row = (byte)(tb.Name[2] - 0x30);
            byte col = (byte)(tb.Name[4] - 0x30);
            char digit = '0';
            if (tb.Text.Length > 0)
                digit = tb.Text[0];

            UpdateSudoku(row, col, digit);
            UpdateGameField();
        }

        #endregion

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
                    currTB.KeyUp += TBdigit_KeyUp;

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
            btLoadSudoku.Size = new System.Drawing.Size((CELL_SZ * FLD_SZ + PADDING_SMALL * 2) / 3 - PADDING_BIG, (int)(CELL_SZ * 1.5));
            btLoadSudoku.Font = new Font(this.Font.FontFamily, (int)Math.Floor(CELL_SZ / 2.6), FontStyle.Regular);

            btReset.Location = new System.Drawing.Point(btLoadSudoku.Right + PADDING_BIG + PADDING_SMALL * 2, PADDING_BIG);
            btReset.Size = btLoadSudoku.Size;
            btReset.Font = btLoadSudoku.Font;

            btHelp_FileFormat.Location = new System.Drawing.Point(btReset.Right + PADDING_BIG + PADDING_SMALL * 2, PADDING_BIG);
            btHelp_FileFormat.Size = btLoadSudoku.Size;
            btHelp_FileFormat.Font = btLoadSudoku.Font;

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
            CheckWrongInputAndColor();
            if (ColorGreenCells_AndCheckWin())
            {
                MessageBox.Show("Вы победили!", "ПОБЕДА", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.Delete(initFileLocation);
                sudokuFileLocation = string.Empty;
                Application.Restart();
            }
        }

        #region color

        /// <summary>
        /// Make incorrect numbers red, unsolved - white
        /// </summary>
        private void CheckWrongInputAndColor()
        {
            for (byte i = 0; i < FLD_SZ; i++)
            {
                for (byte j = 0; j < FLD_SZ; j++)
                {
                    byte currDigit = sudoku[i, j].digit;
                    byte startCol = StartOfCell(j);
                    byte startRow = StartOfCell(i);

                    if (currDigit != 0)
                    {
                        // Clean
                        if (!DigitAlreadyIn_Col(sudoku, i, j, currDigit) ||
                            !DigitAlreadyIn_Col(sudoku, i, j, currDigit) ||
                            !DigitAlreadyIn_Col(sudoku, i, j, currDigit))
                        {
                            ColorDigit(i, j, Color.Gainsboro, Color.White);
                        }

                        // Wrong
                        if (DigitAlreadyIn_Col(sudoku, i, j, currDigit) ||
                            DigitAlreadyIn_Row(sudoku, i, j, currDigit) ||
                            DigitAlreadyIn_Cell(sudoku, i, j, currDigit))
                        {
                            ColorDigit(i, j, Color.Maroon, Color.OrangeRed);
                        }
                    }
                    else
                    {
                        ColorDigit(i, j, Color.Gainsboro, Color.White);
                    }
                }
            }

        }

        /// <summary>
        /// Color all cells green and check win
        /// </summary>
        private bool ColorGreenCells_AndCheckWin()
        {
            byte greenCells = 0;

            for (byte i = 0; i < FLD_SZ; i+=3)
            {
                for (byte j = 0; j < FLD_SZ; j+=3)
                {
                    if (CellFullyCorrect(i, j))
                    {
                        ColorCell(i, j, Color.OliveDrab, Color.LightGreen);
                        greenCells++;
                    }
                }
            }

            return greenCells == 9;
        }

        /// <summary>
        /// Color a sudoku cell
        /// </summary>
        private void ColorCell(byte startRow, byte startCol, Color lockedDigitColor, Color normalDigitColor)
        {
            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                    if (sudoku[i, j].locked) textBoxArray[i, j].BackColor = lockedDigitColor;
                    else textBoxArray[i, j].BackColor = normalDigitColor;
                }
            }
        }

        /// <summary>
        /// Color a sudoku digit
        /// </summary>
        private void ColorDigit(byte row, byte col, Color lockedDigitColor, Color normalDigitColor)
        {
            if (sudoku[row, col].locked) textBoxArray[row, col].BackColor = lockedDigitColor;
            else textBoxArray[row, col].BackColor = normalDigitColor;
        }

        #endregion

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
        private bool ReadSudoku()
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
        private void UpdateSudoku(byte row, byte col, char digit)
        {
            sudoku[row, col].digit = CharToDigit(digit);
        }

        static string Byte2DarrToString(SudokuElement[,] arr)
        {
            string output = string.Empty;
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    output += arr[i, j].digit + " ";
                }
                if (i < FLD_SZ - 1)
                    output += Environment.NewLine;
            }
            return output;
        }

        #endregion

        #region sudoku check

        /// <summary>
        /// True if cell is complete and has no repeating numbers
        /// </summary>
        private bool CellFullyCorrect(byte startRow, byte startCol)
        {
            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                    if (sudoku[i, j].digit == 0) return false;
                    if (DigitIsWrong(sudoku, i, j, sudoku[i, j].digit)) return false;
                }
            }
            return true;
        }

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
        private bool DigitAlreadyIn_Row(SudokuElement[,] sudoku, byte row, byte col, byte digit)
        {
            for (byte i = 0; i < FLD_SZ; i++)
            {
                if (i == col) continue;
                if (sudoku[row, i].digit == digit)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the col
        /// </summary>
        static bool DigitAlreadyIn_Col(SudokuElement[,] sudoku, byte row, byte col, byte digit)
        {
            for (byte i = 0; i < 9; i++)
            {
                if (i == row) continue;
                if (sudoku[i, col].digit == digit)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the cell
        /// </summary>
        static bool DigitAlreadyIn_Cell(SudokuElement[,] sudoku, byte row, byte col, byte digit)
        {
            if (digit == 0) return false;

            byte startCol = StartOfCell(col);
            byte startRow = StartOfCell(row);

            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                    if (i == row && j == col) continue;
                    if (sudoku[i, j].digit == digit)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the row, col or cell
        /// </summary>
        private bool DigitIsWrong(SudokuElement[,] sudoku, byte row, byte col, byte digit)
        {
            return DigitAlreadyIn_Cell(sudoku, row, col, digit) || DigitAlreadyIn_Col(sudoku, row, col, digit) || DigitAlreadyIn_Row(sudoku, row, col, digit);
        }

        #endregion

    }
}
