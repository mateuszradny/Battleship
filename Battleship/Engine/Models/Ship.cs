namespace Battleship.Engine.Models
{
    public class Ship
    {
        public Ship(Coordinates coordinatesOfBeginning, Orientation orientation, int length)
        {
            CoordinatesOfBeginning = coordinatesOfBeginning;
            Orientation = orientation;
            Length = length;

            CoordinatesOfEnd = Orientation == Orientation.Vertical
              ? new Coordinates(CoordinatesOfBeginning.Row + Length - 1, CoordinatesOfBeginning.Column)
              : new Coordinates(CoordinatesOfBeginning.Row, CoordinatesOfBeginning.Column + Length - 1);
        }

        public Coordinates CoordinatesOfBeginning { get; }
        public Coordinates CoordinatesOfEnd { get; }
        public Orientation Orientation { get; }
        public int Length { get; }

        public bool CheckIfCoordinatesBelongToShip(int row, int column)
        {
            return row >= CoordinatesOfBeginning.Row
                && row <= CoordinatesOfEnd.Row
                && column >= CoordinatesOfBeginning.Column
                && column <= CoordinatesOfEnd.Column;
        }
    }
}