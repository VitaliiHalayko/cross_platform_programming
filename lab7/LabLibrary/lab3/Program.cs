using System.Runtime.CompilerServices;

namespace lab3{
	class Program
	{
		public static string Lab3(string[] args)
		{
			if (args.Length != 1)
			{
				return "Usage: lab3 <input file>";
			}

			string inputFile = args[0];
			
			try {
				// read data from file
				(int width, int height, char[,] field, List<int[]> coords) = IO.readDataFromFile(inputFile);

				// initialize path finder
				FindPath pathFinder = new FindPath(width, height, field);

				// list to store results
				List<int> results = new List<int>();

				// find paths for each set of coordinates
				foreach (int[] coord in coords)
				{
					results.Add(pathFinder.findPath(coord[0], coord[1], coord[2], coord[3]));
				}
				
				if (results.Count == 0)
				{
					return "Results list is empty.";
				}

				return string.Join("\n", results);
			}
			catch (Exception e)
			{
				// write error to file
				return e.Message;
			}
		}
	}
}
