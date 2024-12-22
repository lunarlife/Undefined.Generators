using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Undefined.Generators.SyntaxProviderBuilding.Extensions;

public static partial class SyntaxProviderBuilderExtensions
{
    public static ISyntaxProviderBuilder WithAttribute(this ISyntaxProviderBuilder syntaxProviderBuilder,
        string attributeNamespace,
        string attributeName) =>
        syntaxProviderBuilder.PushInstruction(new WithAttributeTransformInstruction(attributeNamespace, attributeName));
}

public class WithAttributeTransformInstruction : SyntaxProviderTransformInstruction
{
    private readonly string _attributeNamespace;
    private readonly string _attributeName;

    public WithAttributeTransformInstruction(string attributeNamespace, string attributeName)
    {
        _attributeNamespace = attributeNamespace;
        _attributeName = attributeName;
    }

    public override bool Transform(UndefinedTransformContext<CSharpSyntaxNode> context)
    {
        if (context.Node is not MemberDeclarationSyntax syntax) return false;
        foreach (var attributeListSyntax in syntax.AttributeLists)
        foreach (var attributeSyntax in attributeListSyntax.Attributes)
        {
            if (context.Model.GetSymbolInfo(attributeSyntax).Symbol is not
                { } attributeSymbol)
                continue;
            var type = attributeSymbol.ContainingType;

            if (type.ContainingNamespace.ToDisplayString() == _attributeNamespace && type.Name == _attributeName)
                return true;
        }

        return false;
    }
}