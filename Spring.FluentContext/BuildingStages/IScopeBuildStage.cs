namespace Spring.FluentContext.BuildingStages
{
	public interface IScopeBuildStage<TObject> : IInstantiationBuildStage<TObject>
	{
		IInstantiationBuildStage<TObject> AsPrototype();

		IInstantiationBuildStage<TObject> AsSingleton();
	}
}
