using System;

namespace Spring.FluentContext
{
	public interface ICtorDefinitionBuilder<TObject, in TArgument>
	{
		IObjectDefinitionBuilder<TObject> ToValue(TArgument value);
		IObjectDefinitionBuilder<TObject> ToReference(string objectId);
		IObjectDefinitionBuilder<TObject> ToDefaultReference();
		IObjectDefinitionBuilder<TObject> ToInlineDefinition<TInnerObject>(Action<IObjectDefinitionBuilder<TInnerObject>> innerObjectBuildAction) where TInnerObject : TArgument;
	}
}