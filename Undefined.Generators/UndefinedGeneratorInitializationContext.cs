using Microsoft.CodeAnalysis;
using Undefined.Generators.SyntaxProviderBuilding;

namespace Undefined.Generators;

public readonly struct UndefinedGeneratorInitializationContext
{
    private readonly IncrementalGeneratorInitializationContext _context;

    public UndefinedGeneratorInitializationContext(IncrementalGeneratorInitializationContext context)
    {
        _context = context;
    }

    public ISyntaxProviderBuilder SyntaxProvider() =>
        new SyntaxProviderBuilder(_context);
}