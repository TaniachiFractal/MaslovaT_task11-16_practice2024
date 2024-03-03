using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MaslovaT_task13_practice2024
{
    public partial class FoxesAndChickens : Form
    {
        #region Form constructor
        public FoxesAndChickens()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables and constants

        #region consts

        const byte CHICKEN_ID = 1;
        const byte FOX_ID = 2;
        const byte FOX_ID2 = 2;
        const byte FOX_ID3 = 3;
        const byte EMPTYCELL = 0;
        const byte NONEXISTENTCELL = 9;
        /// <summary>
        /// The size of the main game grid field
        /// </summary>
        const int FLD_SZ = 7;

        readonly Bitmap chickenImg = Properties.Resources.chicken80x80;
        readonly Bitmap foxImg = Properties.Resources.fox80x80;
        readonly Bitmap noImage = Properties.Resources.empty;

        private readonly Random rnd = new Random();
        #endregion

        #region arrays

        /// <summary>
        /// Matrix that contains buttons to click on
        /// </summary>
        static private Button[,] gameFieldButtonArray = new Button[FLD_SZ, FLD_SZ];
        /// <summary>
        /// "Brother" of gameFieldButtonArray that holds actual data of the game state
        /// </summary>
        static private byte[,] gameFieldDataArray = new byte[FLD_SZ, FLD_SZ];

        #endregion

        #region vars

        /// <summary>
        /// Currently selected cell
        /// </summary>
        static Point selectedCell = new Point(0, 0);
        /// <summary>
        /// Previously selected Cell
        /// </summary>
        static Point previousCell = new Point(0, 0);

        static int CELL_SIZE = 90;

        #endregion

        #endregion

        #region Form events

        /// <summary>
        /// Show help window
        /// </summary>
        private void BtHelp_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        /// <summary>
        /// Loading the form
        /// </summary>
        private void FoxesAndChickens_Load(object sender, EventArgs e)
        {
            CELL_SIZE = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 10;

            FillDataArrayDefault();
            DrawGameField();
            RenderChickenCount();
        }

        /// <summary>
        /// Click event handler: selecting cells, checking if moving is possible, moving chickens and foxes, checking winning/loosing conditions
        /// </summary>
        private void GridButton_Click(object sender, EventArgs e)
        {

            Button currBt = sender as Button;

            int row = Int32.Parse(currBt.Name[2].ToString());
            int col = Int32.Parse(currBt.Name[4].ToString());

            if (gameFieldDataArray[row, col] > CHICKEN_ID)
            {
                MessageBox.Show("В выбранной клетке лиса!", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetPrevAndSelectedCells();
                return;
            }

            previousCell = selectedCell;

            selectedCell.Y = row;
            selectedCell.X = col;

            MoveChickenAndFox(previousCell, selectedCell);


            UpdateGameField();

            if (HasWon())
            {
                YouWonForm youWonForm = new YouWonForm();
                youWonForm.ShowDialog();
            }

            if (HasLost())
            {
                YouLostForm youLostForm = new YouLostForm();
                youLostForm.ShowDialog();
            }
        }

        #endregion

        #region Working with data grid

        /// <summary>
        /// Setup the default game layout
        /// </summary>
        private void FillDataArrayDefault()
        {
            for (int i = 0; i < FLD_SZ; i++)
                for (int j = 0; j < FLD_SZ; j++)
                {
                    if (ButtonShouldExist(i, j))
                        gameFieldDataArray[i, j] = EMPTYCELL;
                    else
                        gameFieldDataArray[i, j] = NONEXISTENTCELL;
                }
            // Set Chickens
            for (int i = 3; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    if (ButtonShouldExist(i, j))
                        gameFieldDataArray[i, j] = CHICKEN_ID;
                }
            }

            gameFieldDataArray[2, 2] = FOX_ID2;
            gameFieldDataArray[2, 4] = FOX_ID3;

        }

        /// <summary>
        /// Returns how many chickens are left
        /// </summary>
        private int CountChickens()
        {
            int count = 0;
            for (int i = 0; i < FLD_SZ; i++)
                for (int j = 0; j < FLD_SZ; j++)
                    if (gameFieldDataArray[i, j] == CHICKEN_ID)
                        count++;
            return count;
        }

        /// <summary>
        /// Move chicken from one cell to another, if moved chicken - move Fox
        /// </summary>
        private void MoveChickenAndFox(Point prevCell, Point nextCell)
        {

            if (prevCell.X == 0 && prevCell.Y == 0) // If the previous cell is default, no moving is done
            {
                return;
            }
            else if (prevCell == nextCell) // Deselect cell if it's pressed twice
            {
            }
            else if (gameFieldDataArray[prevCell.Y, prevCell.X] == EMPTYCELL) // Deselect cell if it's empty
            {
            }
            else if (gameFieldDataArray[nextCell.Y, nextCell.X] != EMPTYCELL) // Check if the destination cell is not empty
            {
                MessageBox.Show("Та клетка занята!", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (prevCell.Y < nextCell.Y) // Check if player is trying to go back
            {
                MessageBox.Show("Назад ходить нельзя!", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Math.Abs(prevCell.Y - nextCell.Y) > 1 || Math.Abs(prevCell.X - nextCell.X) > 1) // Check if player is trying to go too far
            {
                MessageBox.Show("Можно ходить только на 1 клетку влево, вправо или вверх", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Math.Abs(prevCell.X - nextCell.X) == 1 && Math.Abs(prevCell.Y - nextCell.Y) == 1) // Check if player is trying to go diagonally
            {
                MessageBox.Show("По диагонали ходить нельзя!", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else // Move
            {
                gameFieldDataArray[prevCell.Y, prevCell.X] = EMPTYCELL;
                gameFieldDataArray[nextCell.Y, nextCell.X] = CHICKEN_ID;

                MoveFox();
            }
            ResetPrevAndSelectedCells();
        }

        /// <summary>
        /// Check winning conditions: top 9 cells are filled with chickens
        /// </summary>
        private bool HasWon()
        {
            int winningChickenCounter = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 2; j < 5; j++)
                {
                    if (gameFieldDataArray[i, j] == CHICKEN_ID)
                    {
                        winningChickenCounter++;
                    }
                }
            }
            return winningChickenCounter == 9;
        }

        /// <summary>
        /// Check loosing conditions: if there are less than 9 chickens left, it is impossible to win
        /// </summary>
        private bool HasLost()
        {
            return CountChickens() < 9;
        }

        /// <summary>
        /// Make previous and selected cells both 0
        /// </summary>
        private void ResetPrevAndSelectedCells()
        {
            selectedCell = new Point(0, 0);
            previousCell = new Point(0, 0);
        }

        /// <summary>
        /// Get data from byte array in text format
        /// </summary>
        static string Byte2DarrToString(byte[,] arr)
        {
            string output = string.Empty;
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    output += arr[i, j] + " ";
                }
                if (i < FLD_SZ - 1)
                    output += Environment.NewLine;
            }
            return output;
        }

        #endregion

        #region Moving Foxes - done not by me.

        /// <summary>
        /// Returns the entity at a cell or nonexistent cell
        /// </summary>
        private byte WhatIsInCell(int row, int col)
        {
            if (row < 0 || row >= FLD_SZ || col < 0 || col >= FLD_SZ)
                return NONEXISTENTCELL;
            else
                return gameFieldDataArray[row, col];
        }

        /// <summary>
        /// Find a fox across the field
        /// </summary>
        private (int row, int col) FindFox(byte foxId)
        {
            for (int row = 0; row < FLD_SZ; row++)
            {
                for (int col = 0; col < FLD_SZ; col++)
                {
                    if (gameFieldDataArray[row, col] == foxId)
                        return (row, col);
                }
            }
            return (-1, -1);
        }

        /// <summary>
        /// Find the longest gobble chain
        /// </summary>
        private void FindFoxLongestGobbleChain(byte foxId, List<(int row, int col)> path)
        {
            // current fox position
            var (row, col) = path[path.Count - 1];

            // perspective paths right, left, down and up
            List<(int row, int col)>[] AllDirections = new List<(int row, int col)>[]
            {
                new List<(int row, int col)>(path),
                new List<(int row, int col)>(path),
                new List<(int row, int col)>(path),
                new List<(int row, int col)>(path),
            };

            // checks whether the fox can jump to a cell
            bool CanJump(int testRow, int testCol)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    var (r0, c0) = path[i];
                    var (r1, c1) = path[i + 1];

                    if (r0 == row || c0 == col || r1 == testRow || c1 == testCol)
                        return false;

                    (r0, c0) = path[path.Count - i - 1];
                    (r1, c1) = path[path.Count - i - 2];

                    if (r0 == row || c0 == col || r1 == testRow || c1 == testCol)
                        return false;
                }

                var c = WhatIsInCell(testRow, testCol);
                return c == EMPTYCELL || c == foxId;
            }

            if (WhatIsInCell(row, col + 1) == CHICKEN_ID && CanJump(row, col + 2))
            {
                AllDirections[0].Add((row, col + 2));
                FindFoxLongestGobbleChain(foxId, AllDirections[0]);
            }

            if (WhatIsInCell(row, col - 1) == CHICKEN_ID && CanJump(row, col - 2))
            {
                AllDirections[1].Add((row, col - 2));
                FindFoxLongestGobbleChain(foxId, AllDirections[1]);
            }

            if (WhatIsInCell(row + 1, col) == CHICKEN_ID && CanJump(row + 2, col))
            {
                AllDirections[2].Add((row + 2, col));
                FindFoxLongestGobbleChain(foxId, AllDirections[2]);
            }

            if (WhatIsInCell(row - 1, col) == CHICKEN_ID && CanJump(row - 2, col))
            {
                AllDirections[3].Add((row - 2, col));
                FindFoxLongestGobbleChain(foxId, AllDirections[3]);
            }

            int index = 0;
            for (int i = 1; i < AllDirections.Length; i++)
            {
                if (AllDirections[i].Count > AllDirections[index].Count)
                    index = i;
                else if (AllDirections[i].Count == AllDirections[index].Count && rnd.NextDouble() < 0.5)
                    index = i;
            }

            path.Clear();
            path.AddRange(AllDirections[index]);
        }

        /// <returns>Some move for a fox</returns>
        private List<(int row, int col)> FindFoxMove(byte foxId, out bool Gobble)
        {
            List<(int row, int col)> output = new List<(int row, int col)>()
            {
                FindFox(foxId)
            };

            FindFoxLongestGobbleChain(foxId, output);

            Gobble = output.Count > 1;
            if (!Gobble)
            {

                List<int> AllDirections = new List<int>();

                var (row, col) = output[0];

                if (WhatIsInCell(row, col + 1) == EMPTYCELL)
                    AllDirections.Add(0);
                if (WhatIsInCell(row, col - 1) == EMPTYCELL)
                    AllDirections.Add(1);
                if (WhatIsInCell(row + 1, col) == EMPTYCELL)
                    AllDirections.Add(2);
                if (WhatIsInCell(row - 1, col) == EMPTYCELL)
                    AllDirections.Add(3);

                int index;
                if (AllDirections.Count == 0)
                {
                    output.Clear();
                    return output;
                }
                else if (AllDirections.Count == 1)
                {
                    index = AllDirections[0];
                }
                else
                {
                    AllDirections.Remove(3);
                    index = AllDirections[rnd.Next(AllDirections.Count)];
                }

                switch (index)
                {

                    case 0:
                        col++;
                        break;
                    case 1:
                        col--;
                        break;
                    case 2:
                        row++;
                        break;
                    case 3:
                        row--;
                        break;
                }
                output.Add((row, col));
            }
            return output;

        }

        /// <returns>The move that will be done</returns>
        private List<(int row, int col)> DecideFoxMove()
        {
            List<(int row, int col)>
                foxPath2 = FindFoxMove(FOX_ID2, out bool Gobble2),
                foxPath3 = FindFoxMove(FOX_ID3, out bool Gobble3);

            if (Gobble2)
            {
                if (Gobble3)
                {
                    if (foxPath2.Count == foxPath3.Count)
                    {
                        if (rnd.NextDouble() < 0.5)
                            return foxPath3;
                        else
                            return foxPath2;
                    }
                    else if (foxPath2.Count > foxPath3.Count)
                        return foxPath2;
                    else
                        return foxPath3;
                }
                else
                    return foxPath2;
            }
            else if (Gobble3)
                return foxPath3;
            else if (foxPath2.Count != 0)
            {
                if (foxPath3.Count != 0)
                {
                    if (rnd.NextDouble() < 0.5)
                        return foxPath3;
                    else
                        return foxPath2;
                }
                else
                    return foxPath2;
            }
            else if (foxPath3.Count != 0)
                return foxPath3;
            else
                return foxPath2;

        }

        /// <summary>
        /// Empty a cell
        /// </summary>
        private void EmptyCell(int row, int col)
        {
            gameFieldDataArray[row, col] = EMPTYCELL;
        }

        /// <summary>
        /// Move one of the foxes - done not by me.
        /// </summary>
        private void MoveFox()
        {

            List<(int row, int col)> move = DecideFoxMove();
            if (move.Count != 0)
            {
                string str = "";
                for (int i = 0; i < move.Count; i++)
                    str += move[i].row + "  " + move[i].col + "\r\n";
                var (row0, col0) = move[0];
                for (int i = 1; i < move.Count; i++)
                {
                    var (row1, col1) = move[i];

                    gameFieldDataArray[row1, col1] =
                        gameFieldDataArray[row0, col0];

                    EmptyCell(row0, col0);


                    int dc = col1 - col0, dr = row1 - row0;
                    if (dc != 0)
                    {
                        if (dc == 2)
                            EmptyCell(row1, col1 - 1);
                        else if (dc == -2)
                            EmptyCell(row1, col1 + 1);

                    }
                    else if (dr != 0)
                    {
                        if (dr == 2)
                            EmptyCell(row1 - 1, col1);
                        else if (dr == -2)
                            EmptyCell(row1 + 1, col1);

                    }
                    UpdateGameField();



                    (row0, col0) = (row1, col1);
                }
            }

        }

        #endregion

        #region Rendering window

        /// <summary>
        /// Draw the game field
        /// </summary>
        private void DrawGameField()
        {
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    gameFieldButtonArray[i, j] = new Button();
                    var currBt = gameFieldButtonArray[i, j];

                    currBt.Name = "bt" + i + '_' + j;
                    currBt.Location = new System.Drawing.Point(j * CELL_SIZE + 20, i * CELL_SIZE + 20);
                    currBt.Size = new System.Drawing.Size(CELL_SIZE, CELL_SIZE);
                    currBt.FlatStyle = FlatStyle.Flat;
                    if (IsGoalCell(i, j)) currBt.BackColor = Color.LightGreen;
                    else currBt.BackColor = Color.Honeydew;
                    currBt.Image = GetImage(i, j);
                    currBt.FlatAppearance.BorderColor = Color.White;
                    currBt.FlatAppearance.BorderSize = 1;
                    currBt.Click += GridButton_Click;

                    if (ButtonShouldExist(i, j))
                    {
                        Controls.Add(gameFieldButtonArray[i, j]);
                    }
                }
            }
            RenderOtherControls();
        }

        /// <summary>
        /// Draw new images on the field
        /// </summary>
        private void UpdateGameField()
        {
            for (int i = 0; i < FLD_SZ; i++)
            {
                for (int j = 0; j < FLD_SZ; j++)
                {
                    var currBt = gameFieldButtonArray[i, j];
                    currBt.Image = GetImage(i, j);


                    if (selectedCell.X == j && selectedCell.Y == i)
                    {
                        currBt.FlatAppearance.BorderColor = Color.Gray;
                        currBt.FlatAppearance.BorderSize = 3;
                    }
                    else
                    {
                        currBt.FlatAppearance.BorderColor = Color.White;
                        currBt.FlatAppearance.BorderSize = 1;
                    }
                }
            }
            RenderChickenCount();
        }

        /// <summary>
        /// Move and scale other controls according to game field dimens
        /// </summary>
        private void RenderOtherControls()
        {
            int gridSize = gameFieldButtonArray[FLD_SZ - 1, FLD_SZ - 1].Size.Width + gameFieldButtonArray[FLD_SZ - 1, FLD_SZ - 1].Location.X;
            this.Size = new System.Drawing.Size(gridSize + 40, gridSize + 60);

            lbTextChicksLeft.Top = 40 + CELL_SIZE * 5;
            lbChickenCount.Top = lbTextChicksLeft.Bottom + 10;

            pbChick.Size = new System.Drawing.Size(CELL_SIZE*2-20,CELL_SIZE*2-20);
            pbChick.Location = new System.Drawing.Point(CELL_SIZE * 5 + 40, 10);

            pbFox.Size = pbChick.Size;
            pbFox.Location = new System.Drawing.Point(CELL_SIZE * 5 + 40, CELL_SIZE * 5 + 40);
        }

        /// <summary>
        /// Output Chicken count
        /// </summary>
        private void RenderChickenCount()
        {
            int chickenCount = CountChickens();

            lbChickenCount.Text = chickenCount.ToString();

            if (chickenCount > 14)
                lbChickenCount.ForeColor = Color.LightGreen;
            else if (chickenCount > 12)
                lbChickenCount.ForeColor = Color.Gold;
            else
                lbChickenCount.ForeColor = Color.OrangeRed;

        }

        #region Methods for setting custom button data     

        /// <summary>
        /// False if the button in main grid should not exist (The field is a cross, not a full grid)
        /// </summary>
        private bool ButtonShouldExist(int row, int col)
        {
            // Buttons that should not exist
            int[] falseButtons = { 0,  1,  7,  8,
                                   5,  6,  12, 13,
                                   35, 36, 42, 43,
                                   40, 41, 47, 48 };
            foreach (var i in falseButtons)
            {
                if (row * FLD_SZ + col == i) return false;
            }
            return true;
        }

        /// <summary>
        /// Top 9 buttons are destination buttons
        /// </summary>
        private bool IsGoalCell(int row, int col)
        {
            int[] goalButtons = { 2, 3, 4, 9, 10, 11, 16, 17, 18 };
            foreach (var i in goalButtons)
            {
                if (row * FLD_SZ + col == i) return true;
            }
            return false;
        }

        /// <summary>
        /// Load an images to buttons according to data array
        /// </summary>
        private Bitmap GetImage(int row, int col)
        {
            var currBt = gameFieldButtonArray[row, col];
            switch (gameFieldDataArray[row, col])
            {
                case CHICKEN_ID:
                    return chickenImg;
                case FOX_ID2:
                case FOX_ID3:
                    return foxImg;
                case EMPTYCELL:
                    return noImage;
                default:
                    return noImage;
            }
        }

        #endregion

        #endregion

    }
}
