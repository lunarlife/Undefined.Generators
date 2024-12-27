using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding;

public class SyntaxProviderBuilder : ISyntaxProviderBuilder
{
    private readonly IncrementalGeneratorInitializationContext _context;
    private readonly ICollection<SyntaxProviderPredicateInstruction> _predicates = [];
    private readonly ICollection<SyntaxProviderTransformInstruction> _transforms = [];

    public SyntaxProviderBuilder(IncrementalGeneratorInitializationContext context)
    {
        _context = context;
    }

    public ISyntaxProviderBuilder PushInstruction(ICSharpSyntaxProviderInstruction instruction)
    {
        if (instruction is SyntaxProviderPredicateInstruction predicate) _predicates.Add(predicate);
        else if (instruction is SyntaxProviderTransformInstruction transform) _transforms.Add(transform);
        return this;
    }

    public ISyntaxProvider Build()
    {
        return new SyntaxProvider(_predicates, _transforms, _context);
    }

    public ISyntaxProvider<T> Build<T>() where T : CSharpSyntaxNode =>
        new SyntaxProvider<T>(_predicates, _transforms, _context);
}