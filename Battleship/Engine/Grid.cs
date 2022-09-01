using Battleship.Engine.Models;

namespace Battleship.Engine
{
    public class Grid
    {
        private readonly GridCell[,] _grid = new GridCell[10, 10];
        private bool _isAnyCellChecked = false;
        private int _cellsOfShips = 0;

        public GridCell this[int row, int column]
        {
            get => ((row >= 0 && row < _grid.GetLength(0)) && (column >= 0 && column < _grid.GetLength(1)))
              ? _grid[row, column]
              : GridCell.OutOfRangeCall;
        }

        public Grid()
        {
            for (int row = 0; row < 10; row++)
                for (int column = 0; column < 10; column++)
                    _grid[row, column] = new GridCell(this, row, column);
        }

        public void AddShip(Ship ship)
        {
            if (_isAnyCellChecked)
                throw new InvalidOperationException("You cannot add a ship to the grid in this state of the game");

            if (!CanAddShip(ship))
                throw new InvalidOperationException("You cannot add a ship because there is already a ship on the selected cells or the indicated cells are outside the game's grid");

            MarkCellsAsShip(ship);
        }

        public CellStatus CheckCell(int row, int column)
        {
            _isAnyCellChecked = true;

            if (_grid[row, column].Status == CellStatus.Empty)
            {
                _grid[row, column].Status = CellStatus.Miss;
            }
            else if (_grid[row, column].Status == CellStatus.Ship)
            {
                _grid[row, column].Status = CellStatus.Hit;
                _cellsOfShips--;

                if (CheckIfShipSank(row, column))
                    MarkAsSunk(row, column);
            }

            return _grid[row, column].Status;
        }

        public bool CheckIfAllShipsSank() => _cellsOfShips == 0;

        private bool CheckIfShipSank(int row, int column)
        {
            var orientation = GetShipOrientation(row, column);
            return orientation == Orientation.Vertical
              ? CheckVertical(row, column)
              : CheckHorizontal(row, column);
        }

        private bool CheckVertical(int row, int column)
            => CheckTop(row, column) && CheckBottom(row, column);

        private bool CheckHorizontal(int row, int column)
            => CheckLeft(row, column) && CheckRight(row, column);

        private bool CheckTop(int row, int column)
            => CheckDirection(row, column, rowShift: -1);

        private bool CheckBottom(int row, int column)
            => CheckDirection(row, column, rowShift: 1);

        private bool CheckLeft(int row, int column)
            => CheckDirection(row, column, columnShift: -1);

        private bool CheckRight(int row, int column)
            => CheckDirection(row, column, columnShift: 1);

        private bool CheckDirection(int row, int column, int rowShift = 0, int columnShift = 0)
        {
            int rowIndex = row + rowShift;
            int columnIndex = column + columnShift;
            while (IsCellOfShip(rowIndex, columnIndex))
            {
                if (this[rowIndex, columnIndex].Status == CellStatus.Ship)
                    return false;

                rowIndex += rowShift;
                columnIndex += columnShift;
            }

            return true;
        }

        private void MarkAsSunk(int row, int column)
        {
            if (IsCellOfShip(row, column) && _grid[row, column].Status != CellStatus.Sunk)
            {
                _grid[row, column].Status = CellStatus.Sunk;
                MarkAsSunk(row - 1, column);
                MarkAsSunk(row, column + 1);
                MarkAsSunk(row + 1, column);
                MarkAsSunk(row, column - 1);
            }
        }

        private bool IsCellOfShip(int row, int column)
        {
            return this[row, column].Ship != null;
        }

        private Orientation GetShipOrientation(int row, int column)
        {
            if (!IsCellOfShip(row, column))
                throw new InvalidOperationException($"Coordinates [{row}, {column}] don't belong to a ship!");

            return this[row, column].Ship.Orientation;
        }

        private bool CanAddShip(Ship ship)
        {
            for (int row = ship.CoordinatesOfBeginning.Row - 1; row <= ship.CoordinatesOfEnd.Row + 1; row++)
                for (int column = ship.CoordinatesOfBeginning.Column - 1; column <= ship.CoordinatesOfEnd.Column + 1; column++)
                {
                    if (this[row, column].Status != CellStatus.Empty && (ship.CheckIfCoordinatesBelongToShip(row, column) || this[row, column].Status != CellStatus.OutOfRange))
                        return false;
                }

            return true;
        }

        private void MarkCellsAsShip(Ship ship)
        {
            for (int row = ship.CoordinatesOfBeginning.Row; row <= ship.CoordinatesOfEnd.Row; row++)
                for (int column = ship.CoordinatesOfBeginning.Column; column <= ship.CoordinatesOfEnd.Column; column++)
                {
                    _grid[row, column].Status = CellStatus.Ship;
                    _grid[row, column].Ship = ship;
                }

            _cellsOfShips += ship.Length;
        }
    }
}