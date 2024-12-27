using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Undefined.Generators.SyntaxProviderBuilding.Extensions;

public static partial class SyntaxProviderBuilderExtensions
{
    public static ISyntaxProviderBuilder NoStatic(this ISyntaxProviderBuilder syntaxProviderBuilder) =>
        syntaxProviderBuilder.PushInstruction(new GenericsTransformInstruction(ProviderAllowArgument.Disallow));

    public static ISyntaxProviderBuilder OnlyStatic(this ISyntaxProviderBuilder syntaxProviderBuilder) =>
        syntaxProviderBuilder.PushInstruction(
            new GenericsTransformInstruction(ProviderAllowArgument.AllowOnly));
}

public class StaticTransformInstruction : SyntaxProviderTransformInstruction
{
    private readonly ProviderAllowArgument _argument;

    public StaticTransformInstruction(ProviderAllowArgument argument)
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
                return !symbol.IsStatic;
            case ProviderAllowArgument.AllowOnly:
                return symbol.IsStatic;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}