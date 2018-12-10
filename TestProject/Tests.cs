using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using TestsGenerator.Action;
using TestsGenerator.model;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        private TestsGenerator.TestsGenerator _testsGenerator;
        private CompilationUnitSyntax _testCompilationUnit;
        private FileWriter _asyncWriter;
        private string _fullPath;
        private string _resultPath;
        private List<string> _filePaths;
        private Params _params;

        [SetUp]
        public void SetUp()
        {
            _fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _resultPath = _fullPath + @"\..\..\..\Tests";

            _filePaths = new List<string>
            {
                _fullPath + @"\..\..\..\TestsGenerator\Action\FileWriter.cs",
                _fullPath + @"\..\..\..\TestsGenerator\Action\FileReader.cs",
                _fullPath + @"\..\..\..\TestsGenerator\Generator.cs",
                _fullPath + @"\..\..\..\TestsGenerator\SyntaxProcessor.cs",
                _fullPath + @"\..\..\..\TestsGenerator\TestsGenerator.cs"
            };
            _params = new Params(_resultPath, 2, 2, 2);
            _testsGenerator = new TestsGenerator.TestsGenerator(_filePaths, _params);
            _asyncWriter = new FileWriter(_params.OutputDirectoryPath);
        }

        private void DaleteFile()
        {
            foreach(string filePath in _filePaths)
            {
                string pathToResFile = _resultPath + "\\" + Path.GetFileNameWithoutExtension(filePath) + "Tests.cs";
                File.Delete(pathToResFile);
            }
        }

        [Test]
        public void GenerateTests()
        {
            DaleteFile();
            int startCountFiles = Directory.GetFiles(_resultPath).Length;
            _testsGenerator.Generate(_asyncWriter).Wait();
            int currentCountFiles = Directory.GetFiles(_resultPath).Length;
            int expectedCount = startCountFiles + _filePaths.Count;
            Assert.AreEqual(expectedCount, currentCountFiles);
            DaleteFile();
        }
        
        [Test]
        public void GenerateTestsToNotExistDirectory()
        {
            string newResultPath = _resultPath + "1";
            Assert.IsFalse(Directory.Exists(newResultPath));
            Params newParams = new Params(newResultPath, 2, 2, 2);
            Assert.IsTrue(Directory.Exists(newResultPath));
            Directory.Delete(newResultPath);
        }
        
        
        [Test]
        public void TwoClassInOneFile()
        {
            DaleteFile();
            List<string> filePaths = new List<string>
            {
                _fullPath + @"\..\..\TwoClassInOneFile.cs"
            };
            TestsGenerator.TestsGenerator generator =
                new TestsGenerator.TestsGenerator(filePaths, _params);
            FileWriter asyncWriter = new FileWriter(_params.OutputDirectoryPath);
            
            int startCountFiles = Directory.GetFiles(_resultPath).Length;
            generator.Generate(asyncWriter).Wait();
            int currentCountFiles = Directory.GetFiles(_resultPath).Length;
            int expectedCount = startCountFiles + filePaths.Count + 1;
            Assert.AreEqual(expectedCount, currentCountFiles);
            File.Delete(_resultPath + @"\FirstClassTest.cs");
            File.Delete(_resultPath + @"\SecondClassTest.cs");
        }
        
    }
}