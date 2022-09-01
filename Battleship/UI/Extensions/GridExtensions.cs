using Battleship.Engine;

namespace Battleship.UI.Extensions
{
    internal static class GridExtensions
    {
        private static readonly Dictionary<CellStatus, (ConsoleColor Color, string Text)> StatusMappings = new()
        {
            [CellStatus.Empty] = (ConsoleColor.White, "\u25A1 "),
            [CellStatus.Ship] = (ConsoleColor.White, "\u25A1 "),
            [CellStatus.Miss] = (ConsoleColor.DarkGray, "\u25A0 "),
            [CellStatus.Hit] = (ConsoleColor.Red, "\u25A0 "),
            [CellStatus.Sunk] = (ConsoleColor.DarkRed, "\u25A0 "),
        };

        public static void PrintGrid(this Grid grid)
        {
            Console.WriteLine("\tA B C D E F G H I J");

            for (int row = 0; row < 10; row++)
            {
                Console.Write(row + 1 + "\t");
                for (int column = 0; column < 10; column++)
                {
                    var status = grid[row, column].Status;
                    Console.ForegroundColor = StatusMappings[status].Color;
                    Console.Write(StatusMappings[status].Text);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }
    }
}