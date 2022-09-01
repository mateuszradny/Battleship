using Battleship.Engine;
using Battleship.Engine.GridGenerators;
using Battleship.UI.Extensions;

namespace Battleship.UI
{
    public class BattleshipGameController
    {
        public void StartNewGame()
        {
            BattleshipGameEngine engine = new BattleshipGameEngine(new RandomGridGenerator());

            while (engine.Status == GameStatus.PlayerMovement)
            {
                engine.Grid.PrintGrid();

                var coordinates = ConsoleHelper.ReadCoordinates();
                engine.MakePlayerMovement(coordinates.Row, coordinates.Column);

                Console.Clear();
            }

            engine.Grid.PrintGrid();

            Console.WriteLine("You won!");
            Console.ReadKey();
        }
    }
}