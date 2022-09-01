using Battleship.Engine;
using Battleship.Engine.Models;
using Battleship.Tests.Engine.Extensions;
using FluentAssertions;

namespace Battleship.Tests.Engine
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void AddShip_WhenAddedSomeShips_ShouldReturnValidGrid()
        {
            // Arrange
            var grid = new Grid();
            var ships = new Ship[]
            {
                new Ship(new Coordinates(1, 1), Orientation.Vertical, 5),
                new Ship(new Coordinates(2, 4), Orientation.Horizontal, 4),
                new Ship(new Coordinates(8, 3), Orientation.Horizontal, 4),
                new Ship(new Coordinates(5, 8), Orientation.Vertical, 3)
            };

            // Act
            foreach (var ship in ships)
                grid.AddShip(ship);

            // Assert
            var allShipsCells = ships.Sum(x => x.Length);
            var allCells = grid.GetAllCells();
            allCells.Count(x => x.Status == CellStatus.Ship).Should().Be(allShipsCells);
            allCells.Count(x => x.Status == CellStatus.Empty).Should().Be(100 - allShipsCells);
        }

        [TestMethod]
        public void AddShip_WhenAddedTheSameShip_ShouldThrowException()
        {
            // Arrange
            Grid grid = new Grid();
            Ship ship = new Ship(new Coordinates(1, 1), Orientation.Vertical, 5);

            // Act
            grid.AddShip(ship);
            var action = () => grid.AddShip(ship);

            // Assert
            action.Should().ThrowExactly<InvalidOperationException>();
        }

        [TestMethod]
        public void AddShip_WhenAddedShipOutsideGrid_ShouldThrowException()
        {
            // Arrange
            Grid grid = new Grid();
            Ship invalidShip = new Ship(new Coordinates(15, 15), Orientation.Vertical, 5);

            // Act
            var action = () => grid.AddShip(invalidShip);

            // Assert
            action.Should().ThrowExactly<InvalidOperationException>();
        }

        [TestMethod]
        public void AddShip_WhenAddedShipAfterPlayerMovement_ShouldThrowException()
        {
            // Arrange
            Grid grid = new Grid();
            Ship ship1 = new Ship(new Coordinates(1, 1), Orientation.Vertical, 5);
            Ship ship2 = new Ship(new Coordinates(5, 5), Orientation.Vertical, 2);

            // Act
            grid.AddShip(ship1);
            grid.CheckCell(0, 0);
            var action = () => grid.AddShip(ship2);

            // Assert
            action.Should().ThrowExactly<InvalidOperationException>();
        }

        [TestMethod]
        public void CheckCell_ShouldReturnCorrertStatus()
        {
            // Arrange
            Grid grid = new Grid();
            grid.AddShip(new Ship(new Coordinates(1, 1), Orientation.Vertical, 2));

            // Act
            var firstCheck = grid.CheckCell(0, 0);
            var secondCheck = grid.CheckCell(1, 1);
            var thirdCheck = grid.CheckCell(2, 1);

            // Assert
            firstCheck.Should().Be(CellStatus.Miss);
            secondCheck.Should().Be(CellStatus.Hit);
            thirdCheck.Should().Be(CellStatus.Sunk);
        }

        [TestMethod]
        public void CheckIfAllShipsSank_WhenNotAllShipsSank_ShouldReturnFalse()
        {
            // Arrange
            Grid grid = new Grid();
            grid.AddShip(new Ship(new Coordinates(1, 1), Orientation.Vertical, 5));

            // Act
            grid.CheckCell(1, 1);

            // Assert
            grid.CheckIfAllShipsSank().Should().BeFalse();
        }

        [TestMethod]
        public void CheckIfAllShipsSank_WhenAllShipsSank_ShouldReturnFalse()
        {
            // Arrange
            var grid = new Grid();
            grid.AddShip(new Ship(new Coordinates(1, 1), Orientation.Vertical, 2));
            grid.AddShip(new Ship(new Coordinates(5, 5), Orientation.Horizontal, 2));

            // Act
            grid.CheckCell(1, 1);
            grid.CheckCell(2, 1);

            grid.CheckCell(5, 5);
            grid.CheckCell(5, 6);

            // Assert
            grid.CheckIfAllShipsSank().Should().BeTrue();
        }
    }
}