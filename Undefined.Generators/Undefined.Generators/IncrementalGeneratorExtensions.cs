using Microsoft.CodeAnalysis;

namespace Undefined.Generators;

public static class IncrementalGeneratorExtensions
{
    public static UndefinedGeneratorInitializationContext GetUndefined(
        this IncrementalGeneratorInitializationContext context) =>
        new(context);
}