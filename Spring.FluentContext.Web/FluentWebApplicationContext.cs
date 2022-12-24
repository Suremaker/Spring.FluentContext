using System;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext.Web
{
    public class FluentWebApplicationContext : WebApplicationContext
    {
        public const string SpringConfiguratorKey = "SpringConfigurator";
        private IContextConfigurator _configurator;
        
        #region Constructors
        
        /// <summary>
        /// Create a new WebApplicationContext, loading the definitions
        /// from the given XML resource.
        /// </summary>
        /// <param name="configurationLocations">Names of configuration resources.</param>
        public FluentWebApplicationContext(params string[] configurationLocations)
            : base(new WebApplicationContextArgs(string.Empty, null, configurationLocations, null, false))
        {
        }

        /// <summary>
        /// Create a new WebApplicationContext, loading the definitions
        /// from the given XML resource.
        /// </summary>
        /// <param name="name">The application context name.</param>
        /// <param name="caseSensitive">Flag specifying whether to make this context case sensitive or not.</param>
        /// <param name="configurationLocations">Names of configuration resources.</param>
        public FluentWebApplicationContext(string name, bool caseSensitive, params string[] configurationLocations)
            : base(new WebApplicationContextArgs(name, null, configurationLocations, null, caseSensitive))
        {
        }

        /// <summary>
        /// Create a new WebApplicationContext with the given parent,
        /// loading the definitions from the given XML resources.
        /// </summary>
        /// <param name="name">The application context name.</param>
        /// <param name="caseSensitive">Flag specifying whether to make this context case sensitive or not.</param>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="configurationLocations">Names of configuration resources.</param>
        public FluentWebApplicationContext(string name, bool caseSensitive, IApplicationContext parentContext,
                                     params string[] configurationLocations)
            : base(new WebApplicationContextArgs(name, parentContext, configurationLocations, null, caseSensitive))
        { }
        #endregion
        
        protected override void OnPreRefresh()
        {
            const string prefix = SpringConfiguratorKey + ProtocolSeparator;
            
            for (int i = 0; i < ConfigurationLocations.Length; i++)
            {
                if (ConfigurationLocations[i].StartsWith(prefix))
                {
                    string configuratorName = ConfigurationLocations[i].Substring(prefix.Length);
                    ConfigurationLocations[i] = "assembly://Spring.FluentContext.Web/Spring.FluentContext.Web/emptyContext.xml";
                    _configurator = (IContextConfigurator) Activator.CreateInstance(Type.GetType(configuratorName));
                    break; // only one configurator supported
                }
            }
            base.OnPreRefresh();
        }

        /// <summary>
        /// This method registers Fluent objects and calls base post processing
        /// </summary>
        /// <param name="objectFactory"></param>
        protected override void PostProcessObjectFactory(IConfigurableListableObjectFactory objectFactory)
        {
            _configurator?.LoadObjectDefinitions(this);
            base.PostProcessObjectFactory(objectFactory);
        }
    }
}