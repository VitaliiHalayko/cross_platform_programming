using System;

namespace lab3
{
    public class FindPath
    {
        // Width and height of the field
        private int width;
        private int height;
        private char[,] field; // Char array representing the field (with 'x' for obstacles)
        private int[,] grid;   // Integer array for pathfinding (tracks movement states)

        // Constructor to initialize the field, grid, and set width/height
        public FindPath(int width, int height, char[,] field)
        {
            this.width = width;
            this.height = height;
            this.field = field;
            this.grid = new int[width + 4, height + 4]; // Extra border space for easier boundary handling
            InitializeGrid(); // Set up the grid based on the field
        }

        // Method to find the path from (startX, startY) to (endX, endY)
        public int findPath(int startX, int startY, int endX, int endY)
        {
            int stepCount = 0;  // Counter for how many steps were taken to find the path
            bool pathFound = true; // Boolean to check if a path exists

            // Adjust start and end positions due to grid having extra borders
            startX++;
            startY++;
            endX++;
            endY++;

            // Mark start position with '2' and end position with '0' in the grid
            grid[startX, startY] = 2;
            grid[endX, endY] = 0;

            // Loop to incrementally find the path
            while (true)
            {
                stepCount++;
                pathFound = true;

                // Loop through the grid and look for cells marked as '2' (active exploration front)
                for (int i = 1; i < width + 3; i++)
                {
                    for (int j = 1; j < height + 3; j++)
                    {
                        if (grid[i, j] == 2) // If the cell is part of the active front
                        {
                            Spread(grid, i, j); // Spread to adjacent cells
                            grid[i, j] = 3; // Mark the cell as explored
                            pathFound = false; // Path still being explored
                        }
                    }
                }

                // If no cells were spread to, the path does not exist
                if (pathFound)
                {
                    return 0; // No path found, return 0
                }

                // Update the grid, convert '4' (marked for spread) to '2' (active front)
                UpdateGrid(grid, width, height);

                // If the destination was reached, return the number of steps
                if (grid[endX, endY] == 2)
                {
                    ResetGrid(grid, startX, startY, endX, endY, width, height); // Reset the grid for the next use
                    return stepCount; // Return the number of steps taken to reach the destination
                }
            }
        }

        // Method to initialize the grid based on the field (sets obstacles in the grid)
        private void InitializeGrid()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // 'x' in the field indicates an obstacle, mark as '1' in the grid
                    if (char.ToLower(field[j, i]) == 'x')
                    {
                        grid[j + 2, i + 2] = 1;
                    }
                }
            }
        }

        // Method to spread exploration to adjacent cells (marked as '4')
        private void Spread(int[,] grid, int i, int j)
        {
            if (grid[i + 1, j] == 0) grid[i + 1, j] = 4; // Spread right
            if (grid[i - 1, j] == 0) grid[i - 1, j] = 4; // Spread left
            if (grid[i, j + 1] == 0) grid[i, j + 1] = 4; // Spread down
            if (grid[i, j - 1] == 0) grid[i, j - 1] = 4; // Spread up
        }

        // Method to update the grid, convert '4' (spread marker) to '2' (active front)
        private void UpdateGrid(int[,] grid, int width, int height)
        {
            for (int i = 1; i < width + 3; i++)
            {
                for (int j = 1; j < height + 3; j++)
                {
                    if (grid[i, j] == 4) grid[i, j] = 2;
                }
            }
        }

        // Method to reset the grid after finding a path (cleans up for the next run)
        private void ResetGrid(int[,] grid, int startX, int startY, int endX, int endY, int width, int height)
        {
            grid[startX, startY] = 1; // Reset the start point
            grid[endX, endY] = 1;     // Reset the end point

            // Clear any cells that are not obstacles (marked as '1') back to empty
            for (int i = 1; i < width + 3; i++)
            {
                for (int j = 1; j < height + 3; j++)
                {
                    if (grid[i, j] != 1) grid[i, j] = 0;
                }
            }
        }
    }
}
