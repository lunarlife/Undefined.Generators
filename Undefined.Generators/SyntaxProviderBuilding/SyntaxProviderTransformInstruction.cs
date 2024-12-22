using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding;

public abstract class SyntaxProviderTransformInstruction : ICSharpSyntaxProviderInstruction
{
    public abstract bool Transform(UndefinedTransformContext<CSharpSyntaxNode> context);
}

public abstract class SyntaxProviderTransformInstruction<T> : SyntaxProviderTransformInstruction where T :
    CSharpSyntaxNode
{
    public override bool Transform(UndefinedTransformContext<CSharpSyntaxNode> context) =>
        context.Node is T node && Transform(new UndefinedTransformContext<T>(node, context.Model));

    public abstract bool Transform(UndefinedTransformContext<T> context);
}

public struct Transform<T> where T : CSharpSyntaxNode
{
    public T Node { get; }

    public Transform(T node)
    {
        Node = node;
    }
}