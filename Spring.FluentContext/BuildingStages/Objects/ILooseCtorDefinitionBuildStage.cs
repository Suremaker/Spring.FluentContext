using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for loose constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface ILooseCtorDefinitionBuildStage<TObject> : IMethodConfigurationBuildStage<TObject>
	{
		/// <summary>
		/// Binds constructor argument with <c>argIndex</c> index.
		/// </summary>
		/// <typeparam name="TArgument">Type of argument.</typeparam>
		/// <param name="argIndex">Index of argument.</param>
		/// <returns>Constructor argument definition builder.</returns>
		IMethodArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TArgument> BindConstructorArg<TArgument>(int argIndex);

		/// <summary>
		/// Binds constructor argument of <c>TProperty</c> type.
		/// </summary>
		/// <typeparam name="TArgument">Type of argument.</typeparam>
		/// <returns>Constructor argument definition builder.</returns>
		IMethodArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TArgument> BindConstructorArg<TArgument>();
	}
}
