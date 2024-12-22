using System.Collections.Generic;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding;

public class SyntaxProvider : SyntaxProvider<CSharpSyntaxNode>, ISyntaxProvider
{
    public SyntaxProvider(ICollection<ICSharpSyntaxProviderInstruction> instructions,
        IncrementalGeneratorInitializationContext context) : base(instructions, context)
    {
    }
}

public class SyntaxProvider<T> : ISyntaxProvider<T> where T : CSharpSyntaxNode
{
    private readonly ICSharpSyntaxProviderInstruction[] _instructions;

    public IncrementalValuesProvider<T> Values { get; }

    public SyntaxProvider(ICollection<ICSharpSyntaxProviderInstruction> instructions,
        IncrementalGeneratorInitializationContext context)
    {
        _instructions = new ICSharpSyntaxProviderInstruction[instructions.Count];
        instructions.CopyTo(_instructions, 0);
        Values = context.SyntaxProvider.CreateSyntaxProvider(SyntaxProviderPredicate, SyntaxProviderTransform)
            .Where(t => t is not null)!;
    }


    private T? SyntaxProviderTransform(GeneratorSyntaxContext context, CancellationToken ct)
    {
        foreach (var instruction in _instructions)
        {
            if (instruction is not SyntaxProviderTransformInstruction transformInstruction) continue;
            if (!transformInstruction.Transform(
                    new UndefinedTransformContext<CSharpSyntaxNode>((CSharpSyntaxNode)context.Node,
                        context.SemanticModel))) return null;
        }

        return context.Node as T;
    }

    private bool SyntaxProviderPredicate(SyntaxNode node, CancellationToken ct)
    {
        if (node is not T syntaxNode) return false;
        foreach (var instruction in _instructions)
        {
            if (instruction is not SyntaxProviderPredicateInstruction transformInstruction) continue;
            if (!transformInstruction.Predicate(
                    new UndefinedPredicateContext<CSharpSyntaxNode>(syntaxNode)))
                return false;
        }

        return true;
    }
}