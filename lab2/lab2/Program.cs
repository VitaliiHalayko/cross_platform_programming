using System;
using System.Collections.Generic;
using System.IO;

namespace lab2 {
    class Program {
        static void Main(string[] args)
        {
            // Read the input data
            var (N, field) = IO.readDataFromFile();

            // Solve the problem
            try {
                // Result is a tuple of the maximum weight of eaten mosquitoes and the indices of the eaten mosquitoes
                var (result, mosquitoIndices) = Dynamic.SolveCrazyFrog(N, field);

                // Write the result to the output file
                IO.writeDataToFile(result.ToString());

                // Output the result into the console
                Console.WriteLine($"Max weight of eaten mosquitoes: {result}");
                Console.WriteLine("Indexes of eaten mosquitoes:");
                foreach (var index in mosquitoIndices)
                {
                    Console.WriteLine($"Row: {index.Item1}, Column: {index.Item2}");
                }
            } catch (Exception e) {
                // Write the error message to the output file
                IO.writeDataToFile(e.Message);
                return;
            }
        }
    }
}