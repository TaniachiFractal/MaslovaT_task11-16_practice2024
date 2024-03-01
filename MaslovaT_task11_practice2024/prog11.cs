using System;

namespace MaslovaT_task11_practice2024
{
    internal class Program
    {
        static void Main()
        {
            Sudoku sudoku = new();

            #region input
        Retype:
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите сложность от 0 до 10: \n >_ ");
            int difficulty = 0;
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                difficulty = int.Parse(Console.ReadLine());
                if (difficulty < 0 || difficulty > 10) { throw new Exception(); }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка ввода");
                goto Retype;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nСохраните это в .txt файл и откройте \"Судокером\" (задание 12)");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sudoku.ToStringSudoker((double)difficulty/10));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Показать решение? Нажмите любую клавишу");
            Console.ReadKey(true);
            Console.ForegroundColor = ConsoleColor.Cyan;

            #endregion

            Console.WriteLine(sudoku.ToString());
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Sudoku puzzle class
    /// </summary>
    class Sudoku
    {
        /// <summary>
        /// Main data
        /// </summary>
        byte[,] sudoku = new byte[9, 9];

        /// <summary>
        /// Constructor and generator
        /// </summary>
        public Sudoku()
        {
            FillCell(0, 0);
            FillCell(3, 3);
            FillCell(6, 6);

            FillSudoku(0, 3);
        }

        /// <returns>Nicely formatted sudoku </returns>
        public override string ToString()
        {
            string output = String.Empty;

            string GetHorizontalDivider()
            {
                string outDiv = string.Empty;
                outDiv += (" ┼");
                for (byte j = 0; j < 18; j++)
                {
                    outDiv += ('─');
                    if (j % 6 == 5)
                        outDiv += ('┼');
                }
                outDiv += Environment.NewLine;
                return outDiv;
            }

            output += Environment.NewLine;
            output += GetHorizontalDivider();
            for (byte i = 0; i < 9; i++)
            {
                output += (" │");
                for (byte j = 0; j < 9; j++)
                {
                    output += (sudoku[i, j].ToString() + ' ');
                    if (j % 3 == 2)
                        output += ('│');
                }
                output += Environment.NewLine;
                if (i % 3 == 2)
                {
                    output += GetHorizontalDivider();
                }
            }
            return output;
        }

        /// <param name="difficulty">0-fully solved, 1-empty; 0.1 - easiest, 0.9 - hardest</param>
        /// <returns>Sudoker compatible sudoku</returns>
        public string ToStringSudoker(double difficulty)
        {
            Random rnd = new();
            string output = String.Empty;
            output += Environment.NewLine;
            for (byte i = 0; i < 9; i++)
            {
                for (byte j = 0; j < 9; j++)
                {
                    if (rnd.NextDouble() > difficulty)
                        output += (sudoku[i, j].ToString());
                    else
                        output += "-";
                    if (j % 3 == 2) output += " ";
                }
                output += Environment.NewLine;
                if (i % 3 == 2)
                {
                    output += "/" + Environment.NewLine;
                }
            }
            return output;
        }

        /// <summary>
        /// Fill one digit correctly, call itself once again;
        /// <para>Finished - return true, failed - return false</para>
        /// </summary>
        bool FillSudoku(int row, int col)
        {
            if (col == 9)
            {
                if (++row == 9)
                    return true; // Finished
                col = 0; // Next line
            }

            if (sudoku[row, col] != 0) // If digit is not empty, go to the next one
            {
                return FillSudoku(row, col + 1);
            }

            for (byte newDigit = 1; newDigit < 10; newDigit++)
            {
                if (!DigitIsWrong((byte)row, (byte)col, newDigit))
                {
                    sudoku[row, col] = newDigit; // Write new digit

                    if (FillSudoku(row, col + 1)) // Check finish conditions
                        return true; //Finished

                    sudoku[row, col] = 0; // If finish conditions are not met, erase cell
                }

            }

            return false;

        }

        /// <summary>
        /// Randomly fill 1 cell
        /// </summary>
        void FillCell(byte startRow, byte startCol)
        {
            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                ReGen:
                    byte tmpDigit = GenerateDigit();
                    if (DigitAlreadyIn_Cell(i, j, tmpDigit)) goto ReGen;
                    sudoku[i, j] = tmpDigit;
                }
            }
        }

        /// <summary>
        /// Randomly returns any digit 0 - 8
        /// </summary>
        byte GenerateDigit()
        {
            Random rnd = new Random();
            return (byte)rnd.Next(1, 10);
        }

        #region Check

        /// <summary>
        /// Returns the start row/cell of a cell by any digit coords in it: (5,2)->(0,3)  (1,4)->(3,0)
        /// </summary>
        byte StartOfCell(byte row_cell)
        {
            return (byte)(row_cell - row_cell % 3);
        }

        /// <summary>
        /// True if the newly generated digit is already in the row
        /// </summary>
        bool DigitAlreadyIn_Row(byte row, byte digit)
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
        bool DigitAlreadyIn_Col(byte col, byte digit)
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
        bool DigitAlreadyIn_Cell(byte row, byte col, byte digit)
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
        bool DigitIsWrong(byte row, byte col, byte digit)
        {
            return DigitAlreadyIn_Cell(row, col, digit) || DigitAlreadyIn_Col(col, digit) || DigitAlreadyIn_Row(row, digit);
        }

        #endregion
    }
}
