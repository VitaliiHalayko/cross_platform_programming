using System.Runtime.CompilerServices;

namespace lab3{
	class Program
	{
		static void Main()
		{
			try {
				// read data from file
				(int width, int height, char[,] field, List<int[]> coords) = IO.readDataFromFile();

				// initialize path finder
				FindPath pathFinder = new FindPath(width, height, field);

				// list to store results
				List<int> results = new List<int>();

				// find paths for each set of coordinates
				foreach (int[] coord in coords)
				{
					results.Add(pathFinder.findPath(coord[0], coord[1], coord[2], coord[3]));
				}

				// write results to file
				IO.writeResultsToFile(results);
			}
			catch (Exception e)
			{
				// write error to file
				IO.writeErrorToFile(e.Message);
			}
		}
	}
}
