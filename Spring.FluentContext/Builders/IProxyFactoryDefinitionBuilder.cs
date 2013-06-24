using Spring.FluentContext.BuildingStages.ProxyFactories;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for proxy factory definition builder.
	/// </summary>
	public interface IProxyFactoryDefinitionBuilder
	{
		/// <summary>
		/// Registers proxy factory with specified <c>id</c> for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <param name="id">Proxy definition id.</param>
		/// <returns>Next build stage.</returns>
		IProxyTargetDefinitionBuildStage<T> RegisterNamedProxyFactory<T>(string id);

		/// <summary>
		/// Registers proxy factory with default id for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <returns>Next build stage.</returns>
		IProxyTargetDefinitionBuildStage<T> RegisterDefaultProxyFactory<T>();

		/// <summary>
		/// Registers proxy factory with unique id for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <returns>Next build stage.</returns>
		IProxyTargetDefinitionBuildStage<T> RegisterUniquelyNamedProxyFactory<T>();
	}
}