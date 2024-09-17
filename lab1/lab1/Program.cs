namespace lab1{
	class Program
	{
		static void Main(string[] args)
		{
			int n, k;
			try 
			{
				// Check if the input file exists in the project folder
				(n, k) = IO.readDataFromFile();

				int[] permutation = Permutation.getPermutation(n, k);

				IO.writePermutationToFile(permutation);
			} 
			catch (Exception e) 
			{
				// Catch any exceptions (e.g., file read or parse errors) and display the error message
				IO.writeErrorToFile(e.Message);
			}
		}
	}
}