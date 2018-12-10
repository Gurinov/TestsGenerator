using System.IO;
using System.Threading.Tasks;

namespace TestsGenerator.Action
{
    public class FileReader
    {
        public async Task<string> ReadClassesFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}