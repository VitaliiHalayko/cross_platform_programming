namespace lab1{
	class Program
	{
		public static void Lab1(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Usage: lab1 <input_file> <output_file>");
				return;
			}

			string inputFile = args[0];
			string outputFile = args[1];

			int n, k;
			try 
			{
				// Check if the input file exists in the project folder
				(n, k) = IO.readDataFromFile(inputFile);

				int[] permutation = Permutation.getPermutation(n, k);

				IO.writePermutationToFile(outputFile, permutation);
			} 
			catch (Exception e) 
			{
				// Catch any exceptions (e.g., file read or parse errors) and display the error message
				IO.writeErrorToFile(outputFile, e.Message);
			}
		}
	}
}