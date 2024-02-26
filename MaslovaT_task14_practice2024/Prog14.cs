using System;
using System.Threading;

namespace MaslovaT_task14_practice2024
{
    /// <summary>
    /// <para>
    /// Задается число N (нечетное). Рисуется квадрат NхN. Смоделировать процесс распространения инфекции,
    /// при условии, что исходной зараженной клеткой является центральная.
    /// </para> 
    /// 
    /// <para>Выполняются следующие условия:</para>
    /// 
    /// <para>
    /// В каждый интервал времени(задается пользователем) пораженная инфекцией клетка может с вероятностью 0,5 
    /// заразить любую из соседних(по горизонтали или вертикали) здоровых клеток
    /// По прошествии шести единиц времени зараженная клетка становится невосприимчивой к инфекции
    /// Возникший иммунитет действует в течение последующих четырех единиц времени, а затем клетка оказывается
    /// здоровой и может заразиться снова
    /// </para>
    /// 
    /// <para>
    /// В ходе моделирования описанного процесса выдавать, согласно указанного временного интервала, текущее 
    /// состояние квадрата в каждом интервале времени отмечая различными цветами зараженные, невосприимчивые к инфекции и здоровые клетки
    /// </para>
    /// 
    /// </summary>
    static internal class Prog14
    {

        #region Consts

        /// <summary>
        /// Probability of infecting a cellState
        /// </summary>
        const float INFECT_PROB = (float)0.5;

        /// <summary>
        /// Timers of a cell state
        /// </summary>
        const byte MAX_INFECTED_TIME = 6, MAX_IMMUNE_TIME = 4;

        /// <summary>
        /// Identificators of a cellState state
        /// </summary>
        const byte NORMAL_CELL = 0, INFECTED_CELL = 1, IMMUNE_CELL = 2;

        /// <summary>
        /// Boundaries of the field
        /// </summary>
        const byte WALL = 4;

        /// <summary>
        /// Convenient color names
        /// </summary>
        const ConsoleColor gray = ConsoleColor.Gray, red = ConsoleColor.Red, green = ConsoleColor.Green,
            blue = ConsoleColor.Blue, yellow = ConsoleColor.Yellow, white = ConsoleColor.White, cyan = ConsoleColor.Cyan;

        #endregion

        #region Vars

        /// <summary>
        /// Size of the game field
        /// </summary>
        private static int FLD_SZ;

        /// <summary>
        /// Period between field updates in milliseconds
        /// </summary>
        private static int PERIOD_MS;

        /// <summary>
        /// Main game data array
        /// </summary>
        static private Cell[,] gameFieldDataArray;

        #endregion

        #region Convenience

        /// <summary>
        /// Output a colored line
        /// </summary>
        static void Writeln(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Output a colored line without \n
        /// </summary>
        static void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Change console foreground color
        /// </summary>
        static void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        #endregion

        #region Output

        /// <summary>
        /// Write data array
        /// </summary>
        static void Writeln(Cell[,] arr) 
        {
            int fld_sz = arr.GetLength(0);
            int trueSize = fld_sz - 2;

            Write("╔", white);
            for (int i = 0; i < trueSize*2+1; i++) 
            {
                Write("═", white);
            }
            Writeln("╗", white);

            for (int i = 1; i < fld_sz-1; i++)
            {
                Write("║ ", white);
                for (int j = 1; j < fld_sz-1; j++)
                {                
                            
                    if (arr[i,j].timeLeftInfected != 0)
                        Write(arr[i, j].timeLeftInfected+" ", CellDataToColor(arr[i, j].state));
                    else if (arr[i, j].timeLeftImmune != 0)
                        Write(arr[i, j].timeLeftImmune + " ", CellDataToColor(arr[i, j].state));
                    else
                        Write("0 ", CellDataToColor(arr[i, j].state));
                }
                Writeln("║", white);
            }

            Write("╚", white);
            for (int i = 0; i < trueSize*2+1; i++)
            {
                Write("═", white);
            }
            Writeln("╝", white);

        }

        /// <summary>
        /// Convert cellState data to a color
        /// </summary>
        static ConsoleColor CellDataToColor(byte cellState)
        {
            switch (cellState) 
            {
                case NORMAL_CELL: return ConsoleColor.Green;
                case INFECTED_CELL: return ConsoleColor.Red;
                case IMMUNE_CELL: return ConsoleColor.Cyan;
                case WALL: return ConsoleColor.White;
                default: return ConsoleColor.Gray;
            }
        }

        #endregion

        #region Working with data

        /// <summary>
        /// Update every cell and check if to continue
        /// </summary>
        static bool UpdateField(Cell[,] arr)
        {
            int fld_sz = arr.GetLength(0);

            int infectedCells = 0;

            for (int i = 1; i < fld_sz-1; i++)
            {
                for (int j = 1; j < fld_sz-1; j++)
                {
                    Random rnd = new();

                    switch (arr[i,j].state) 
                    {
                        case IMMUNE_CELL: 
                            SafeDecrement(ref arr[i,j].timeLeftImmune);
                            if (arr[i,j].timeLeftImmune==0)
                            {
                                arr[i,j].state = NORMAL_CELL;
                            }
                            break;

                        case NORMAL_CELL:
                            if (HasInfectedNeigbour(i,j))
                            {
                                double rndValue = rnd.NextDouble();
                                if (rndValue > INFECT_PROB)
                                {
                                    arr[i,j].state = INFECTED_CELL;
                                    arr[i,j].timeLeftInfected = MAX_INFECTED_TIME;
                                }
                            }
                            break;


                        case INFECTED_CELL:
                            SafeDecrement(ref arr[i,j].timeLeftInfected); 
                            if (arr[i,j].timeLeftInfected==0)
                            {
                                arr[i, j].state = IMMUNE_CELL;
                                arr[i,j].timeLeftImmune = MAX_IMMUNE_TIME; 
                            }
                            infectedCells++;
                            break;


                        default: 
                            break;

                    }
                }
            }
            if (infectedCells < 1)
                return false;
            return true;

        }
        /// <summary>
        /// True if one of the cells next to it is infected
        /// </summary>
        static bool HasInfectedNeigbour(int row, int col)
        {
            var arr = gameFieldDataArray;
            if (arr[row + 1, col].state == INFECTED_CELL) return true;
            if (arr[row - 1, col].state == INFECTED_CELL) return true;
            if (arr[row, col + 1].state == INFECTED_CELL) return true;
            if (arr[row, col - 1].state == INFECTED_CELL) return true;
            return false;
        }

        /// <summary>
        /// target -= 1, but it won't go lower than 0
        /// </summary>
        static void SafeDecrement(ref byte target)
        {
            if (target > 0)
                target--;
        }

        #endregion


        static void Main()
        {
            #region setup the custom settings

            ReFLD_SZ:
            Writeln("\nВведите размер поля N*N:\n", white);
            Write(">_ ", yellow);
            try
            {
                ChangeColor(cyan);
                FLD_SZ = int.Parse(Console.ReadLine());
            }
            catch
            {
                Writeln("ВЫ ВВЕЛИ НЕ НАТУРАЛЬНОЕ ЧИСЛО!!!", red); goto ReFLD_SZ;
            } // Input

            if( FLD_SZ < 4 )
            {
                Writeln("Слишком маленькое поле.", red); goto ReFLD_SZ;
            } // Check if the field is too small

            if ((FLD_SZ & 1) == 0)
            {
                FLD_SZ++;
            } // Check if the FLD_SZ is even

            // To make boundaries
            FLD_SZ += 2;

            RePEDIOD_MS:
            Writeln("\nВведите временной интервал обновления поля в миллисекундах:\n", white);
            Write(">_ ", yellow);
            try
            {
                ChangeColor(cyan);
                PERIOD_MS = int.Parse(Console.ReadLine());
            }
            catch
            {
                Writeln("ВЫ ВВЕЛИ НЕ НАТУРАЛЬНОЕ ЧИСЛО!!!", red); goto RePEDIOD_MS;
            } // Input

            if (PERIOD_MS < 1 ) 
            {
                Writeln("Некорректный ввод времени.", red); goto RePEDIOD_MS;
            } // Check if the time is impossible


            Console.Clear();

            #endregion

            #region setup default state

            gameFieldDataArray = new Cell[FLD_SZ, FLD_SZ];


            for (int j = 0; j < FLD_SZ; j++)
            {
                gameFieldDataArray[0, j] = new Cell(WALL);
            }
            for(int i = 1;  i < FLD_SZ-1; i++)
            {
                gameFieldDataArray[i, 0] = new Cell(WALL);
                for (int j = 1; j < FLD_SZ-1; j++)
                {
                    gameFieldDataArray[i, j] = new Cell(NORMAL_CELL);
                }
                gameFieldDataArray[i, FLD_SZ-1] = new Cell(WALL);
            }
            for (int j = 0; j < FLD_SZ; j++)
            {
                gameFieldDataArray[FLD_SZ-1, j] = new Cell(WALL);
            }

            int MDL_CELL = FLD_SZ / 2 ;
            gameFieldDataArray[MDL_CELL, MDL_CELL].state = INFECTED_CELL;

            #endregion


            bool continueFlag = true;
            while (continueFlag) 
            {
                Console.SetCursorPosition(0, 0);
                Writeln(gameFieldDataArray);
                Thread.Sleep(PERIOD_MS);
               
                continueFlag = UpdateField(gameFieldDataArray);
            }

            Writeln("Заражённых клеток не осталось!", cyan);
            Console.ReadKey();
        }

    }
}
