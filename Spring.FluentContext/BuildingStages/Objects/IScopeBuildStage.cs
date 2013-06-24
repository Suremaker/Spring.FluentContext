namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for scope build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	public interface IScopeBuildStage<TObject> : IIndirectDependencyBuildStage<TObject>
	{
		/// <summary>
		/// Specified that every time configured object is requested, a new instance of object is be returned.
		/// </summary>
		/// <returns>Next build stage.</returns>
		IIndirectDependencyBuildStage<TObject> AsPrototype();

		/// <summary>
		/// Specified that every time configured object is requested, always the same instance of object would be returned.
		/// </summary>
		/// <returns>Next build stage.</returns>
		IIndirectDependencyBuildStage<TObject> AsSingleton();	
	}
}
