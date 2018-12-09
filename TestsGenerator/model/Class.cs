namespace TestsGenerator.model
{
    public class Class
    {
        public string ClassName { get; }
        public string ClassData { get; }

        public Class(string className, string classData)
        {
            ClassName = className;
            ClassData = classData;
        }
    }
}