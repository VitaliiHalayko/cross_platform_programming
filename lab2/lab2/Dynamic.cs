using System;

namespace lab2
{
	public static class Dynamic 
	{
		public static (int, List<(int, int)>) SolveCrazyFrog(int N, int[,] field)
        {
            // Initialize the dynamic programming table
            // dp[col, eatenCount, rowSum] represents the maximum value achievable
            int[,,] dp = new int[N + 1, N + 1, N + 1];
            
            // Initialize the indices table
            List<(int, int)>[,,] indices = new List<(int, int)>[N + 1, N + 1, N + 1];
            
            // Fill the indices table with empty lists
            for (int i = 0; i <= N; i++)
                for (int j = 0; j <= N; j++)
                    for (int k = 0; k <= N; k++)
                        indices[i, j, k] = new List<(int, int)>();
            
            // Fill the dynamic programming table
            for (int col = 1; col <= N; col++)
            {
                for (int eatenCount = 0; eatenCount <= N; eatenCount++)
                {
                    for (int rowSum = 0; rowSum <= N; rowSum++)
                    {
                        // Option 1: Don't eat in this column
                        if (col > 1)
                        {
                            if (dp[col - 1, eatenCount, rowSum] > dp[col, eatenCount, rowSum])
                            {
                                dp[col, eatenCount, rowSum] = dp[col - 1, eatenCount, rowSum];
                                indices[col, eatenCount, rowSum] = new List<(int, int)>(indices[col - 1, eatenCount, rowSum]);
                            }
                        }

                        // Option 2: Eat in this column
                        if (eatenCount > 0)
                        {
                            for (int row = 1; row <= N; row++)
                            {
                                if (rowSum >= row)
                                {
                                    int newValue = (col > 1 ? dp[col - 1, eatenCount - 1, rowSum - row] : 0) + field[row - 1, col - 1];
                                    if (newValue > dp[col, eatenCount, rowSum])
                                    {
                                        dp[col, eatenCount, rowSum] = newValue;
                                        indices[col, eatenCount, rowSum] = col > 1 
                                            ? new List<(int, int)>(indices[col - 1, eatenCount - 1, rowSum - row]) 
                                            : new List<(int, int)>();
                                        indices[col, eatenCount, rowSum].Add((row, col));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            // Find the maximum value where rowSum equals N
            int maxValue = 0;
            int maxEatenCount = 0;
            for (int eatenCount = 1; eatenCount <= N; eatenCount++)
            {
                if (dp[N, eatenCount, N] > maxValue)
                {
                    maxValue = dp[N, eatenCount, N];
                    maxEatenCount = eatenCount;
                }
            }
            
            // Return the result
            return (maxValue, indices[N, maxEatenCount, N]);
        }
	}
}