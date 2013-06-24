using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for factory method argument build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	public interface IFactoryMethodArgBuildStage<TObject, TArg1>
	{
		/// <summary>
		/// Binds factory method argument.
		/// </summary>
		/// <returns>Argument definition builder.</returns>
		IMethodArgumentDefinitionBuilder<IAutoConfigurationBuildStage<TObject>, TArg1> BindMethodArg();
	}

	/// <summary>
	/// Interface for factory method argument build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	/// <typeparam name="TArg2">Type of second factory method argument.</typeparam>
	public interface IFactoryMethodArgBuildStage<TObject, TArg1, TArg2>
	{
		/// <summary>
		/// Binds factory method argument.
		/// </summary>
		/// <returns>Argument definition builder.</returns>
		IMethodArgumentDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg2>, TArg1> BindMethodArg();
	}

	/// <summary>
	/// Interface for factory method argument build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	/// <typeparam name="TArg2">Type of second factory method argument.</typeparam>
	/// <typeparam name="TArg3">Type of third factory method argument.</typeparam>
	public interface IFactoryMethodArgBuildStage<TObject, TArg1, TArg2, TArg3>
	{
		/// <summary>
		/// Binds factory method argument.
		/// </summary>
		/// <returns>Argument definition builder.</returns>
		IMethodArgumentDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg2, TArg3>, TArg1> BindMethodArg();
	}
}