using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class MineGrid
    {
        public List<List<MineGridSquare>> MineGridMap;
        Random MineChance;

        public MineGrid(int rows, int columns, double percentMines) 
        {
            MineGridMap = [[]];
            MineChance = new Random();
            InitializeGrid(rows, columns);
            PlaceBombs(percentMines);
            PlaceNumberHints();
        }

        public MineGrid InitializeGrid(int rows = 14, int columns = 18)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    MineGridMap[i][j] = new MineGridSquare();
                }
            }
            return this;
        }

        public MineGrid PlaceBombs(double percentMines = 0.1)
        {
            for (int i = 0; i < MineGridMap.Count; i++)
            {
                for (int j = 0; j < MineGridMap[i].Count; j++)
                {
                    if (MineChance.NextDouble() <= percentMines)
                    {
                        MineGridMap[i][j].IsBomb = true;
                    }
                }
            }
            return this;
        }

        public MineGrid PlaceNumberHints() 
        {
            for (int i = 0; i < MineGridMap.Count; i++)
            {
                for (int j = 0; j<MineGridMap[i].Count; j++)
                {
                    MineGridMap[i][j].NumBombsAround = CalculateBombs(i, j);
                }
            }
            return this;
        }

        public int CalculateBombs(int rowNum, int colNum)
        {
            if (MineGridMap[rowNum][colNum].IsBomb)
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
                        if (MineGridMap[i][j].IsBomb)
                        {
                            numBombs++;
                        }
                    }
                }
            }
            return numBombs;
        }

        public bool IsValidCoord(int rowNum, int colNum, int rowModifier, int colModifier)
        {
            if (rowNum < 0 || rowNum >= MineGridMap.Count || colNum < 0 || colNum >= MineGridMap[0].Count || (rowModifier == 0 && colModifier == 0))
            {
                return false;
            }
            return true;
        }
    }
}
