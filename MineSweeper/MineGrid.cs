using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MineSweeper
{
    public class MineGrid
    {
        public List<List<MineGridSquare>> Grid;
        Random MineChance;
        public const int UNEXPLORED = 0;
        public const int EXPLORED = 1;
        public const int FLAGGED = 2;
        public bool GameLost = false;
        

        public MineGrid(int rows, int columns, double percentMines) 
        {
            Grid = [];
            MineChance = new Random();
            InitializeGrid(rows, columns);
            PlaceBombs(percentMines);
            PlaceNumberHints();
        }

        public MineGrid InitializeGrid(int rows = 14, int columns = 18)
        {
            //need to append new rows when creating it
            for (int i = 0; i < rows; i++)
            {
                Grid.Add([]);
                for (int j = 0; j < columns; j++)
                {
                    Grid[i].Add(new MineGridSquare());
                }
            }
            return this;
        }

        public MineGrid PlaceBombs(double percentMines = 0.1)
        {
            for (int i = 0; i < Grid.Count; i++)
            {
                for (int j = 0; j < Grid[i].Count; j++)
                {
                    if (MineChance.NextDouble() <= percentMines)
                    {
                        Grid[i][j].IsBomb = true;
                    }
                }
            }
            return this;
        }

        public MineGrid PlaceNumberHints() 
        {
            for (int i = 0; i < Grid.Count; i++)
            {
                for (int j = 0; j<Grid[i].Count; j++)
                {
                    Grid[i][j].NumBombsAround = CalculateBombs(i, j);
                }
            }
            return this;
        }

        public int CalculateBombs(int rowNum, int colNum)
        {
            if (Grid[rowNum][colNum].IsBomb)
            {
                return -1;
            }
            int numBombs = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j =  -1; j <= 1; j++)
                {
                    if (IsValidCoord(rowNum + i, colNum + j, i, j))
                    {
                        if (Grid[rowNum + i][colNum + j].IsBomb)
                        {
                            numBombs++;
                        }
                    }
                }
            }
            return numBombs;
        }

        public bool IsValidCoord(int rowNum, int colNum, int rowModifier = 1, int colModifier = 1)
        {
            if (rowNum < 0 || rowNum >= Grid.Count || colNum < 0 || colNum >= Grid[0].Count || (rowModifier == 0 && colModifier == 0))
            {
                return false;
            }
            return true;
        }

        public void DisplayGrid()
        {
            Console.WriteLine();
            Console.Write("   ");
            bool writeNums = true;
            Console.Write("  ");
            for (int i = 0; i < Grid[0].Count; i++)
            {
                if (writeNums)
                {
                    Console.Write($"{i} ");
                    if (i + 1 < 10)
                    {
                        Console.Write(' ');
                    }
                    if (i == Grid[0].Count - 1)
                    {
                        Console.Write("\n   ");
                        writeNums = false;
                        i = -1;
                    }
                }
                else
                {
                    Console.Write("___");
                }
            }
            Console.WriteLine("_");
            for (int i = 0; i < Grid.Count; i++)
            {
                Console.Write($"{i} ");
                if (i < 10)
                {
                    Console.Write(' ');
                }
                Console.Write("| ");

                string spacing = "  ";
                for (int j = 0; j < Grid[i].Count; j++)
                {
                    if (j == Grid[i].Count - 1)
                    {
                        spacing = " ";
                    }
                    MineGridSquare square = Grid[i][j];
                    if (GameLost && square.IsBomb && square.Status != FLAGGED)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"X");
                        Console.ResetColor();
                        Console.Write($"{spacing}");
                        continue;
                    }
                    int status = Grid[i][j].Status; 
                    if (status == UNEXPLORED)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write($"O{spacing}");
                        Console.ResetColor();
                    }
                    else if (status == EXPLORED)
                    {
                        if (square.NumBombsAround == 0)
                        {
                            Console.Write($" {spacing}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"{square.NumBombsAround + spacing}");
                            Console.ResetColor();
                        }
                        Console.ResetColor();
                    }
                    else if (status == FLAGGED)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"?{spacing}");
                        Console.ResetColor();
                    }
                    else
                    {
                        throw new Exception("Somehow square has a status out of range");
                    }
                }
                Console.WriteLine($"| {i}");
            }
            Console.Write("   |");
            for (int i = 0; i < Grid[0].Count; i++)
            {
                if (!writeNums)
                {
                    Console.Write("\u0304\u0304\u0304");
                    if (i == Grid[0].Count - 1)
                    {
                        Console.Write("|\n     ");
                        writeNums = true;
                        i = -1;
                    }
                }
                else
                {
                    Console.Write($"{i} ");
                    if (i + 1 < 10)
                    {
                        Console.Write(' ');
                    }
                }
                    
            }
            Console.WriteLine();
        }

        public void FlagSquare(int row, int column)
        {
            if (Grid[row][column].Status == UNEXPLORED)
            {
                Grid[row][column].Status = FLAGGED;
            }
            else if (Grid[row][column].Status == FLAGGED)
            {
                Grid[row][column].Status = UNEXPLORED;
            }
            else if (Grid[row][column].Status == EXPLORED)
            {
                //eventually throw error to reset turn
                Console.WriteLine("Cannot flag an explored spot");
            }
        }

        public void ExploreSquare(int row, int column)
        {
            if (IsValidCoord(row, column) && Grid[row][column].Status == UNEXPLORED) 
            {
                Grid[row][column].Status = EXPLORED;
                if (Grid[row][column].IsBomb)
                {
                    GameLost = true;
                    return;
                }

                if (Grid[row][column].NumBombsAround == 0)
                {
                    ExploreSquare(row - 1, column - 1);
                    ExploreSquare(row, column - 1);
                    ExploreSquare(row + 1, column - 1);
                    ExploreSquare(row + 1, column);
                    ExploreSquare(row + 1, column + 1);
                    ExploreSquare(row, column + 1);
                    ExploreSquare(row - 1, column + 1);
                    ExploreSquare(row - 1, column);
                }  
            }
        }

        public MineGrid? MarkSquare(int exploreOrFlag, int row, int column)
        {
            if (!IsValidCoord(row, column))
            {
                Console.WriteLine("Invalid coordinates given");
                return null;
            }
            if (exploreOrFlag == EXPLORED)
            {
                ExploreSquare(row, column);
            }
            else if (exploreOrFlag == FLAGGED)
            {
                FlagSquare(row, column);
            }
            
            return this;
        }

        public bool CheckIfWinner()
        {
            for (int i = 0; i < Grid.Count; i++)
            {
                for (int j = 0; j < Grid[i].Count; j++)
                {
                    if (Grid[i][j].Status == UNEXPLORED)
                    {
                        return false;
                    }
                    else if (Grid[i][j].Status == FLAGGED & !Grid[i][j].IsBomb)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
