using System.IO;
using TestsGenerator.model;

namespace TestsGenerator.Action
{
    public class FileWriter
    {
        private string outputDirectory;

        public FileWriter(string outputDirectory)
        {
            this.outputDirectory = outputDirectory;
        }
        
        public async void WriteClassesFromFile(Class testClassInMemory)
        {
            string filePath = $"{outputDirectory}\\{testClassInMemory.ClassName}";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(testClassInMemory.ClassData);
            }
        }
    }
}