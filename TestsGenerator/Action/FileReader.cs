using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TestsGenerator.Action
{
    public class FileReader
    {
        private readonly IEnumerable<string> _filePaths;
        private readonly ParallelOptions _maxReadingTasksCount;

        public FileReader(IEnumerable<string> filePaths) : this(filePaths, -1)
        {
        }

        public FileReader(IEnumerable<string> filePaths, int maxReadingTasksCount)
        {
            _filePaths = filePaths;
            _maxReadingTasksCount = new ParallelOptions { MaxDegreeOfParallelism = maxReadingTasksCount };
        }

        public IEnumerable<string> ReadClassesFromFile()
        {
            List<string> sourceCodesBuffer = new List<string>();

            Parallel.ForEach(_filePaths, _maxReadingTasksCount, filePath => {
                sourceCodesBuffer.Add(File.ReadAllText(filePath));
            });
            return sourceCodesBuffer;
        }
    }
}