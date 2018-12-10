using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsGenerator.model;

namespace TestsGenerator
{
    public class SyntaxProcessor
    {
        SyntaxTree tree;
        SyntaxNode root;
        List<ClassDeclarationSyntax> classes;

        public List<ClassInfo> Process(string sourceCode)
        {
            tree = CSharpSyntaxTree.ParseText(sourceCode);
            root = tree.GetRoot();
            List<ClassInfo> syntaxRes = new List<ClassInfo>(GetClasses());
            return syntaxRes;
        }

        private List<ClassInfo> GetClasses()
        {
            classes = new List<ClassDeclarationSyntax>(root.DescendantNodes().OfType<ClassDeclarationSyntax>());
            List<ClassInfo> result = new List<ClassInfo>();
            foreach (ClassDeclarationSyntax cls in classes)
            {
                string className = cls.Identifier.ToString();
                NamespaceDeclarationSyntax clsParent = (NamespaceDeclarationSyntax)cls.Parent;
                string nmsp = clsParent.Name.ToString();
                List<string> methods = GetMethods(cls);
                result.Add(new ClassInfo(className, nmsp, methods));
            }
            return result;
        }

        private List<string> GetMethods(ClassDeclarationSyntax cls)
        {
            return new List<string>(
                cls.DescendantNodes().OfType<MethodDeclarationSyntax>().
                    Where(method => method.Modifiers.
                        Any(modifer => modifer.ToString() == "public"))
                    .Select(element => element.Identifier.ToString()));
        }
    }
}