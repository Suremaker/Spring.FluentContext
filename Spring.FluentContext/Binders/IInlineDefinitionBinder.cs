using System;

namespace Spring.FluentContext.Binders
{
	public interface IInlineDefinitionBinder<TObject, in TTargetType>
	{
		IObjectDefinitionBuilder<TObject> ToInlineDefinition<TInnerObject>(Action<IObjectDefinitionBuilder<TInnerObject>> innerObjectBuildAction) where TInnerObject : TTargetType;
	}
}