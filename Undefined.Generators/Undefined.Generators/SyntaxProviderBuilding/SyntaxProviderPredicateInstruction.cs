using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding;

public abstract class SyntaxProviderPredicateInstruction : ICSharpSyntaxProviderInstruction
{
    public abstract bool Predicate(UndefinedPredicateContext<CSharpSyntaxNode> context);
}

public abstract class SyntaxProviderPredicateInstruction<T> : SyntaxProviderPredicateInstruction
    where T : CSharpSyntaxNode
{
    public override bool Predicate(UndefinedPredicateContext<CSharpSyntaxNode> context) => context.Node is T node &&
        Predicate(new UndefinedPredicateContext<CSharpSyntaxNode>(node));

    public abstract bool Predicate(UndefinedPredicateContext<T> context);
}