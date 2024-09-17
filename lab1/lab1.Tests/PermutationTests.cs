using System;
using Xunit;

namespace lab1.Tests
{
    public class PermutationTests
    {
        [Theory]
        [InlineData(1, 1, new[] { 1 })]
        [InlineData(3, 2, new[] { 1, 3, 2 })]
        [InlineData(3, 3, new[] { 2, 1, 3 })]
        [InlineData(3, 4, new[] { 2, 3, 1 })]
        [InlineData(3, 5, new[] { 3, 1, 2 })]
        [InlineData(12, 479001600, new[] { 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 })]
        [InlineData(4, 1, new[] { 1, 2, 3, 4 })]
        [InlineData(4, 24, new[] { 4, 3, 2, 1 })]
        public void GetCorrectPermutation_ReturnsCorrectPermutation(int n, int k, int[] expected)
        {
            // Act
            var result = Permutation.getPermutation(n, k);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetPermutation_InvalidN_ThrowsArgumentException()
        {
            // Arrange
            int n = 13; // Out of valid range (1 to 12)
            int k = 1;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => Permutation.getPermutation(n, k));
            Assert.Equal("Invalid input: n must be between 1 and 12, and k must be between 1 and n!", ex.Message);
        }

        // Test for invalid input: k out of range
        [Fact]
        public void GetPermutation_InvalidK_ThrowsArgumentException()
        {
            // Arrange
            int n = 5;
            int k = 121; // Out of valid range for n = 5 (which has only 120 permutations)

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => Permutation.getPermutation(n, k));
            Assert.Equal("Invalid input: n must be between 1 and 12, and k must be between 1 and n!", ex.Message);
        }
    }
}
