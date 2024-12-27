using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding;

public interface ISyntaxProviderBuilder
{
    public ISyntaxProviderBuilder PushInstruction(ICSharpSyntaxProviderInstruction instruction);
    public ISyntaxProvider Build();
    public ISyntaxProvider<T> Build<T>() where T : CSharpSyntaxNode;
}