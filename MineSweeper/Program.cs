// See https://aka.ms/new-console-template for more information
using MineSweeper;

Console.WriteLine("Hello, World!");

MineGrid game = new MineGrid(14, 18, 0.1);
game.DisplayGrid();

int action;
int row;
int column;
while (true)
{
    try
    {
        Console.Write("\nExplore(1) or Flag(2): ");
        action = Int32.Parse(Console.ReadLine() ?? " ");
        if (action != 1 && action != 2)
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2");
            continue;
        }
        Console.Write("Row: ");
        row = Int32.Parse(Console.ReadLine() ?? " ");
        if (row < 0 || row > game.Grid.Count)
        {
            Console.WriteLine($"Invalid input. Enter a row number between 0 and {game.Grid.Count - 1}");
            continue;
        }
        Console.Write("Column: ");
        column = Int32.Parse(Console.ReadLine() ?? " ");
        if (row < 0 || row > game.Grid.Count)
        {
            Console.WriteLine($"Invalid input. Enter a row number between 0 and {game.Grid.Count - 1}");
            continue;
        }
    }
    catch
    {
        Console.WriteLine("Gave an entry that's not a number. Enter just integers");
        continue;
    }
    game.MarkSquare(action, row, column);
    if (game.ResetTurn) { continue; }
    game.DisplayGrid();
    if (game.CheckIfWinner())
    {
        Console.WriteLine("You've won!");
        break;
    }
}

