// See https://aka.ms/new-console-template for more information
using MineSweeper;

Console.WriteLine("Hello, World!");

MineGrid game = new MineGrid(14, 18, 0.02);
game.DisplayGrid();

string row;
string column;
string action;
while (true)
{
    Console.Write("\nExplore(1) or Flag(2): ");
    action = Console.ReadLine() ?? " ";
    Console.Write("Row: ");
    row = Console.ReadLine() ?? " ";
    Console.Write("Column: ");
    column = Console.ReadLine() ?? " ";
    game.MarkSquare(Int32.Parse(action), Int32.Parse(row), Int32.Parse(column));
    game.DisplayGrid();
    if (game.CheckIfWinner())
    {
        Console.WriteLine("You've won!");
        break;
    }
}

