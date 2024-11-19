namespace lab1{
	class Program
	{
		public static string Lab1(string[] args)
		{
			if (args.Length != 1)
			{
				return "Usage: lab1 <input_file>";
			}

			string inputFile = args[0];

			int n, k;
			try 
			{
				// Check if the input file exists in the project folder
				(n, k) = IO.readDataFromFile(inputFile);

				int[] permutation = Permutation.getPermutation(n, k);

				return string.Join(" ", permutation);
			} 
			catch (Exception e) 
			{
				// Catch any exceptions (e.g., file read or parse errors) and display the error message
				return e.Message;
			}
		}
	}
}