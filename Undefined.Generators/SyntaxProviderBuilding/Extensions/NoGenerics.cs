using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Undefined.Generators.SyntaxProviderBuilding.Extensions;

public static partial class SyntaxProviderBuilderExtensions
{
    public static ISyntaxProviderBuilder NoGenerics(this ISyntaxProviderBuilder syntaxProviderBuilder) =>
        syntaxProviderBuilder.PushInstruction(new NoGenericsTransformInstruction());
}

public class NoGenericsTransformInstruction : SyntaxProviderTransformInstruction
{
    public override bool Transform(UndefinedTransformContext<CSharpSyntaxNode> context)
    {
        if (context.Node is not TypeDeclarationSyntax syntax) return false;
        return syntax.TypeParameterList is not {} list || list.Parameters.Count ==0;
    }
}