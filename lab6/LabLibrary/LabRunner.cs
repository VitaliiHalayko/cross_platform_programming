using System;

namespace LabLibrary
{
    public class LabRunner
    {
        public string RunLab1(string inputFile)
        {
            return lab1.Program.Lab1(new string[] { inputFile });
        }

        public string RunLab2(string inputFile)
        {
            return lab2.Program.Lab2(new string[] { inputFile });
        }

        public string RunLab3(string inputFile)
        {
            return lab3.Program.Lab3(new string[] { inputFile });
        }
    }
}
