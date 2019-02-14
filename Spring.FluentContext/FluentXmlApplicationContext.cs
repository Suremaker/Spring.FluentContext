using Spring.Context;
using Spring.Context.Support;
using Spring.Core.IO;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext
{
    public class FluentXmlApplicationContext : XmlApplicationContext, IFluentConfigurableApplicationContext
    {
        private readonly IContextConfigurator _configurator;

        /// <summary>
        /// Creates a new instance of the
        /// <see cref="FluentXmlApplicationContext"/> class,
        /// loading the definitions from the supplied XML resource locations,
        /// with the given <paramref name="parentContext"/>.
        /// </summary>
        /// <remarks>
        /// This constructor is meant to be used by derived classes. By passing <paramref name="refresh"/>=false, it is
        /// the responsibility of the deriving class to call <see cref="AbstractApplicationContext.Refresh()"/> to initialize the context instance.
        /// </remarks>
        /// <param name="refresh">if true, <see cref="AbstractApplicationContext.Refresh()"/> is called automatically.</param>
        /// <param name="name">The application context name.</param>
        /// <param name="caseSensitive">Flag specifying whether to make this context case sensitive or not.</param>
        /// <param name="parentContext">
        /// The parent context (may be <see langword="null"/>).
        /// </param>
        /// <param name="configurator">Allow to add other resources then from XML</param>
        /// <param name="configurationLocations">
        /// Any number of XML based object definition resource locations.
        /// </param>
        /// <param name="configurationResources">Resource list for context configuration</param>
        public FluentXmlApplicationContext(IContextConfigurator configurator = null,
            bool refresh = true,
            string name = "",
            bool caseSensitive = true,
            IApplicationContext parentContext = null,
            string[] configurationLocations = null,
            IResource[] configurationResources = null)
            : base(new XmlApplicationContextArgs(name, parentContext, configurationLocations,
                configurationResources, caseSensitive, false))
        {
            _configurator = configurator;
            if (refresh)
                Refresh();
        }

        public IConfigurableListableObjectFactory TemporaryInitObjectFactory { get; set; }
        protected override void LoadObjectDefinitions(DefaultListableObjectFactory objectFactory)
        {
            base.LoadObjectDefinitions(objectFactory);
            
            TemporaryInitObjectFactory = objectFactory;
            _configurator?.LoadObjectDefinitions(this);
        }
    }
}