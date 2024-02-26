using System;
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
        const byte EMPTYCELL = 0;
        const byte NONEXISTENTCELL = 9;
        /// <summary>
        /// The size of the main game grid field
        /// </summary>
        const int FLD_SZ = 7;

        readonly Bitmap chickenImg = Properties.Resources.chicken80x80;
        readonly Bitmap foxImg = Properties.Resources.fox80x80;
        readonly Bitmap noImage = Properties.Resources.empty;

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

        /// <summary>
        /// Coordinates of foxes
        /// </summary>
        static Point[] foxesCoords = { new Point(0, 0), new Point(0, 0) };

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
            tbDebug.Text = Byte2DarrToString(gameFieldDataArray);
        }

        /// <summary>
        /// Click event handler: selecting cells, checking if moving is possible, moving chickens, checking winning/loosing conditions
        /// </summary>
        private void GridButton_Click(object sender, EventArgs e)
        {

            Button currBt = sender as Button;

            int row = Int32.Parse(currBt.Name[2].ToString());
            int col = Int32.Parse(currBt.Name[4].ToString());

            if (gameFieldDataArray[row, col] == FOX_ID)
            {
                MessageBox.Show("В выбранной клетке лиса!", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetPrevAndSelectedCells();
                return;
            }

            previousCell = selectedCell;

            selectedCell.Y = row;
            selectedCell.X = col;

            MoveChickenAndFox(previousCell, selectedCell);

            tbDebug.Text = Byte2DarrToString(gameFieldDataArray);

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
            // Set Foxes
            foxesCoords[0] = new Point(2, 2);
            foxesCoords[1] = new Point(4, 2);
            PutFoxesOnTheField();
        }

        /// <summary>
        /// Load the fox data to the gameFieldDataArray
        /// </summary>
        private void PutFoxesOnTheField()
        {
            gameFieldDataArray[foxesCoords[0].Y, foxesCoords[0].X] = gameFieldDataArray[foxesCoords[1].Y, foxesCoords[1].X] = FOX_ID;
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
            tbDebug2.Text = prevCell.ToString() + Environment.NewLine + nextCell.ToString();

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

               // MoveFox();
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

        #region Moving Foxes
    
        /// <summary>
        /// Move one of the foxes
        /// </summary>
        private void MoveFox()
        {
            Random rnd = new Random();
            int currFoxID = rnd.Next(0, 2);

            int foxRow = foxesCoords[currFoxID].Y;
            int foxCol = foxesCoords[currFoxID].X;

            DeleteFox(foxRow, foxCol);

            int destinationRow = 0;
            int destinationCol = 0;

            if (FoxCanEat(foxRow,foxCol))
            {
                destinationRow = ChickenToEat(foxRow, foxCol).X;
                destinationCol = ChickenToEat(foxRow, foxCol).Y;
            }
            else
            {
                destinationRow = MoveSomewhere(foxRow, foxCol).X;
                destinationCol = MoveSomewhere(foxRow, foxCol).Y;
            }

            foxesCoords[currFoxID].Y = destinationRow;
            foxesCoords[currFoxID].X = destinationCol;

            PutFoxesOnTheField();
        }
 
        /// <summary>
        /// Return an empty cell next to the fox
        /// </summary>
        private Point MoveSomewhere(int foxRow, int foxCol)
        {
            var dataArr = gameFieldDataArray;

            if      (dataArr[foxRow, foxCol + 1] == EMPTYCELL)
            {
                return new Point(foxRow, foxCol + 1);
            }


            else if (dataArr[foxRow + 1, foxCol] == EMPTYCELL)
            {
                return new Point(foxRow + 1, foxCol);
            }


            else if (dataArr[foxRow, foxCol - 1] == EMPTYCELL)
            {
                return new Point(foxRow, foxCol - 1);
            }


            else if (dataArr[foxRow - 1, foxCol] == EMPTYCELL)
            {
                return new Point(foxRow - 1, foxCol);
            }
            return new Point(foxRow,foxCol);
        }

        /// <summary>
        /// Check if the fox move is legal (Checker rules)
        /// </summary>
        private bool FoxCanMove(int foxRow, int foxCol, int destinationRow, int destinationCol)
        {
            if (gameFieldDataArray[destinationRow, destinationCol] != EMPTYCELL)
            {
                return false;
            }
            else if (destinationRow < 0 || destinationCol < 0 || destinationCol > 6 || destinationRow > 6)
            {
                return false;
            }
            else if (!ButtonShouldExist(destinationRow, destinationCol))
            {
                return false;
            }
            else if (Math.Abs(foxCol - destinationCol) == 1 && Math.Abs(foxRow - destinationRow) == 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check all possible eating directions
        /// </summary>
        private Point ChickenToEat(int foxRow, int foxCol)
        {
            var dataArr = gameFieldDataArray;

            if (dataArr[foxRow, foxCol + 1] == CHICKEN_ID)
            {
                if (dataArr[foxRow, foxCol + 2] == EMPTYCELL)
                {
                    return new Point(foxRow, foxCol + 1);
                }
            }


            else if (dataArr[foxRow + 1, foxCol] == CHICKEN_ID)
            {
                if (dataArr[foxRow + 2, foxCol] == EMPTYCELL)
                {
                    return new Point(foxRow + 1, foxCol);
                }
            }


            else if (dataArr[foxRow, foxCol - 1] == CHICKEN_ID)
            {
                if (dataArr[foxRow, foxCol - 2] == EMPTYCELL)
                {
                    return new Point(foxRow, foxCol - 1);
                }
            }


            else if (dataArr[foxRow - 1, foxCol] == CHICKEN_ID)
            {
                if (dataArr[foxRow - 2, foxCol] == EMPTYCELL)
                {
                    return new Point(foxRow - 1, foxCol);
                }
            }

            return new Point(-1, 0);
        }     

        /// <summary>
        /// True if there are legal eating moves
        /// </summary>
        private bool FoxCanEat(int foxRow, int foxCol)
        {
            return ChickenToEat(foxRow, foxCol).X != -1;
        }

        /// <summary>
        /// Remove fox from the target cell
        /// </summary>
        private void DeleteFox(int foxRow, int foxCol)
        {
            gameFieldDataArray[foxRow, foxCol] = EMPTYCELL;
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

                    currBt.Text = i + "_" + j;
                    currBt.ForeColor = Color.Black;

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

            tbDebug.Size = new System.Drawing.Size(CELL_SIZE * 2 - 20, CELL_SIZE * 2 - 20);
            tbDebug.Left = 40 + CELL_SIZE * 5;

            tbDebug2.Top = lbTextChicksLeft.Top;
            tbDebug2.Left = tbDebug.Left;
            tbDebug2.Size = tbDebug.Size;
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
                case FOX_ID:
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
