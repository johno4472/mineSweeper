// See https://aka.ms/new-console-template for more information
using MineSweeper;

Console.WriteLine("Hello, World!");

MineGrid game = new MineGrid(14, 18, 0.1);
game.DisplayGrid();

string row;
string column;
while (true)
{
    Console.Write("Row: ");
    row = Console.ReadLine() ?? " ";
    Console.Write("\nColumn: ");
    column = Console.ReadLine() ?? " ";
    game.MarkSquare(2, Int32.Parse(row), Int32.Parse(column));
    game.DisplayGrid();
}

