using System;
using System.Linq.Expressions;
using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for instantiation build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	public interface IInstantiationBuildStage<TObject> : ILooseCtorDefinitionBuildStage<TObject>
	{
		/// <summary>
		/// Specifies that object should be instantiated by calling static method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IAutoConfigurationBuildStage<TObject> UseStaticFactoryMethod(Func<TObject> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling static method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">Factory method argument.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodArgBuildStage<TObject, TArg1> UseStaticFactoryMethod<TArg1>(Expression<Func<TArg1, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling static method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">Factory method argument 1.</typeparam>
		/// <typeparam name="TArg2">Factory method argument 2.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodArgBuildStage<TObject, TArg1, TArg2> UseStaticFactoryMethod<TArg1, TArg2>(Expression<Func<TArg1, TArg2, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling static method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">Factory method argument 1.</typeparam>
		/// <typeparam name="TArg2">Factory method argument 2.</typeparam>
		/// <typeparam name="TArg3">Factory method argument 3.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodArgBuildStage<TObject, TArg1, TArg2, TArg3> UseStaticFactoryMethod<TArg1, TArg2, TArg3>(Expression<Func<TArg1, TArg2, TArg3, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodDefinitionBuilder<TFactoryObject, TObject> UseFactoryMethod<TFactoryObject>(Expression<Func<TFactoryObject, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
		/// <typeparam name="TArg1">Factory method argument.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1> UseFactoryMethod<TFactoryObject, TArg1>(Expression<Func<TFactoryObject, TArg1, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
		/// <typeparam name="TArg1">Factory method argument 1.</typeparam>
		/// <typeparam name="TArg2">Factory method argument 2.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2> UseFactoryMethod<TFactoryObject, TArg1, TArg2>(Expression<Func<TFactoryObject, TArg1, TArg2, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
		/// <typeparam name="TArg1">Factory method argument 1.</typeparam>
		/// <typeparam name="TArg2">Factory method argument 2.</typeparam>
		/// <typeparam name="TArg3">Factory method argument 3.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2, TArg3> UseFactoryMethod<TFactoryObject, TArg1, TArg2, TArg3>(Expression<Func<TFactoryObject, TArg1, TArg2, TArg3, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling constructor specified by <c>constructorSelector</c>.
		/// </summary>
		/// <typeparam name="TArg">First constructor parameter type.</typeparam>
		/// <param name="constructorSelector">Lambda expression to select constructor to call.</param>
		/// <returns>Next build stage.</returns>
		ICtorDefinitionBuildStage<TObject, TArg> UseConstructor<TArg>(Func<TArg, TObject> constructorSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling constructor specified by <c>constructorSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">First constructor parameter type.</typeparam>
		/// <typeparam name="TArg2">Second constructor parameter type.</typeparam>
		/// <param name="constructorSelector">Lambda expression to select constructor to call.</param>
		/// <returns>Next build stage.</returns>
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2> UseConstructor<TArg1, TArg2>(Func<TArg1, TArg2, TObject> constructorSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling constructor specified by <c>constructorSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">First constructor parameter type.</typeparam>
		/// <typeparam name="TArg2">Second constructor parameter type.</typeparam>
		/// <typeparam name="TArg3">Third constructor parameter type.</typeparam>
		/// <param name="constructorSelector">Lambda expression to select constructor to call.</param>
		/// <returns>Next build stage.</returns>
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3> UseConstructor<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TObject> constructorSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling constructor specified by <c>constructorSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">First constructor parameter type.</typeparam>
		/// <typeparam name="TArg2">Second constructor parameter type.</typeparam>
		/// <typeparam name="TArg3">Third constructor parameter type.</typeparam>
		/// <typeparam name="TArg4">Fourth constructor parameter type.</typeparam>
		/// <param name="constructorSelector">Lambda expression to select constructor to call.</param>
		/// <returns>Next build stage.</returns>
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3, TArg4> UseConstructor<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TObject> constructorSelector);
	}
}
