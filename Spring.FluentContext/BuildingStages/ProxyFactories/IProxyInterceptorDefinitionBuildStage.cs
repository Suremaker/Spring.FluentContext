using AopAlliance.Aop;

namespace Spring.FluentContext.BuildingStages.ProxyFactories
{
	/// <summary>
	/// Interface for proxy interceptor definition stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IProxyInterceptorDefinitionBuildStage<TObject>: IReferencingStage<TObject>
	{
		/// <summary>
		/// Specifies that each proxy call would be intercepted by interceptor with <c>objectId</c> id.
		/// </summary>
		/// <param name="objectId">Interceptor object definition id.</param>
		/// <returns>Same build stage.</returns>
		IProxyInterceptorDefinitionBuildStage<TObject> InterceptedBy(string objectId);

		/// <summary>
		/// Specifies that each proxy call would be intercepted by interceptor referenced with <c>reference</c>.
		/// </summary>
		/// <typeparam name="TInterceptorType">Type of interceptor.</typeparam>
		/// <param name="reference">Interceptor object reference.</param>
		/// <returns>Same build stage.</returns>
        IProxyInterceptorDefinitionBuildStage<TObject> InterceptedBy<TInterceptorType>(IObjectRef<TInterceptorType> reference) where TInterceptorType : IAdvice;

		/// <summary>
		/// Specifies that each proxy call would be intercepted by interceptor with default id for <c>TInterceptorType</c> type.
		/// </summary>
		/// <typeparam name="TInterceptorType">Type of interceptor.</typeparam>
		/// <returns>Same stage.</returns>
        IProxyInterceptorDefinitionBuildStage<TObject> InterceptedByDefault<TInterceptorType>() where TInterceptorType : IAdvice;
	}
}