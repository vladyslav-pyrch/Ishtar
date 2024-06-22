namespace Ishtar.DependencyInjection;

public class RecursiveDependencyException(Type dependency1, Type dependency2)
    : Exception($"{dependency1.FullName} and {dependency2.FullName} are in a recursive dependency.");