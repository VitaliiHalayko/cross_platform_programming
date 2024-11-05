using System.Runtime.CompilerServices;

namespace lab1
{
	public static class IO 
	{
		public static (int n, int k) readDataFromFile(string input) 
		{
			// Read all lines from the file
			string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			if (lines.Length == 2)
			{
				// Parse the first line as integer 'n' and the second line as integer 'k'
				try {
					int n = int.Parse(lines[0]);
					int k = int.Parse(lines[1]);

					return (n, k);
				} catch (FormatException e) {
					// If the file contains invalid data, throw an error message
					throw new IOException("Input data is incorrect! The file must contain exactly 2 int values.");
				}
			}
			else
			{
				// If the file does not contain exactly 2 lines, throw an error message
				throw new IOException("Input data is incorrect! The file must contain exactly 2 lines.");
			}
		}
	}	
}