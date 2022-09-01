namespace Battleship.Engine
{
    public enum CellStatus : short
    {
        Empty,
        Ship,
        Miss,
        Hit,
        Sunk,
        OutOfRange
    }
}