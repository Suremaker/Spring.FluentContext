using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg">Type of first constructor argument.</typeparam>
	public interface ICtorDefinitionBuildStage<TObject, TArg>
	{
		/// <summary>
		/// Binds constructor argument.
		/// </summary>
		/// <returns>Constructor argument definition builder instance.</returns>
		IMethodArgumentDefinitionBuilder<IMethodConfigurationBuildStage<TObject>, TArg> BindConstructorArg();
	}

	/// <summary>
	/// Interface for constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first constructor argument.</typeparam>
	/// <typeparam name="TArg2">Type of second constructor argument.</typeparam>
	public interface ICtorDefinitionBuildStage<TObject, TArg1, TArg2>
	{
		/// <summary>
		/// Binds constructor arguments.
		/// </summary>
		/// <returns>Constructor argument definition builder instance.</returns>
		IMethodArgumentDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2>, TArg1> BindConstructorArg();
	}

	/// <summary>
	/// Interface for constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first constructor argument.</typeparam>
	/// <typeparam name="TArg2">Type of second constructor argument.</typeparam>
	/// <typeparam name="TArg3">Type of third constructor argument.</typeparam>
	public interface ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3>
	{
		/// <summary>
		/// Binds constructor arguments.
		/// </summary>
		/// <returns>Constructor argument definition builder instance.</returns>
		IMethodArgumentDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2, TArg3>, TArg1> BindConstructorArg();
	}

	/// <summary>
	/// Interface for constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first constructor argument.</typeparam>
	/// <typeparam name="TArg2">Type of second constructor argument.</typeparam>
	/// <typeparam name="TArg3">Type of third constructor argument.</typeparam>
	/// <typeparam name="TArg4">Type of fourth constructor argument.</typeparam>
	public interface ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3, TArg4>
	{
		/// <summary>
		/// Binds constructor arguments.
		/// </summary>
		/// <returns>Constructor argument definition builder instance.</returns>
		IMethodArgumentDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2, TArg3, TArg4>, TArg1> BindConstructorArg();
	}
}
