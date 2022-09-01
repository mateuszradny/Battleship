using Battleship.Engine.Models;

namespace Battleship.Engine
{
    public class GridCell
    {
        public static readonly GridCell OutOfRangeCall = new GridCell(null, -1, -1);

        public GridCell(Grid grid, int row, int column)
        {
            Grid = grid;
            Row = row;
            Column = column;
            Status = grid == null ? CellStatus.OutOfRange : CellStatus.Empty;
        }

        public Grid Grid { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public CellStatus Status { get; set; }
        public Ship? Ship { get; set; }
    }
}