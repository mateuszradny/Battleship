using Battleship.Engine.Models;

namespace Battleship.Engine.GridGenerators
{
    public class RandomGridGenerator : IGridGenerator
    {
        private static readonly Random _random = new Random();

        public Grid Generate()
        {
            Grid grid = new Grid();
            AddShip(grid, 5);
            AddShip(grid, 4);
            AddShip(grid, 4);

            return grid;
        }

        private void AddShip(Grid grid, int length)
        {
            while (!TryAddShip(grid, length)) ;
        }

        private bool TryAddShip(Grid grid, int length)
        {
            var orientation = _random.Next(0, 2) == 0 ? Orientation.Horizontal : Orientation.Vertical;
            int row = _random.Next(0, 10 - (orientation == Orientation.Horizontal ? 0 : length));
            int column = _random.Next(0, 10 - (orientation == Orientation.Vertical ? 0 : length));
            var ship = new Ship(new Coordinates(row, column), orientation, length);

            try
            {
                grid.AddShip(ship);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}