using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding;

public class SyntaxProviderBuilder : ISyntaxProviderBuilder
{
    private readonly IncrementalGeneratorInitializationContext _context;
    private readonly ICollection<ICSharpSyntaxProviderInstruction> _instructions = [];

    public SyntaxProviderBuilder(IncrementalGeneratorInitializationContext context)
    {
        _context = context;
    }

    public ISyntaxProviderBuilder PushInstruction(SyntaxProviderTransformInstruction transformInstruction)
    {
        _instructions.Add(transformInstruction);
        return this;
    }

    public ISyntaxProviderBuilder PushInstruction<T>(SyntaxProviderTransformInstruction<T> transformInstruction)
        where T : CSharpSyntaxNode
    {
        _instructions.Add(transformInstruction);
        return this;
    }

    public ISyntaxProvider Build() => new SyntaxProvider(_instructions, _context);
    public ISyntaxProvider<T> Build<T>() where T : CSharpSyntaxNode => new SyntaxProvider<T>(_instructions, _context);
}