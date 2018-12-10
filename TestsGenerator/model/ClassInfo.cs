using System.Collections.Generic;

namespace TestsGenerator.model
{
    public class ClassInfo
    {
        public string Name { get; }
        public string Namespace { get; }
        public List<string> Methods { get; }

        public ClassInfo(string name, string ns, List<string> methods)
        {
            Name = name;
            Namespace = ns;
            Methods = methods;
        }
    }
}