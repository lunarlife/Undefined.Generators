using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding.Extensions;

public static partial class SyntaxProviderBuilderExtensions
{
    public static ISyntaxProviderBuilder NoGenerics(this ISyntaxProviderBuilder syntaxProviderBuilder) =>
        syntaxProviderBuilder.PushInstruction(new GenericsTransformInstruction(ProviderAllowArgument.Disallow));

    public static ISyntaxProviderBuilder OnlyGenerics(this ISyntaxProviderBuilder syntaxProviderBuilder) =>
        syntaxProviderBuilder.PushInstruction(
            new GenericsTransformInstruction(ProviderAllowArgument.AllowOnly));
}

public class GenericsTransformInstruction : SyntaxProviderTransformInstruction
{
    private readonly ProviderAllowArgument _argument;

    public GenericsTransformInstruction(ProviderAllowArgument argument)
    {
        _argument = argument;
    }

    public override bool Transform(UndefinedTransformContext<CSharpSyntaxNode> context)
    {
        if (context.Model.GetSymbolInfo(context.Node).Symbol is not INamedTypeSymbol symbol)
            return false;

        switch (_argument)
        {
            case ProviderAllowArgument.Both:
                return true;
            case ProviderAllowArgument.Disallow:
                return symbol.TypeParameters.Length == 0;
            case ProviderAllowArgument.AllowOnly:
                return symbol.TypeParameters.Length != 0;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}