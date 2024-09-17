using System;
using System.IO;
using Xunit;
using Moq;

namespace lab1.Tests
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
            var lines = new[] { "5", "3" };
            File.WriteAllLines(inputFilePath, lines); // Simulate valid input file

            // Act
            var result = IO.readDataFromFile();

            // Assert
            Assert.Equal(5, result.n);
            Assert.Equal(3, result.k);

            // Cleanup
            File.Delete(inputFilePath); // Remove the test file
        }

        // Test for invalid data (not integers)
        [Fact]
        public void ReadDataFromFile_InvalidData_ThrowsIOException()
        {
            // Arrange
            var lines = new[] { "five", "three" }; // Non-numeric strings
            File.WriteAllLines(inputFilePath, lines);

            // Act & Assert
            var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());
            Assert.Equal("Input data is incorrect! The file must contain exactly 2 int values.", exception.Message);

            // Cleanup
            File.Delete(inputFilePath);
        }

        // Test for incorrect number of lines
        [Fact]
        public void ReadDataFromFile_IncorrectNumberOfLines_ThrowsIOException()
        {
            // Arrange
            var lines = new[] { "5" }; // Only one line
            File.WriteAllLines(inputFilePath, lines);

            // Act & Assert
            var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());
            Assert.Equal("Input data is incorrect! The file must contain exactly 2 lines.", exception.Message);

            // Cleanup
            File.Delete(inputFilePath);
        }

        // Test for file not found
        [Fact]
        public void ReadDataFromFile_FileNotFound_ThrowsIOException()
        {
            // Arrange
            if (File.Exists(inputFilePath))
            {
                File.Delete(inputFilePath);
            }

            // Act & Assert
            var exception = Assert.Throws<IOException>(() => IO.readDataFromFile());
            Assert.Equal("Input data is incorrect! The file must contain exactly 2 lines.", exception.Message);
        }

        // Test for writing permutation to file
        [Fact]
        public void WritePermutationToFile_ValidData_WritesCorrectly()
        {
            // Arrange
            var permutation = new[] { 1, 2, 3 };

            // Act
            IO.writePermutationToFile(permutation);

            // Assert
            var output = File.ReadAllText(outputFilePath);
            Assert.Equal("1 2 3", output);

            // Cleanup
            File.Delete(outputFilePath);
        }

        // Test for writing error message to file
        [Fact]
        public void WriteErrorToFile_ValidMessage_WritesCorrectly()
        {
            // Arrange
            var errorMessage = "An error occurred!";

            // Act
            IO.writeErrorToFile(errorMessage);

            // Assert
            var output = File.ReadAllText(outputFilePath);
            Assert.Equal(errorMessage, output);

            // Cleanup
            File.Delete(outputFilePath);
        }
    }
}
