using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators;

public struct UndefinedTransformContext<T> where T : CSharpSyntaxNode
{
    public T Node { get; }
    public SemanticModel Model { get; }

    public UndefinedTransformContext(T node, SemanticModel model)
    {
        Node = node;
        Model = model;
    }
}