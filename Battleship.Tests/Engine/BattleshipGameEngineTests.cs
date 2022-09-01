using Battleship.Engine;
using Battleship.Engine.GridGenerators;
using Battleship.Engine.Models;
using FluentAssertions;
using Moq;

namespace Battleship.Tests.Engine
{
    [TestClass]
    public class BattleshipGameEngineTests
    {
        [TestMethod]
        public void MakePlayerMovement_WhenPlayerMadeSomeMovements_ShouldReturnCorrectStatus()
        {
            // Arrange
            var gridGeneratorMock = new Mock<IGridGenerator>();
            gridGeneratorMock.Setup(x => x.Generate()).Returns(() =>
            {
                var cpuGrid = new Grid();
                cpuGrid.AddShip(new Ship(new Coordinates(1, 1), Orientation.Horizontal, 2));

                return cpuGrid;
            });

            var engine = new BattleshipGameEngine(gridGeneratorMock.Object);

            // Act
            var status1 = engine.Status;
            var status2 = engine.MakePlayerMovement(0, 0);
            var status3 = engine.Status;
            var status4 = engine.MakePlayerMovement(1, 1);
            var status5 = engine.Status;
            var status6 = engine.MakePlayerMovement(1, 2);
            var status7 = engine.Status;

            // Assert
            status1.Should().Be(GameStatus.PlayerMovement);
            status2.Should().Be(CellStatus.Miss);
            status3.Should().Be(GameStatus.PlayerMovement);
            status4.Should().Be(CellStatus.Hit);
            status5.Should().Be(GameStatus.PlayerMovement);
            status6.Should().Be(CellStatus.Sunk);
            status7.Should().Be(GameStatus.PlayerWon);
        }
    }
}