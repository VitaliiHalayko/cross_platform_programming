using System;
using System.IO;
using Xunit;
using Moq;

namespace lab2.Tests
{
    public class DynamicTests
    {
        [Fact]
        public void SolveCrazyFrog_ValidDataSize3_ReturnsCorrectValues()
        {
            // Arrange
            int N = 3;
            int[,] field = new int[,] { { 8, 2, 1 }, { 1, 2, 6 }, { 2, 7, 2 } };

            // Act
            var (maxMosquitoes, indices) = Dynamic.SolveCrazyFrog(N, field);

            // Assert
            Assert.Equal(14, maxMosquitoes);
            Assert.Equal(2, indices.Count);
            Assert.Equal((1, 1), indices[0]);
            Assert.Equal((2, 3), indices[1]);
        }

        [Fact]
        public void SolveCrazyFrog_ValidDataSize5_ReturnsCorrectValues()
        {
            // Arrange
            int N = 5;
            int[,] field = new int[,] { { 8, 2, 1, 2, 3 }, { 1, 2, 6, 2, 4 }, { 2, 7, 2, 3, 4 }, { 1, 3, 2, 4, 4 }, { 1, 3, 4, 3, 1 } };

            // Act
            var (maxMosquitoes, indices) = Dynamic.SolveCrazyFrog(N, field);

            // Assert
            Assert.Equal(19, maxMosquitoes);
            Assert.Equal(4, indices.Count);
            Assert.Equal((1, 1), indices[0]);
            Assert.Equal((1, 2), indices[1]);
            Assert.Equal((2, 3), indices[2]);
            Assert.Equal((1, 5), indices[3]);
        }
    }
}