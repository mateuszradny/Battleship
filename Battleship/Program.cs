using Battleship.UI;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

BattleshipGameController controller = new BattleshipGameController();
controller.StartNewGame();

Console.ReadKey();