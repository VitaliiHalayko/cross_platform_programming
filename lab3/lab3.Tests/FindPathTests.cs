using System;
using System.IO;
using Xunit;
using Moq;

namespace lab3.Tests
{
	public class FindPathTests
	{
		// Test to verify the grid is initialized correctly based on the provided field
        [Fact]
        public void InitializeGrid_ShouldSetObstaclesCorrectly()
        {
            // Arrange: Create a simple 3x3 field with 'x' as obstacles
            char[,] field = new char[,]
            {
                { '.', 'x', '.' },
                { '.', '.', '.' },
                { 'x', '.', 'x' }
            };
            int width = 3;
            int height = 3;

            // Create an instance of FindPath
            FindPath findPath = new FindPath(width, height, field);

            // Act: Trigger the grid initialization (this happens in the constructor)
            // We will access the grid through reflection to verify its state

            // Use reflection to get access to the private grid field
            var gridField = typeof(FindPath).GetField("grid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            int[,] grid = (int[,])gridField.GetValue(findPath);

            // Assert: Check if the grid is initialized correctly with obstacles
            Assert.Equal(1, grid[2, 3]); // There should be an obstacle at (1,0) -> (2,3)
            Assert.Equal(1, grid[4, 2]); // There should be an obstacle at (0,2) -> (4,2)
            Assert.Equal(1, grid[4, 4]); // There should be an obstacle at (2,2) -> (4,4)

            // Check that non-obstacle cells are not marked as obstacles
            Assert.Equal(0, grid[2, 2]); // No obstacle at (0,0) -> (2,2)
            Assert.Equal(0, grid[3, 3]); // No obstacle at (1,1) -> (3,3)
        }

		// Test find path method
		[Fact]
		public void FindPath_ShouldReturnCorrectPath()
		{
			// Arrange: Create a simple 3x3 field with 'x' as obstacles
			char[,] field = new char[,]
			{
				{ 'X', 'X', 'X', '.' },
				{ 'X', '.', 'X', 'X' },
				{ 'X', '.', 'X', 'X' },
				{ 'X', '.', '.', 'X' },
				{ 'X', 'X', 'X', '.' },
			};
			int width = 5;
			int height = 4;

			// Create an instance of FindPath
			FindPath findPath = new FindPath(width, height, field);

			List<int[]> testCases = new List<int[]>();
			testCases.Add(new int[] { 2, 3, 5, 3, 5 });
			testCases.Add(new int[] { 1, 3, 4, 4, 6 });
			testCases.Add(new int[] { 2, 3, 3, 4, 0 });

			for (int i = 0; i < testCases.Count; i++)
			{
				var testCase = testCases[i];
				
				// Act: Find the path from (1,1) to (3,3)
				int steps = findPath.findPath(testCase[0], testCase[1], testCase[2], testCase[3]);

				// Assert: Check if the path was found and the number of steps taken
				Assert.Equal(testCase[4], steps);
			}
		}
	}
}