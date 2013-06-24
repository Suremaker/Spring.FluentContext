namespace Spring.FluentContext.BuildingStages.Aliases
{
	/// <summary>
	/// Interface for alias linking build stage
	/// </summary>
	/// <typeparam name="TObject">Type of alias.</typeparam>
	public interface IAliasLinkingBuildStage<TObject>
	{
		/// <summary>
		/// Specifies object with default id for <c>TDerived</c> type to be alias target.
		/// </summary>
		/// <typeparam name="TDerived">Type of targeted object.</typeparam>
		/// <returns>Next build stage.</returns>
		IReferencingStage<TObject> ToRegisteredDefault<TDerived>() where TDerived : TObject;

		/// <summary>
		/// Specifies object with specified <c>objectId</c> id for <c>TDerived</c> type to be alias target.
		/// </summary>
		/// <typeparam name="TDerived">Type of targeted object.</typeparam>
		/// <param name="objectId">Object definition id.</param>
		/// <returns>Next build stage.</returns>
		IReferencingStage<TObject> ToRegistered<TDerived>(string objectId) where TDerived : TObject;

		/// <summary>
		/// Specifies object referenced with <c>reference</c> to be alias target.
		/// </summary>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Next build stage.</returns>
		IReferencingStage<TObject> ToRegistered(IObjectRef<TObject> reference);
	}

}