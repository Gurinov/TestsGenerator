using System;
using System.Collections.Generic;
using TestsGenerator.Action;

namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<string> filePaths = new[]
            {
                @"C:\Users\Gurinov\Desktop\TestsGenerator\TestsGenerator\Action\FileReader.cs",
                @"C:\Users\Gurinov\Desktop\TestsGenerator\TestsGenerator\Action\FileWriter.cs",
                @"C:\Users\Gurinov\Desktop\TestsGenerator\TestsGenerator\model\Class.cs",
                @"C:\Users\Gurinov\Desktop\TestsGenerator\TestsGenerator\model\Params.cs"
            };
            FileReader fr = new FileReader(filePaths, 2);
            IEnumerable<string> x = fr.ReadClassesFromFile();
            foreach (var VARIABLE in x)
            {
                Console.Out.WriteLine(VARIABLE);
            }
        }
    }
}