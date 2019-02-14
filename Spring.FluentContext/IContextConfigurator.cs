using Spring.Context;

namespace Spring.FluentContext
{
    public interface IContextConfigurator
    {
        void LoadObjectDefinitions(IConfigurableApplicationContext ctx);
    }
}