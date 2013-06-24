namespace Spring.FluentContext.BuildingStages.ProxyFactories
{
	/// <summary>
	/// Interface for proxy target definition stage.
	/// </summary>
	/// <typeparam name="TObject">Type of objects created by factory.</typeparam>
	public interface IProxyTargetDefinitionBuildStage<TObject>
	{
		/// <summary>
		/// Specifies object with <c>objectId</c> id to be proxy target.
		/// </summary>
		/// <param name="objectId">Object definition id.</param>
		/// <returns>Next build stage.</returns>
		IProxyInstantiationDefinitionBuildStage<TObject> Targeting(string objectId);

		/// <summary>
		/// Specifies object with default id for <c>TReferencedType</c> type to be proxy target.
		/// </summary>
		/// <typeparam name="TReferencedType">Type of targeted object.</typeparam>
		/// <returns>Next build stage.</returns>
		IProxyInstantiationDefinitionBuildStage<TObject> TargetingDefault<TReferencedType>() where TReferencedType : TObject;

		/// <summary>
		/// Specifies object referenced with <c>reference</c> to be proxy target.
		/// </summary>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Next build stage.</returns>
		IProxyInstantiationDefinitionBuildStage<TObject> Targeting(IObjectRef<TObject> reference);
	}
}