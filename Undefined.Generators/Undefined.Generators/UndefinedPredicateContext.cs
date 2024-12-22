using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators;

public struct UndefinedPredicateContext<T> where T : CSharpSyntaxNode
{
    public T Node { get; }

    public UndefinedPredicateContext(T node)
    {
        Node = node;
    }
}