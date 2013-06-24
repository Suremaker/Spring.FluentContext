using Spring.FluentContext.BuildingStages.Aliases;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for aliases registration.
	/// </summary>
	public interface IAliasDefinitionBuilder
	{
		/// <summary>
		/// Registers alias with default id for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <returns>Next build stage.</returns>
		IAliasLinkingBuildStage<T> RegisterDefaultAlias<T>();

		/// <summary>
		/// Registers alias with unique id for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <returns>Next build stage.</returns>
		IAliasLinkingBuildStage<T> RegisterUniquelyNamedAlias<T>();

		/// <summary>
		/// Registers alias with <c>id</c> for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <param name="id">Alias id.</param>
		/// <returns>Next build stage.</returns>
		IAliasLinkingBuildStage<T> RegisterNamedAlias<T>(string id);
	}
}