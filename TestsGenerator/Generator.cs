using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsGenerator.model;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace TestsGenerator
{
    public class Generator
    {
        public List<Class> GetTemplate(string sourceCode)
        {
            SyntaxProcessor syntaxProcessor = new SyntaxProcessor();
            List<ClassInfo> classInfos = syntaxProcessor.Process(sourceCode);
            List<Class> result = new List<Class>();
            foreach (ClassInfo classInfo in classInfos)
            {
                NamespaceDeclarationSyntax namespaceDeclaration = NamespaceDeclaration(
                    QualifiedName(
                        IdentifierName(classInfo.Namespace),
                        IdentifierName("Tests")));
                CompilationUnitSyntax testClass = CompilationUnit()
                    .WithUsings(GetTemplateUsings(classInfo))
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(namespaceDeclaration
                        .WithMembers(SingletonList<MemberDeclarationSyntax>(ClassDeclaration(classInfo.Name + "Tests")
                            .WithAttributeLists(
                                SingletonList(
                                    AttributeList(
                                        SingletonSeparatedList(
                                            Attribute(
                                                IdentifierName("TestFixture"))))))
                            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                            .WithMembers(GetTestMethods(classInfo.Methods))))));
                string fileName = classInfo.Name + "Tests.cs";
                string fileData = testClass.NormalizeWhitespace().ToFullString();
                result.Add(new Class(fileName, fileData));
            }
            return result;
        }

        private SyntaxList<MemberDeclarationSyntax> GetTestMethods(List<string> methods)
        {
            List<MemberDeclarationSyntax> result = new List<MemberDeclarationSyntax>();
            foreach (string method in methods)
            {
                result.Add(GenerateTestMethod(method));
            }
            return new SyntaxList<MemberDeclarationSyntax>(result);
        }

        private MethodDeclarationSyntax GenerateTestMethod(string methodName)
        {
            string attributeForTemplate = "Test";
            string methodBody = "Assert.Fail(\"autogenerated\");";
            return MethodDeclaration(
                PredefinedType(
                    Token(SyntaxKind.VoidKeyword)),
                Identifier(methodName + "Test"))
                .WithAttributeLists(
                    SingletonList(
                        AttributeList(
                            SingletonSeparatedList(
                                Attribute(
                                    IdentifierName(attributeForTemplate))))))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block(ParseStatement(methodBody)));
        }

        private SyntaxList<UsingDirectiveSyntax> GetTemplateUsings(ClassInfo classInfo)
        {
            List<UsingDirectiveSyntax> usingDirectives = new List<UsingDirectiveSyntax>
            {
                UsingDirective(IdentifierName("System")),
                UsingDirective(QualifiedName(
                    QualifiedName(IdentifierName("System"), IdentifierName("Collections")), IdentifierName("Generic"))
                ),
                UsingDirective(QualifiedName(IdentifierName("System"), IdentifierName("Linq"))),
                UsingDirective(QualifiedName(IdentifierName("System"), IdentifierName("Text"))),
                UsingDirective(QualifiedName(IdentifierName("NUnit"), IdentifierName("Framework"))),
                UsingDirective(IdentifierName(classInfo.Namespace))
            };

            return List(usingDirectives);
        }
    }
}