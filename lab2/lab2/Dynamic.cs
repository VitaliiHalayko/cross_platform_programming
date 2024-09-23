using System;

namespace lab2
{
	public static class Dynamic 
	{
		public static (int, List<(int, int)>) SolveCrazyFrog(int N, int[,] field)
        {
            // Initialize the dynamic programming table
            int[,] dp = new int[N + 1, N + 1];

            // Fill the dynamic programming table with zeros
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    dp[i, j] = 0;
                }
            }

            // Initialize the indices table
            List<(int, int)>[,] indices = new List<(int, int)>[N + 1, N + 1];

            // Fill the indices table with empty lists
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    indices[i, j] = new List<(int, int)>();
                }
            }

            // Fill the dynamic programming table
            for (int i = 1; i <= N; i++) 
            {   
                // Iterate over the possible sums of eaten mosquitoes
                for (int s = 0; s <= N; s++) 
                { 
                    // Iterate over the possible number of eaten mosquitoes
                    for (int j = 0; j < N; j++) 
                    {
                        // Check if the sum is greater than or equal to the number of eaten mosquitoes
                        if (s >= j)
                        {   
                            // Check if the current value is less than the new value
                            if (dp[i, s] < dp[i - 1, s - j] + field[j, i - 1])
                            {
                                dp[i, s] = dp[i - 1, s - j] + field[j, i - 1]; // Update the value
                                indices[i, s] = new List<(int, int)>(indices[i - 1, s - j]); // Update the indices
                                indices[i, s].Add((j, i - 1)); // Add the new index
                            }
                        }
                    }
                }
            }

            // Return the result
            return (dp[N, N], indices[N, N]);
        }
	}
}