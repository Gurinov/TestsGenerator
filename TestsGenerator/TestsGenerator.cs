using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestsGenerator.Action;
using TestsGenerator.model;

namespace TestsGenerator
{
    public class TestsGenerator
    {
        private Params _params;
        private List<string> fileNames;

        public TestsGenerator(List<string> fileNames, Params initParams)
        {
            this.fileNames = new List<string>(fileNames);
            _params = initParams;
        }

        public async Task Generate(FileWriter writer)
        {
            DataflowLinkOptions linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            ExecutionDataflowBlockOptions maxReadableFilesTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = _params.MaxReadingCount
            };

            ExecutionDataflowBlockOptions maxProcessableTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = _params.MaxProcessingCount
            };

            ExecutionDataflowBlockOptions maxWritableTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = _params.MaxWritingCount
            };
            
            FileReader asyncReader = new FileReader();
            Generator generator = new Generator();

            TransformBlock<string, string> readingBlock = 
                new TransformBlock<string, string>(new Func<string, Task<string>>(asyncReader.ReadClassesFromFile), maxReadableFilesTasks);

            TransformBlock<string, List<Class>> processingBlock =
                new TransformBlock<string, List<Class>>(new Func<string, List<Class>>(generator.GetTemplate), maxProcessableTasks);

            ActionBlock<List<Class>> writingBlock = new ActionBlock<List<Class>>(
                (classTemplate) => writer.WriteClassesFromFile(classTemplate), maxWritableTasks);

            readingBlock.LinkTo(processingBlock, linkOptions);
            processingBlock.LinkTo(writingBlock, linkOptions);

            foreach (string filePath in fileNames)
            {
                readingBlock.Post(filePath);
            }

            readingBlock.Complete();

            await writingBlock.Completion;
        }
       
    }
}