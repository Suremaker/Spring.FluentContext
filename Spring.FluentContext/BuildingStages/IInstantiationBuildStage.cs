using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages
{
	public interface IInstantiationBuildStage<TObject> : IAutoConfigurationBuildStage<TObject>
	{
		ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>(int argIndex);
		ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>();
	}
}
