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
            List<string> filePaths = new List<string>
            {
                @"..\..\..\TestsGenerator\Action\FileWriter.cs",
                @"..\..\..\TestsGenerator\Action\FileReader.cs",
                @"..\..\..\TestsGenerator\Generator.cs",
                @"..\..\..\TestsGenerator\SyntaxProcessor.cs",
                @"..\..\..\TestsGenerator\TestsGenerator.cs"
            };
            Params myParams = new Params(@"..\..\..\Tests", 2, 2, 2);
            TestsGenerator.TestsGenerator generator =
                new TestsGenerator.TestsGenerator(filePaths, myParams);
            FileWriter asyncWriter = new FileWriter(myParams.OutputDirectoryPath);
            generator.Generate(asyncWriter).Wait();
            Console.WriteLine("Success");
        }
    }
}