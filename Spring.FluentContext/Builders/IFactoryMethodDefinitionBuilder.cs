using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for factory method definition builder.
	/// </summary>
	/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
	/// <typeparam name="TObject">Type of constructed object.</typeparam>
	public interface IFactoryMethodDefinitionBuilder<TFactoryObject, TObject>
		: IGenericFactoryMethodDefinitionBuilder<IAutoConfigurationBuildStage<TObject>, TFactoryObject>
	{
	}

	/// <summary>
	/// Interface for factory method definition builder.
	/// </summary>
	/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
	/// <typeparam name="TObject">Type of constructed object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	public interface IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1>
		: IGenericFactoryMethodDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg1>, TFactoryObject>
	{
	}

	/// <summary>
	/// Interface for factory method definition builder.
	/// </summary>
	/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
	/// <typeparam name="TObject">Type of constructed object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	/// <typeparam name="TArg2">Type of second factory method argument.</typeparam>
	public interface IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2>
		: IGenericFactoryMethodDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg1, TArg2>, TFactoryObject>
	{
	}

	/// <summary>
	/// Interface for factory method definition builder.
	/// </summary>
	/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
	/// <typeparam name="TObject">Type of constructed object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	/// <typeparam name="TArg2">Type of second factory method argument.</typeparam>
	/// <typeparam name="TArg3">Type of third factory method argument.</typeparam>
	public interface IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2, TArg3> :
		IGenericFactoryMethodDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg1, TArg2, TArg3>, TFactoryObject>
	{
	}
}
