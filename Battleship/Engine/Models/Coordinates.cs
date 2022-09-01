namespace Battleship.Engine.Models
{
    public record Coordinates(int Row, int Column)
    {
        private const string Columns = "ABCDEFGHIJ";

        public static Coordinates Parse(string input)
        {
            if (input == null || input.Length > 3)
                throw new ArgumentException("Invalid input!");

            var columnPart = input[0];
            var rowPart = input[1..];

            int column = Columns.IndexOf(columnPart);
            if (column == -1)
                throw new ArgumentException("Invalid column number!");

            if (!int.TryParse(rowPart.ToString(), out var row) || row < 1 || row > 10)
                throw new ArgumentException("Invalid row number!");

            return new Coordinates(row - 1, column);
        }
    }
}