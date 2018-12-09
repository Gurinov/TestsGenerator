using System.IO;

namespace TestsGenerator.model
{
    public class Params
    {
        public int MaxProcessingCount { get; }
        public int MaxWritingCount { get; }
        public int MaxReadingCount { get; }
        public string OutputDirectoryPath { get; }

        public Params(string outputDirectoryPath, int maxProcessingTasksCount, int maxWritingCount,
            int maxReadingCount)
        {
            OutputDirectoryPath = outputDirectoryPath;
            MaxProcessingCount = maxProcessingTasksCount;
            MaxWritingCount = maxWritingCount;
            MaxReadingCount = maxReadingCount;
            if (!Directory.Exists(OutputDirectoryPath))
            {
                Directory.CreateDirectory(OutputDirectoryPath);
            }
        }
    }
}