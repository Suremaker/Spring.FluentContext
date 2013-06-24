namespace Spring.FluentContext.BuildingStages.ProxyFactories
{	
	/// <summary>
	/// Interface for proxy instantiation definition stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IProxyInstantiationDefinitionBuildStage<TObject> : IProxyInterceptorDefinitionBuildStage<TObject>
	{
		/// <summary>
		/// Specifies that factory would be returning prototypes (each request for proxy will result with new proxy instance).
		/// </summary>
		/// <returns>Next build stage.</returns>
		IProxyInterceptorDefinitionBuildStage<TObject> ReturningPrototypes();

		/// <summary>
		/// Specifies that factory would be returning singleton proxy (each request for proxy will result with the same proxy instance).
		/// </summary>
		/// <returns>Next build stage.</returns>
		IProxyInterceptorDefinitionBuildStage<TObject> ReturningSingleton();
	}	
}