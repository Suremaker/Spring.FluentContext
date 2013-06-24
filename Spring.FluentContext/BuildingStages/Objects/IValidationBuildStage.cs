using Spring.Objects.Factory.Support;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for validation build stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IValidationBuildStage<TObject> : IReferencingStage<TObject>
	{
		/// <summary>
		/// Specifies that object dependencies should be checked after object is created and configured.
		/// It uses DependencyCheckingMode.All mode.
		/// </summary>
		/// <returns>Next build stage.</returns>
		IReferencingStage<TObject> CheckDependencies();

		/// <summary>
		/// Specifies that object dependencies should be checked after object is created and configured.
		/// </summary>
		/// <param name="mode">Dependency checking mode.</param>
		/// <returns>Next build stage.</returns>
		IReferencingStage<TObject> CheckDependencies(DependencyCheckingMode mode);
	}
}
