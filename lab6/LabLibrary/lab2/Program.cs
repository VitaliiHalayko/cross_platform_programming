using System;
using System.Collections.Generic;
using System.IO;

namespace lab2 {
    class Program {
        public static string Lab2(string[] args)
        {
            if (args.Length != 1)
            {
                return "Usage: lab2 <input file>";
            }

            string inputFile = args[0];

            // Read the input data
            var (N, field) = IO.readDataFromFile(inputFile);

            // Solve the problem
            try {
                // Result is a tuple of the maximum weight of eaten mosquitoes and the indices of the eaten mosquitoes
                var (result, mosquitoIndices) = Dynamic.SolveCrazyFrog(N, field);

                // Write the result to the output file
                return result.ToString();
            } catch (Exception e) {
                // Write the error message to the output file
                return e.Message;
            }
        }
    }
}