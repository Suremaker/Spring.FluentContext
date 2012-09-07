using Spring.Objects.Factory.Support;

namespace Spring.FluentContext.BuildingStages
{
	public interface IValidationBuildStage<TObject> : IReferencingStage<TObject>
	{
		IReferencingStage<TObject> CheckDependencies();
		IReferencingStage<TObject> CheckDependencies(DependencyCheckingMode mode);
	}
}
