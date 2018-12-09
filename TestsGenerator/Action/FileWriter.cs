using System.IO;
using TestsGenerator.model;

namespace TestsGenerator.Action
{
    public class FileWriter
    {
        public void WriteClassesFromFile(Class testClassInMemory, string outputDirectoryPath)
        {
            string filePath = $"{outputDirectoryPath}\\{testClassInMemory.ClassName}";
            File.WriteAllText(filePath, testClassInMemory.ClassData);
        }
    }
}