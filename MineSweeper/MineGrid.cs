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
            for (int i = 1; i < Grid[0].Count; i++)
            {
                Console.Write("___");
                string macron = "¯";
            }
            Console.WriteLine("____");
            for (int i = 1; i < Grid.Count; i++)
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
                    int status = Grid[i][j].Status; 
                    if (status == UNEXPLORED)
                    {
                        Console.Write($"O{spacing}");
                    }
                    else if (status == EXPLORED)
                    {
                        if (square.NumBombsAround == 0)
                        {
                            Console.Write($" {spacing}");
                        }
                        else if (square.NumBombsAround == -1)
                        {
                            Console.Write($"X{spacing}");
                        }
                        else
                        {
                            Console.Write($"{square.NumBombsAround + spacing}");
                        }
                        
                    }
                    else if (status == FLAGGED)
                    {
                        Console.Write($"-{spacing}");
                    }
                    else
                    {
                        throw new Exception("Somehow square has a status out of range");
                    }
                }
                Console.WriteLine("|");
            }
            Console.Write("   |");
            bool writeNums = false;
            for (int i = 0; i < Grid[0].Count; i++)
            {
                if (!writeNums)
                {
                    Console.Write("\u0304\u0304\u0304");
                    if (i == Grid[0].Count - 1)
                    {
                        Console.Write("|\n   ");
                        writeNums = true;
                        i = -1;
                    }
                }
                else
                {
                    Console.Write($"{i + 1} ");
                    if (i + 1 < 10)
                    {
                        Console.Write(' ');
                    }
                }
                    
            }
            Console.WriteLine();
        }

        public MineGrid? MarkSquare(int exploreOrFlag, int row, int column)
        {
            if (!IsValidCoord(row, column))
            {
                Console.WriteLine("Invalid coordinates given");
                return null;
            }
            Grid[row][column].Status = exploreOrFlag;
            return this;
        }
    }
}
