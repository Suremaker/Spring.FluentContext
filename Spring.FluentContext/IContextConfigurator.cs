using Spring.Context;

namespace Spring.FluentContext
{
    /// <summary>
    /// Inteface which encapsulate context object registrations 
    /// </summary>
    public interface IContextConfigurator
    {
        /// <summary>
        /// Should include all context registrations.
        /// </summary>
        /// <param name="ctx">Context for the registration</param>
        void LoadObjectDefinitions(IConfigurableApplicationContext ctx);
    }
}