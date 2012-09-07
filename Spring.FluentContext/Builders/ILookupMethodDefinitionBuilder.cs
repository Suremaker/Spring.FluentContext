using Spring.FluentContext.Binders;
using Spring.FluentContext.BuildingStages;

namespace Spring.FluentContext.Builders
{
	public interface ILookupMethodDefinitionBuilder<TObject, in TResult>
		: IReferenceBinder<IConfigurationBuildStage<TObject>, TResult>
	{
	}
}