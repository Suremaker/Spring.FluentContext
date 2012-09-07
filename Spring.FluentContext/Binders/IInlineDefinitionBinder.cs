using System;
using Spring.FluentContext.BuildingStages;

namespace Spring.FluentContext.Binders
{
	public interface IInlineDefinitionBinder<out TBuilder, in TTargetType>
	{
		TBuilder ToInlineDefinition<TInnerObject>(Action<IInstantiationBuildStage<TInnerObject>> innerObjectBuildAction) where TInnerObject : TTargetType;
		TBuilder ToInlineDefinition<TInnerObject>() where TInnerObject : TTargetType;
	}
}