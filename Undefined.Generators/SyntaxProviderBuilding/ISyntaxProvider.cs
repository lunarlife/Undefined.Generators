using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding;

public interface ISyntaxProvider : ISyntaxProvider<CSharpSyntaxNode>
{
}
public interface ISyntaxProvider<T> where T : CSharpSyntaxNode
{
    
    public IncrementalValuesProvider<T> Values { get; }
}
