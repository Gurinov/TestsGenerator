using System.Collections.Generic;
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
        
        public async void WriteClassesFromFile(List<Class> testClasses)
        {
            foreach (var thisClass in testClasses)
            {
                string filePath = $"{outputDirectory}\\{thisClass.Name}";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    await writer.WriteAsync(thisClass.Data);
                }       
            }
        }
    }
}