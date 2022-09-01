using Battleship.Engine;
using Battleship.Engine.GridGenerators;
using Battleship.Tests.Engine.Extensions;
using FluentAssertions;

namespace Battleship.Tests.Engine.GridGenerators
{
    [TestClass]
    public class RandomGridGeneratorTests
    {
        [TestMethod]
        public void Generate_WhenNewBoardIsGenerated_ShouldReturnValidNumberOfShips()
        {
            // Arrange
            var randomGridGenerator = new RandomGridGenerator();

            // Act
            var grid = randomGridGenerator.Generate();

            // Assert
            var allCells = grid.GetAllCells();

            // 1x5 + 2x4 = 13
            allCells.Count(x => x.Status == CellStatus.Ship).Should().Be(13);
            allCells.Count(x => x.Status == CellStatus.Empty).Should().Be(87);
        }
    }
}