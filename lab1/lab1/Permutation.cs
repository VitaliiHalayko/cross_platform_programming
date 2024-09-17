namespace lab1;

public static class Permutation 
{
	// Precomputed list of factorials from 1! to 12!
	// Used to calculate the total number of permutations for a given n
	static List<long> factorials = new List<long> { 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600 };

	// Function to generate the k-th permutation of numbers 1 to n
	public static int[] getPermutation(int n, int k) 
	{
        if (n >= 1 && n <= 12 && k >= 1 && k <= factorials[n - 1]) {
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
        else
        {
            throw new ArgumentException("Invalid input! n must be between 1 and 12, and k must be between 1 and n!");
        }
	}
}