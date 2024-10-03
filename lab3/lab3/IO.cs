using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("lab3.Tests")]
namespace lab3
{
	public static class IO
    {
        public const string inputFilePath = "INPUT.TXT";
        public const string outputFilePath = "OUTPUT.TXT";

        public static (int width, int height, char[,] field, List<int[]> coords) readDataFromFile()
        {
			if (!File.Exists(inputFilePath)) 
			{
				throw new IOException("Input file not found.");
			}

			try 
			{
				string[] inputLines = File.ReadAllLines(inputFilePath);

				// check if the input file is empty
				if (inputLines.Length < 2) 
				{
					throw new IOException("Input file must contain at least two lines.");
				}

				if (string.IsNullOrEmpty(inputLines[0])) 
				{
					throw new IOException("First line of input file must contain width and height.");
				}

				string[] firstLine = inputLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

				if (firstLine.Length != 2) 
				{
					throw new IOException("First line of input file must contain width and height separated by a space.");
				}

				// initialize variables
				int width = int.Parse(firstLine[0]);
				int height = int.Parse(firstLine[1]);

				if (width < 1 || height < 1 || width > 75 || height > 75) 
				{
					throw new IOException("Width and height must be integers between 1 and 75.");
				}

				if (inputLines.Length < height + 1) 
				{
					throw new IOException("Input file must contain field of size width x height.");
				}

				// initialize field and coords
				char[,] field = new char[width, height];
				List<int[]> coords = new List<int[]>();

				// read field
				for (int i = 1; i <= height; i++)
				{
					string line = inputLines[i].Trim();
					if (line.Length != width)
					{
						throw new IOException("Field line " + line + " must contain exactly " + width + " characters.");
					}

					for (int j = 0; j < width; j++)
					{
						if (line[j] != '.' && line[j] != 'X')
						{
							throw new IOException("Field line " + line + " must contain only '.' and 'X' characters.");
						}
						
						field[j, i - 1] = line[j];
					}
				}

				if (height + 1 == inputLines.Length) 
				{
					throw new IOException("Input file must contain at least one set of coordinates.");
				}

				// read coords
				int lineIndex = height + 1;
				bool shouldExit = false;  // Flag to indicate if we should exit the while loop

				while (lineIndex < inputLines.Length && coords.Count < 20 && !shouldExit)
				{
					string[] coordsStr = inputLines[lineIndex].Split(' ', StringSplitOptions.RemoveEmptyEntries);
					
					if (coordsStr.Length != 4)
					{
						throw new IOException("Coordinates must contain exactly 4 integers.");
					} 
					else 
					{
						int[] coord = new int[4];
						for (int j = 0; j < 4; j++)
						{
							if (j % 2 == 0) 
							{
								int x = int.Parse(coordsStr[j]);
								if (x == 0) 
								{
									shouldExit = true;  // Set flag to exit while loop
									break;  // Exit for loop
								}
								if (x < 1 || x > width) 
								{
									throw new IOException("Coordinates x must be integers between 1 and " + width + ".");
								}

								coord[j] = x;
							}
							else
							{
								int y = int.Parse(coordsStr[j]);
								if (y == 0)
								{
									shouldExit = true;  // Set flag to exit while loop
									break;  // Exit for loop
								}
								if (y < 1 || y > height) 
								{
									throw new IOException("Coordinates y must be integers between 1 and " + height + ".");
								}

								coord[j] = y;
							}
						}

						// Only add the coordinates if we didn't exit the loop early
						if (!shouldExit) 
						{
							coords.Add(coord);
						}
					}

					lineIndex++;
				}

				return (width, height, field, coords);
			}
			catch (Exception e) {
				throw new IOException("Error reading input file: " + e.Message);
			}
        }

        // Write the result to the output file
        public static void writeResultsToFile(List<int> results)
		{
			if (results.Count == 0)
			{
				throw new IOException("Results list is empty.");
			}

			string result = string.Join("\n", results);
			File.WriteAllText(outputFilePath, result);
		}

        // Write the error message to the output file
		public static void writeErrorToFile(string message)
		{
			File.WriteAllText(outputFilePath, message);
		}
    }
}