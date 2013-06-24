namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for indirect dependency build stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IIndirectDependencyBuildStage<TObject> : IInstantiationBuildStage<TObject>
	{
		/// <summary>
		/// Specifies that object with default id for <c>TOtherObject</c> type has to be configured before one that is currently being defined.
		/// </summary>
		/// <typeparam name="TOtherObject">Type of object.</typeparam>
		/// <returns>Same build stage.</returns>
		IIndirectDependencyBuildStage<TObject> DependingOnDefault<TOtherObject>();

		/// <summary>
		/// Specifies that object with specified <c>id</c> for <c>TOtherObject</c> type has to be configured before one that is currently being defined.
		/// </summary>
		/// <typeparam name="TOtherObject">Type of object.</typeparam>
		/// <param name="objectId">Object id.</param>
		/// <returns>Same build stage.</returns>
		IIndirectDependencyBuildStage<TObject> DependingOn<TOtherObject>(string objectId);

		/// <summary>
		/// Specifies that object referenced with <c>reference</c> has to be configured before one that is currently being defined.
		/// </summary>
		/// <typeparam name="TOtherObject">Type of object.</typeparam>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Same build stage.</returns>
		IIndirectDependencyBuildStage<TObject> DependingOn<TOtherObject>(IObjectRef<TOtherObject> reference);
	}
}
