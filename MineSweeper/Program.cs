// See https://aka.ms/new-console-template for more information
using MineSweeper;

Console.WriteLine("Hello, World!");

MineGrid game = new MineGrid(17, 25, 0.1);
game.DisplayGrid();

game.MarkSquare(2, 2, 2);

game.DisplayGrid();
