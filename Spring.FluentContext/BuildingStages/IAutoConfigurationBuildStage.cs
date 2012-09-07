using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.BuildingStages
{
	public interface IAutoConfigurationBuildStage<TObject> : IConfigurationBuildStage<TObject>
	{
		IConfigurationBuildStage<TObject> Autowire();
		IConfigurationBuildStage<TObject> Autowire(AutoWiringMode mode);
	}
}
