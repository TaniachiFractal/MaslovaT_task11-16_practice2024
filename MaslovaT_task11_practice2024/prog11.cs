using System;

namespace MaslovaT_task11_practice2024
{
    internal class Program
    {
        /// <summary>
        /// Main array
        /// </summary>
        static byte[,] sudoku = new byte[9, 9];

        static void Main()
        {
            FillSudoku(sudoku);

            Console.ForegroundColor = ConsoleColor.Cyan;
            OutputSudoku(sudoku);

            Console.ReadKey();
        }

        #region done

        /// <summary>
        /// Randomly returns any digit 0 - 8
        /// </summary>
        /// <returns></returns>
        static byte GenerateDigit()
        {
            Random rnd = new Random();
            return (byte)rnd.Next(1, 10);
        }

        /// <summary>
        /// Randomly fill 1 cell
        /// </summary>
        static void FillCell(byte[,] sudoku, byte startRow, byte startCol)
        {
            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                ReGen:
                    byte tmpDigit = GenerateDigit();
                    if (DigitAlreadyIn_Cell(sudoku, i, j, tmpDigit)) goto ReGen;
                    sudoku[i, j] = tmpDigit;
                }
            }
        }

        /// <summary>
        /// Fill cell with 0
        /// </summary>
        static void EraseCell(byte[,] sudoku, byte startRow, byte startCol)
        {
            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                    sudoku[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Generate a string of non-repeating digits
        /// </summary>
        static byte[] RandomDigitString()
        {
            byte[] output = new byte[9];
            for (byte i = 0; i < 9; i++)
            {
            ReGenDigit:
                byte tmpDigit = GenerateDigit();
                for (byte j = 0; j < i; j++)
                {
                    if (output[j] == tmpDigit)
                        goto ReGenDigit;
                }
                output[i] = tmpDigit;
                Console.Write(tmpDigit);
            }
            return output;
        }

        /// <summary>
        /// Write sudoku byteo the console
        /// </summary>
        static void OutputSudoku(byte[,] sudoku)
        {
            void WriteHorizontalDivider()
            {
                Console.Write(" ┼");
                for (byte j = 0; j < 18; j++)
                {
                    Console.Write('─');
                    if (j % 6 == 5)
                        Console.Write('┼');
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            WriteHorizontalDivider();
            for (byte i = 0; i < 9; i++)
            {
                Console.Write(" │");
                for (byte j = 0; j < 9; j++)
                {
                    Console.Write(sudoku[i, j].ToString() + ' ');
                    if (j % 3 == 2)
                        Console.Write('│');
                }
                Console.WriteLine();
                if (i % 3 == 2)
                {
                    WriteHorizontalDivider();
                }
            }
        }

        /// <summary>
        /// Generate random empty cell
        /// </summary>
        static BytePoint GenerateCoord(byte[,] sudoku)
        {
            Random rnd = new Random();
        ReGenPoint:
            byte row = (byte)rnd.Next(0, 9);
            byte col = (byte)rnd.Next(0, 9);
            if (sudoku[row, col] != 0)
            {
                goto ReGenPoint;
            }
            return new BytePoint(row, col);
        }

        static void SetRandomConsoleColor()
        {
            Random rnd = new();
            int possibleColor = rnd.Next(1, 14);
            if (possibleColor == 11 || Console.ForegroundColor == (ConsoleColor)possibleColor)
            {
                possibleColor++;
            }
            Console.ForegroundColor = (ConsoleColor)possibleColor;
        }

        #endregion

        #region Check

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

        /// <summary>
        /// Fill sudoku with numbers
        /// </summary>
        static void FillSudoku(byte[,] sudoku)
        {
            #region fill three cells in the main diagonal
            FillCell(sudoku, 0, 0);
            FillCell(sudoku, 3, 3);
            FillCell(sudoku, 6, 6);
        #endregion

        RestartFill:
            #region empty other cells
            EraseCell(sudoku, 0, 3);
            EraseCell(sudoku, 0, 6);
            EraseCell(sudoku, 3, 6);

            EraseCell(sudoku, 3, 0);
            EraseCell(sudoku, 6, 0);
            EraseCell(sudoku, 6, 3);
            #endregion

            for (byte i = 0; i < 9; i++)
            {
                for (byte j = 0; j < 9; j++)
                {
                    BytePoint newCoord = GenerateCoord(sudoku);
                    byte[] possibleDigits = RandomDigitString();

                    int iter;
                    for (iter = 0; iter < 9; iter++)
                    {
                        if (!DigitIsWrong(sudoku, newCoord.col, newCoord.row, possibleDigits[iter]))
                        {
                            sudoku[newCoord.col, newCoord.row] = possibleDigits[iter];
                            break;
                        }
                    }
                    OutputSudoku(sudoku);
                    if (iter == 8)
                    {
                        SetRandomConsoleColor();
                        goto RestartFill;
                    }
                    iter = 0;
                }
            }
            for (byte i = 0; i < 9; i++)
            {
                for (byte j = 0; j < 9; j++)
                {
                    if (sudoku[i, j] == 0)
                        goto RestartFill;
                }
            }
        }




    }
}
