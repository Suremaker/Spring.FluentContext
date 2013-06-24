using Spring.FluentContext.Binders;
using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for property definition builder.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TProperty">Type of property.</typeparam>
	public interface IPropertyDefinitionBuilder<TObject, in TProperty>
		: IReferenceBinder<IObjectConfigurationBuildStage<TObject>, TProperty>,
		IValueBinder<IObjectConfigurationBuildStage<TObject>, TProperty>,
		IInlineDefinitionBinder<IObjectConfigurationBuildStage<TObject>, TProperty>,
		IDefinitionBinder<IObjectConfigurationBuildStage<TObject>,TProperty> 
	{
	}
}