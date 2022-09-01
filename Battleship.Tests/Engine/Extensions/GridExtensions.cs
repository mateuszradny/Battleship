using Battleship.Engine;

namespace Battleship.Tests.Engine.Extensions
{
    internal static class GridExtensions
    {
        public static List<GridCell> GetAllCells(this Grid grid)
        {
            var allCells = new List<GridCell>();
            for (int row = 0; row < 10; row++)
                for (int column = 0; column < 10; column++)
                    allCells.Add(grid[row, column]);

            return allCells;
        }
    }
}