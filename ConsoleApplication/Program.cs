using System;
using System.Collections.Generic;
using TestsGenerator.Action;
using TestsGenerator.model;

namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> filePaths = new List<string>()
            {
                @"C:\Users\Gurinov\Desktop\TestsGenerator\TestsGenerator\Action\FileReader.cs",
                @"C:\Users\Gurinov\Desktop\TestsGenerator\TestsGenerator\Action\FileWriter.cs",
                @"C:\Users\Gurinov\Desktop\TestsGenerator\TestsGenerator\model\Class.cs",
                @"C:\Users\Gurinov\Desktop\TestsGenerator\TestsGenerator\model\Params.cs"
            };
            TestsGenerator.TestsGenerator generator =
                new TestsGenerator.TestsGenerator(filePaths, new Params(@"C:\Users\Gurinov\Desktop\q", 2, 2, 2));
            FileWriter asyncWriter = new FileWriter(@"C:\Users\Gurinov\Desktop\q");
            generator.Generate(asyncWriter).Wait();
            Console.WriteLine("Success");
        }
    }
}