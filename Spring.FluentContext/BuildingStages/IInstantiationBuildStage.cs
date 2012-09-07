using Spring.FluentContext.Builders;
using System;

namespace Spring.FluentContext.BuildingStages
{
	public interface IInstantiationBuildStage<TObject> : IAutoConfigurationBuildStage<TObject>
	{
		ICtorArgumentDefinitionBuilder<IInstantiationBuildStage<TObject>, TProperty> BindConstructorArg<TProperty>(int argIndex);

		ICtorArgumentDefinitionBuilder<IInstantiationBuildStage<TObject>, TProperty> BindConstructorArg<TProperty>();

		ICtorDefinitionBuilder<TObject, TArg> UseConstructor<TArg>(Func<TArg,TObject> constructorSelector);
		ICtorDefinitionBuilder<TObject, TArg1, TArg2> UseConstructor<TArg1,TArg2>(Func<TArg1,TArg2,TObject> constructorSelector);
		ICtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3> UseConstructor<TArg1,TArg2,TArg3>(Func<TArg1,TArg2,TArg3,TObject> constructorSelector);
		ICtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3, TArg4> UseConstructor<TArg1,TArg2,TArg3,TArg4>(Func<TArg1,TArg2,TArg3,TArg4,TObject> constructorSelector);
	}
}
