using System;

namespace Spring.FluentContext
{
	public interface ICtorDefinitionBuilder<TObject, in TArgument>
	{
		IObjectDefinitionBuilder<TObject> ToValue(TArgument value);
		IObjectDefinitionBuilder<TObject> ToReference(string objectId);
		IObjectDefinitionBuilder<TObject> ToReference<TRef>(ObjectRef<TRef> reference) where TRef : TArgument;
		IObjectDefinitionBuilder<TObject> ToDefaultReference();
		IObjectDefinitionBuilder<TObject> ToDefaultReferenceOfType<TReferencedType>() where TReferencedType : TArgument;
		IObjectDefinitionBuilder<TObject> ToInlineDefinition<TInnerObject>(Action<IObjectDefinitionBuilder<TInnerObject>> innerObjectBuildAction) where TInnerObject : TArgument;
	}
}