using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("lab1.Tests")]
class Program
{
	// Precomputed list of factorials from 1! to 12!
	// Used to calculate the total number of permutations for a given n
	static List<long> factorials = new List<long> { 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600 };

    static void Main(string[] args)
    {
		// File path where n and k are stored
		string inputFilePath = "INPUT.txt";
		string outputFilePath = "OUTPUT.txt";

		try 
		{
			// Check if the input file exists in the project folder
			if (File.Exists(inputFilePath))
        	{
				// Read all lines from the file
				string[] lines = File.ReadAllLines(inputFilePath);

				// Parse the first line as integer 'n' and the second line as integer 'k'
				int n = int.Parse(lines[0]);
				int k = int.Parse(lines[1]);

				// Validate that n is within the allowed range (1 <= n <= 12) 
				// and k is a valid permutation index (1 <= k <= factorial[n-1])
				if (n >= 1 && n <= 12 && k >= 1 && k <= factorials[n - 1])
				{
					// Get the k-th permutation of numbers from 1 to n
					int[] result = getPermutation(n, k);

					// Write the result to the output file
					File.WriteAllText(outputFilePath, string.Join(" ", result));
				}
				else
				{
					// If the input values are out of range, display an error message
					Console.WriteLine("Input data is incorrect! Input data must be: 1 <= n <= 12, 1 <= k <= factorial(n)!");
				}
			}
			else
			{
				// If the file is not found, display an error message
				Console.WriteLine($"File {inputFilePath} not found in project folder lab1!");
			}
		} 
		catch (Exception e) 
		{
			// Catch any exceptions (e.g., file read or parse errors) and display the error message
			Console.WriteLine(e.Message);
		}
	}

	// Function to generate the k-th permutation of numbers 1 to n
	public static int[] getPermutation(int n, int k) 
	{
		// Initialize an array with values from 1 to n
		int[] a = Enumerable.Range(1, n).ToArray();
		int i, j, temp;

        // Repeat the permutation generation process until we reach the k-th permutation
        while (--k > 0) 
        {
            // Step 1: Find the largest index i such that a[i - 1] < a[i]
			// This is the first inversion point from the right
            i = a.Length - 1;
            while (i > 0 && a[i] <= a[i - 1]) 
            {
                i--;
            }

            // If no such index exists, the array is in descending order (last permutation)
			// In that case, just return the current state of the array
            if (i == 0) 
            {
                break;
            }

            // Step 2: Find the largest index j such that a[j] > a[i - 1]
			// This will allow us to swap and form the next permutation
            j = a.Length - 1;
            while (a[j] <= a[i - 1]) 
            {
                j--;
            }

            // Step 3: Swap the elements a[i - 1] and a[j]
			// This allows us to "step" to the next lexicographical permutation
            temp = a[i - 1];
            a[i - 1] = a[j];
            a[j] = temp;

            // Step 4: Reverse the sequence from a[i] to the end of the array
			// This ensures that the rest of the array forms the smallest possible sequence
            j = a.Length - 1;
            while (i < j) 
            {
                temp = a[i];
                a[i] = a[j];
                a[j] = temp;
                i++;
                j--;
            }
        }

		// Return the k-th permutation of the array
		return a;
	}
}
