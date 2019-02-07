using Spring.Context;
using Spring.FluentContext.Builders;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory;

namespace Spring.FluentContext
{
    /// <summary>
    /// Interface for fluent application context.
    /// </summary>
    public interface IFluentApplicationContext : IApplicationContext,
        IAliasDefinitionBuilder,
        IObjectDefinitionBuilder,
        IProxyFactoryDefinitionBuilder,
        ISingletonDefinitionBuilder
    { }
}