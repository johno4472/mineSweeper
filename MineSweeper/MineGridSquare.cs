using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{

    public class MineGridSquare
    {

        //set to -1 if is bomb; value set in third iteration of bomblaying
        public int NumBombsAround { get; set; }

        //true - bomb, false - notbomb; set to false by default, then changed to true on second iteration of bomblaying
        public bool IsBomb { get; set; } = false;

        //0 - unexplored, 1 - explored, 2 - flagged; all set to unexplored by default
        public int Status { get; set; } = 1;
    }
}
