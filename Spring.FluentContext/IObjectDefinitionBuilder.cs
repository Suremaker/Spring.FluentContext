using Spring.FluentContext.BuildingStages;

namespace Spring.FluentContext
{
	public interface IObjectDefinitionBuilder<TObject> : IScopeBuildStage<TObject>
	{
	}
}