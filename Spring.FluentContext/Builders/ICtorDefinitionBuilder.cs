using Spring.FluentContext.Binders;
using Spring.FluentContext.BuildingStages;

namespace Spring.FluentContext.Builders
{
	public interface ICtorDefinitionBuilder<TObject, in TArgument>
		: IReferenceBinder<IInstantiationBuildStage<TObject>, TArgument>,
		IInlineDefinitionBinder<IInstantiationBuildStage<TObject>, TArgument>,
		IValueBinder<IInstantiationBuildStage<TObject>, TArgument>
	{
	}
}