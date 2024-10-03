using System;
using System.IO;
using Xunit;
using Moq;

namespace lab3.Tests
{
    public class IOTests
    {
        private const string inputFilePath = "INPUT.txt";
        private const string outputFilePath = "OUTPUT.txt";

        // Test for a valid input case
        [Fact]
        public void ReadDataFromFile_ValidData_ReturnsCorrectValues()
        {
            // Arrange
            var lines = new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".XXX.", "2 3 5 3", "1 3 4 4", "2 3 3 4", "0 0 0 0" };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

            // Act
            (int width, int height, char[,] field, List<int[]> coords) = IO.readDataFromFile();

            // Assert
            Assert.Equal(5, width);
            Assert.Equal(4, height);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Assert.Equal(lines[i + 1][j], field[j, i]);
				}
			}
			Assert.Equal(3, coords.Count);
			for (int i = 0; i < coords.Count; i++)
			{
				Assert.Equal(int.Parse(lines[i + 1 + height].Split(' ')[0]), coords[i][0]);
				Assert.Equal(int.Parse(lines[i + 1 + height].Split(' ')[1]), coords[i][1]);
				Assert.Equal(int.Parse(lines[i + 1 + height].Split(' ')[2]), coords[i][2]);
				Assert.Equal(int.Parse(lines[i + 1 + height].Split(' ')[3]), coords[i][3]);
			}

			// Cleanup
			File.Delete(inputFilePath); // Remove the test file
		}
		
		// Test for a valid output case
		[Fact]
		public void WriteResultsToFile_ValidResults_WritesCorrectly()
		{
			// Arrange
			var results = new List<int> { 5, 6, 0 };

			// Act
			IO.writeResultsToFile(results);

			// Assert
			var output = File.ReadAllText(outputFilePath);
			Assert.Equal("5\n6\n0", output);

			// Cleanup
			File.Delete(outputFilePath);
		}

        // Test for a not exist file
        [Fact]
		public void ReadDataFromFile_NotExistFile_ThrowsError()
		{
			// Arrange
			// Ensure the input file does not exist
			if (File.Exists(inputFilePath))
			{
				File.Delete(inputFilePath);
			}

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Input file not found.", exception.Message);
		}

		// Test for a empty file
        [Fact]
		public void ReadDataFromFile_EmptyFile_ThrowsError()
		{
			// Arrange
			// Create an empty input file
			File.WriteAllText(inputFilePath, string.Empty);

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Error reading input file: Input file must contain at least two lines.", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for a file with only one line
        [Fact]
		public void ReadDataFromFile_InvalidLinesCountFile_ThrowsError()
		{
			// Arrange
			var lines = new[] { "5 4" };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Error reading input file: Input file must contain at least two lines.", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for a file with empty first line
        [Fact]
		public void ReadDataFromFile_EmptyFirstLineFile_ThrowsError()
		{
			// Arrange
			var lines = new[] { "", "XXXXX", "X...X" };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Error reading input file: First line of input file must contain width and height.", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for a file with incorrect first line
        [Fact]
		public void ReadDataFromFile_IncorrectFirstLineFile_ThrowsError()
		{
			// Arrange
			var testCases = new List<string[]>();
			testCases.Add(new[] { "1", "XXXXX", "X...X" });
			testCases.Add(new[] { "54", "XXXXX", "X...X" });
			testCases.Add(new[] { "5 4 3", "XXXXX", "X...X" });

			for (int i = 0; i < testCases.Count; i++)
			{
				File.WriteAllLines(inputFilePath, testCases[i]); // Simulate valid input file

				// Act & Assert
				var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

				// Check the exception message (optional)
				Assert.Equal("Error reading input file: First line of input file must contain width and height separated by a space.", exception.Message);

				// Cleanup
				File.Delete(IO.inputFilePath); // Remove the test file
			}
		}

		// Test for a file with incorrect width and height
        [Fact]
		public void ReadDataFromFile_IncorrectWidthAndHeightFile_ThrowsError()
		{
			// Arrange
			var testCases = new List<string[]>();
			testCases.Add(new[] { "0 5", "XXXXX", "X...X" });
			testCases.Add(new[] { "5 0", "XXXXX", "X...X" });
			testCases.Add(new[] { "0 0", "XXXXX", "X...X" });

			for (int i = 0; i < testCases.Count; i++)
			{
				File.WriteAllLines(inputFilePath, testCases[i]); // Simulate valid input file

				// Act & Assert
				var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

				// Check the exception message (optional)
				Assert.Equal("Error reading input file: Width and height must be integers between 1 and 75.", exception.Message);

				// Cleanup
				File.Delete(IO.inputFilePath); // Remove the test file
			}
		}

		// Test for a file without full field
        [Fact]
		public void ReadDataFromFile_WithoutFullFieldFile_ThrowsError()
		{
			// Arrange
			var lines = new[] { "5 4", "XXXXX", "X...X", "XXX.X" };

			File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Error reading input file: Input file must contain field of size width x height.", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for a file with incorrect field lines length
        [Fact]
		public void ReadDataFromFile_IncorrectFieldLinesLengthFile_ThrowsError()
		{
			// Arrange
			var testCases = new List<string[]>();
			testCases.Add(new[] { "5 4", "XXXX", "X...X", "XXX.X", ".XXX." });
			testCases.Add(new[] { "5 4", "XXXXX", "X...", "XXX.X", ".XXX." });
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "XXX.", ".XXX." });
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", ".XXX.", ".XXX" });

			for (int i = 0; i < testCases.Count; i++)
			{
				File.WriteAllLines(inputFilePath, testCases[i]); // Simulate valid input file

				// Act & Assert
				var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

				// Check the exception message (optional)
				Assert.Equal("Error reading input file: Field line " + testCases[i][i + 1] + " must contain exactly 5 characters.", exception.Message);

				// Cleanup
				File.Delete(IO.inputFilePath); // Remove the test file
			}
		}

		// Test for a file with incorrect field characters
        [Fact]
		public void ReadDataFromFile_IncorrectFieldCharactersFile_ThrowsError()
		{
			// Arrange
			var testCases = new List<string[]>();
			testCases.Add(new[] { "5 4", "34563", "X...X", "XXX.X", ".XXX." });
			testCases.Add(new[] { "5 4", "XXXXX", "a...X", "XXX.X", ".XXX." });
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "X45.X", ".XXX." });
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".X_X." });

			for (int i = 0; i < testCases.Count; i++)
			{
				File.WriteAllLines(inputFilePath, testCases[i]); // Simulate valid input file

				// Act & Assert
				var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

				// Check the exception message (optional)
				Assert.Equal("Error reading input file: Field line " + testCases[i][i + 1] + " must contain only '.' and 'X' characters.", exception.Message);

				// Cleanup
				File.Delete(IO.inputFilePath); // Remove the test file
			}
		}

		// Test for a file without coords
        [Fact]
		public void ReadDataFromFile_WithoutCoordsFile_ThrowsError()
        {
            // Arrange
            var lines = new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".XXX." };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Error reading input file: Input file must contain at least one set of coordinates.", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for a file with incorrect coords count
        [Fact]
		public void ReadDataFromFile_IncorrectCoordsCountFile_ThrowsError()
		{
			// Arrange
			var testCases = new List<string[]>();
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".XXX.", "2 3 5" });
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".XXX.", "2 3 5 4 5" });

			for (int i = 0; i < testCases.Count; i++)
			{
				File.WriteAllLines(inputFilePath, testCases[i]); // Simulate valid input file

				// Act & Assert
				var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

				// Check the exception message (optional)
				Assert.Equal("Error reading input file: Coordinates must contain exactly 4 integers.", exception.Message);

				// Cleanup
				File.Delete(IO.inputFilePath); // Remove the test file
			}
		}

		// Test for a file with incorrect coords
        [Fact]
		public void ReadDataFromFile_IncorrectCoordsFile_ThrowsError()
		{
			// Arrange
			var testCases = new List<string[]>();
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".XXX.", "-1 3 5 4" });
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".XXX.", "2 -1 5 4" });
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".XXX.", "2 3 -1 4" });
			testCases.Add(new[] { "5 4", "XXXXX", "X...X", "XXX.X", ".XXX.", "2 3 5 -1" });

			for (int i = 0; i < testCases.Count; i++)
			{
				File.WriteAllLines(inputFilePath, testCases[i]); // Simulate valid input file

				// Act & Assert
				var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

				if (i % 2 == 0) 
				{
					// with incorrect x
					// Check the exception message (optional)
					Assert.Equal("Error reading input file: Coordinates x must be integers between 1 and 5.", exception.Message);
				}
				else
				{
					// with incorrect y
					// Check the exception message (optional)
					Assert.Equal("Error reading input file: Coordinates y must be integers between 1 and 4.", exception.Message);
				}

				// Cleanup
				File.Delete(IO.inputFilePath); // Remove the test file
			}
		}
    }
}
