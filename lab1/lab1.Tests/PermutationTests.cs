using System;
using Xunit;

namespace lab1.Tests
{
    public class PermutationTests
    {
        [Theory]
        [InlineData(3, 1, new[] { 1, 2, 3 })]
        [InlineData(3, 2, new[] { 1, 3, 2 })]
        [InlineData(3, 3, new[] { 2, 1, 3 })]
        [InlineData(3, 4, new[] { 2, 3, 1 })]
        [InlineData(3, 5, new[] { 3, 1, 2 })]
        [InlineData(3, 6, new[] { 3, 2, 1 })]
        public void TestGetPermutation(int n, int k, int[] expected)
        {
            // Act
            var result = Program.getPermutation(n, k);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
