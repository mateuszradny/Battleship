using Battleship.Engine.Models;

namespace Battleship.UI
{
    public static class ConsoleHelper
    {
        public static Coordinates ReadCoordinates()
        {
            Console.Write("Enter coordinates: ");

            do
            {
                try
                {
                    string coordinates = Console.ReadLine();
                    return Coordinates.Parse(coordinates);
                }
                catch (ArgumentException)
                {
                    ClearPreviousConsoleLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Incorrect coordinates! Try again: ");
                    Console.ResetColor();
                }
            }
            while (true);
        }

        private static void ClearPreviousConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}