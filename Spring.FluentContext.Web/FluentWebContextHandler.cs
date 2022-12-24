using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Common.Logging;
using Spring.Context;
using Spring.Context.Support;
using Spring.Core.IO;

namespace Spring.FluentContext.Web
{
    public class FluentWebContextHandler : WebContextHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(WebContextHandler));
        private readonly string _configurator;

        public FluentWebContextHandler()
        {
            _configurator = ConfigurationManager.AppSettings["SpringConfigurator"];
        }

        /// <summary>
        /// Sets default context type to <see cref="FluentWebApplicationContext"/>
        /// </summary>
        protected override Type DefaultApplicationContextType
        {
            get { return typeof(FluentWebApplicationContext); }
        }

        /// <summary>
        /// Handles web specific details of context instantiation.
        /// </summary>
        protected override IApplicationContext InstantiateContext(IApplicationContext parent, object configContext,
            string contextName, Type contextType, bool caseSensitive, IList<string> resources)
        {
            string[] resourcesArray = resources.Union(
                string.IsNullOrWhiteSpace(_configurator)
                    ? new string[0]
                    : new[]
                    {
                        FluentWebApplicationContext.SpringConfiguratorKey +
                        ConfigurableResourceLoader.ProtocolSeparator + _configurator
                    }).ToArray();

            return base.InstantiateContext(parent, configContext, contextName, contextType, caseSensitive,
                resourcesArray);
        }       
    }
}