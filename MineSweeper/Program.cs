// See https://aka.ms/new-console-template for more information
using MineSweeper;

Console.WriteLine("Hello, World!");

MineGrid game = new MineGrid(14, 18, 0.1);
game.DisplayGrid();

game.MarkSquare(2, 2, 2);
game.MarkSquare(1, 3, 2);
game.MarkSquare(1, 4, 2);
game.MarkSquare(1, 5, 2);

game.DisplayGrid();
