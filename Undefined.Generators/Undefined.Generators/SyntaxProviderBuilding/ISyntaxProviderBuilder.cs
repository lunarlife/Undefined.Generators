using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding;

public interface ISyntaxProviderBuilder
{
    public ISyntaxProviderBuilder PushInstruction(SyntaxProviderTransformInstruction transformInstruction);
    public ISyntaxProviderBuilder PushInstruction<T>(SyntaxProviderTransformInstruction<T> transformInstruction) where T : CSharpSyntaxNode;
    public ISyntaxProvider Build();
    public ISyntaxProvider<T> Build<T>() where T : CSharpSyntaxNode;
}
