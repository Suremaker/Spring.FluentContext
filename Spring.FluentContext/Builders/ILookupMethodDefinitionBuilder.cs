using Spring.FluentContext.Binders;
using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for lookup method definition builder.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	public interface ILookupMethodDefinitionBuilder<TObject, in TResult>
		: IReferenceBinder<IMethodConfigurationBuildStage<TObject>, TResult>
	{
	}
}