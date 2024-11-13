using System.Runtime.CompilerServices;

namespace lab2
{
	public static class IO
    {
        public static (int n, int[,] weights) readDataFromFile(string input)
        {
            // Try to read the input file
            try
            {
                // Read all lines from the file
                string[] inputLines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Check if the file is empty
                if (inputLines.Length == 0)
                {
                    // If the file is empty, throw an error message
                    throw new IOException("Input file is empty!");
                }

                // Try to parse the first line of the file
                int N = int.Parse(inputLines[0].Trim());

                if (N < 1 || N > 50)
                {
                    throw new IOException("N must be between 1 and 50.");
                }

                // Check if the file contains the correct number of lines
                if (inputLines.Length != N + 1)
                {
                    // If the file contains invalid data, throw an error message
                    throw new IOException("Input file must contain first line with N and then N lines with N integers between 1 and 50.");
                }

                int[,] field = new int[N, N];

                // Try to parse the remaining lines of the file
                for (int i = 0; i < N; i++)
                {
                    string[] rowValues = inputLines[i + 1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (rowValues.Length != N)
                    {
                        throw new IOException("Input file must contain first line with N and then N lines with N integers between 1 and 50.");
                    }

                    // Try to parse the elements of the row
                    for (int j = 0; j < N; j++)
                    {
                        if (!int.TryParse(rowValues[j], out field[i, j]))
                        {
                            throw new FormatException($"Incorrect number format in row {i}, column {j}");
                        }
                    }
                }

                return (N, field);
            }
            catch (FormatException e)
            {
                // If the file contains invalid data, throw an error message
                throw new IOException("Error occurred while parsing the input file: " + e.Message);
            }
        }
    }
}