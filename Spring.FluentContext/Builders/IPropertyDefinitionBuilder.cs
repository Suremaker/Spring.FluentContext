using Spring.FluentContext.Binders;
using Spring.FluentContext.BuildingStages;

namespace Spring.FluentContext.Builders
{
	public interface IPropertyDefinitionBuilder<TObject, in TProperty>
		: IReferenceBinder<IConfigurationBuildStage<TObject>, TProperty>,
		IValueBinder<IConfigurationBuildStage<TObject>, TProperty>,
		IInlineDefinitionBinder<IConfigurationBuildStage<TObject>, TProperty>
	{
	}
}