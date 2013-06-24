namespace Spring.FluentContext.BuildingStages
{
	/// <summary>
	/// Interface for referencing stage.
	/// </summary>
	/// <typeparam name="TObject">Type of constructed object.</typeparam>
	public interface IReferencingStage<TObject>
	{
		/// <summary>
		/// Returns object definition reference.
		/// </summary>
		/// <returns>Object definition reference.</returns>
		IObjectRef<TObject> GetReference();
	}
}
