using System;
using System.IO;
using Xunit;
using Moq;

namespace lab2.Tests
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
            var lines = new[] { "3", "8 2 1", "1 2 6", "2 7 2" };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

            // Act
            var (N, mosquitoes) = IO.readDataFromFile();

            // Assert
            Assert.Equal(3, N);
            Assert.Equal(8, mosquitoes[0, 0]);
			Assert.Equal(2, mosquitoes[0, 1]);
			Assert.Equal(1, mosquitoes[0, 2]);
			Assert.Equal(1, mosquitoes[1, 0]);
			Assert.Equal(2, mosquitoes[1, 1]);
			Assert.Equal(6, mosquitoes[1, 2]);
			Assert.Equal(2, mosquitoes[2, 0]);
			Assert.Equal(7, mosquitoes[2, 1]);
			Assert.Equal(2, mosquitoes[2, 2]);

            // Cleanup
            File.Delete(inputFilePath); // Remove the test file
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
			Assert.Equal("Input file not found!", exception.Message);
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
			Assert.Equal("Input file is empty!", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for a incorrect N number in file
        [Fact]
		public void ReadDataFromFile_IncorrectNInFile_ThrowsError()
		{
			// Arrange
			// Create an empty input file
			var lines = new[] { "54", "8 2 1", "1 2 6", "2 7 2" };
            File.WriteAllLines(inputFilePath, lines);

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("N must be between 1 and 50.", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}
		
		// Test for a invalid lines count file
        [Fact]
		public void ReadDataFromFile_InvalidFilesFile_ThrowsError()
		{
			// Arrange
			var lines = new[] { "3", "8 2 1", "1 2 6" };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Input file must contain first line with N and then N lines with N integers between 1 and 50.", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for a invalid data count in line file
        [Fact]
		public void ReadDataFromFile_InvalidDataCountInLineFile_ThrowsError()
		{
			// Arrange
			var lines = new[] { "3", "8 2 1", "1 2 6 4", "1 2 6" };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Input file must contain first line with N and then N lines with N integers between 1 and 50.", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for a invalid data in line file
        [Fact]
		public void ReadDataFromFile_InvalidDataInLineFile_ThrowsError()
		{
			// Arrange
			var lines = new[] { "3", "8 2 f", "1 2 6 4", "1 2 6" };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

			// Act & Assert
			var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());

			// Check the exception message (optional)
			Assert.Equal("Error occurred while parsing the input file: Incorrect number format in row 0, column 2", exception.Message);

			// Cleanup
			File.Delete(IO.inputFilePath); // Remove the test file
		}

		// Test for writing error message to file
        [Fact]
        public void WriteErrorToFile_ValidMessage_WritesCorrectly()
        {
            // Arrange
            var message = "Hello world!";

            // Act
            IO.writeDataToFile(message);

            // Assert
            var output = File.ReadAllText(outputFilePath);
            Assert.Equal(message, output);

            // Cleanup
            File.Delete(outputFilePath);
        }
    }
}
