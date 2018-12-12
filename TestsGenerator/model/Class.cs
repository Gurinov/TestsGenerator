namespace TestsGenerator.model
{
    public class Class
    {
        public string Name { get; }
        public string Data { get; }

        public Class(string name, string data)
        {
            Name = name;
            Data = data;
        }
    }
}