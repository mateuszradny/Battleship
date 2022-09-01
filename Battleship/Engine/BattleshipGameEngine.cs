using Battleship.Engine.GridGenerators;

namespace Battleship.Engine
{
    public class BattleshipGameEngine
    {
        private readonly IGridGenerator _gridGenerator;

        public GameStatus Status { get; private set; }
        public Grid Grid { get; private set; }

        public BattleshipGameEngine(IGridGenerator gridGenerator)
        {
            _gridGenerator = gridGenerator ?? throw new ArgumentNullException(nameof(gridGenerator));
            Grid = _gridGenerator.Generate();

            Status = GameStatus.PlayerMovement;
        }

        public CellStatus MakePlayerMovement(int row, int column)
        {
            if (Status != GameStatus.PlayerMovement)
                throw new InvalidOperationException("It's not your move!");

            var cellStatus = Grid.CheckCell(row, column);
            if (cellStatus == CellStatus.OutOfRange)
                throw new InvalidOperationException($"Invalid coordinates [{row}, {column}]!");

            if (Grid.CheckIfAllShipsSank())
                Status = GameStatus.PlayerWon;

            return cellStatus;
        }
    }
}