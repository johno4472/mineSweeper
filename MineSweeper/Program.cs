// See https://aka.ms/new-console-template for more information
using MineSweeper;

Console.WriteLine("Welcome to minesweeper! For best playing environment, make your terminal full screen");

int customOption;
int rowCount = 14;
int columnCount = 18;
double squaresPerMine;
double chanceOfMine = 0.1;
while (true)
{
    try
    {
        Console.WriteLine("Want to customize gameplay (1) or play default (2)?\n" +
            "(Default gameplay has 14 rows, 18 columns, and about 1 mine per 10 squares)");
        customOption = Int32.Parse(Console.ReadLine() ?? " ");
        if (customOption != 1 && customOption != 2)
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2");
            continue;
        }
        else if (customOption == 2)
        {
            break;
        }
        Console.Write("How many rows do you want? (under 30): ");
        rowCount = Int32.Parse(Console.ReadLine() ?? " ");
        if (rowCount < 1 || rowCount > 30)
        {
            Console.WriteLine($"Invalid input. Enter a row number between 1 and 30");
            continue;
        }
        Console.Write("How many columns do you want? (under 30): ");
        columnCount = Int32.Parse(Console.ReadLine() ?? " ");
        if (columnCount < 1 || columnCount > 30)
        {
            Console.WriteLine($"Invalid input. Enter a row number between 1 and 100");
            continue;
        }
        Console.Write("1 out of every ____ squares should be mines (enter a whole number, default is 10): ");
        squaresPerMine = Double.Parse(Console.ReadLine() ?? " ");
        if (squaresPerMine <= 1)
        {
            Console.WriteLine($"Please enter a number above one");
            continue;
        }
        chanceOfMine = 1 / squaresPerMine;
        Console.WriteLine($"Alright! You will have a {chanceOfMine} chance of hitting a mine. Good luck!");
        Console.WriteLine("Drawing up grid...");
        Thread.Sleep(1000);
    }
    catch 
    {
        Console.WriteLine("Gave an entry that's not a number. Enter just integers");
        continue;
    }
    break;
}




int action;
int row;
int column;
bool exploreMode = true;
string currentMode;
string otherMode;
while (true)
{
    MineGrid game = new MineGrid(rowCount, columnCount, chanceOfMine);
    game.DisplayGrid();
    while (true)
    {
        game.ResetTurn = false;
        try
        {
            if (exploreMode)
            {
                action = 1;
                currentMode = "explore";
                otherMode = "flag";
            }
            else
            {
                action = 2;
                currentMode = "flag";
                otherMode = "explore";
            }
            Console.Write("You are in ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{currentMode}");
            Console.ResetColor();
            Console.WriteLine($" mode. Enter 's' if you want to switch to {otherMode} mode. Otherwise, proceed.");


            Console.Write("Row: ");
            var rowResponse = Console.ReadLine() ?? " ";
            if (rowResponse == "s")
            {
                exploreMode = !exploreMode;
                continue;
            }
            row = Int32.Parse(rowResponse);
            if (row < 0 || row > game.Grid.Count)
            {
                Console.WriteLine($"Invalid input. Enter 's' or a row number between 0 and {game.Grid.Count - 1}");
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
        else if (game.GameLost)
        {
            Console.WriteLine("You hit a mine! Game over");
            break;
        }
    }
    Console.WriteLine("Play again? y/n");
    string playAgain = Console.ReadLine() ?? "";
    if (playAgain == "n")
    {
        Console.WriteLine("Have great day!");
        Thread.Sleep(1000);
        break;
    }
    else if (playAgain == "y")
    {
        Console.WriteLine("Alright, let's give it another go!");
        Thread.Sleep(1000);
    }
    else
    {
        Console.WriteLine("You didn't write 'y' or 'n', so I'm gonna take that as a yes");
        Thread.Sleep(1000);
    }
}

