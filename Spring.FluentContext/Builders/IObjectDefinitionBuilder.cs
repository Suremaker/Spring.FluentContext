using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for object definition builder.
	/// </summary>
	public interface IObjectDefinitionBuilder
	{
		/// <summary>
		/// Registers object definition with specified <c>id</c> for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <param name="id">Object id.</param>
		/// <returns>Next build stage.</returns>
		IScopeBuildStage<T> RegisterNamed<T>(string id);

		/// <summary>
		/// Registers object definition with default id for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <returns>Next build stage.</returns>
		IScopeBuildStage<T> RegisterDefault<T>();

		/// <summary>
		/// Registers object definition with unique id for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <returns>Next build stage.</returns>
		IScopeBuildStage<T> RegisterUniquelyNamed<T>();
	}
}