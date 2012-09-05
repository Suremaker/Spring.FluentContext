using System;

namespace Spring.FluentContext
{
	public interface IPropertyDefinitionBuilder<TObject, in TProperty>
	{
		IObjectDefinitionBuilder<TObject> ToValue(TProperty value);
		IObjectDefinitionBuilder<TObject> ToReference(string objectId);
		IObjectDefinitionBuilder<TObject> ToReference<TRef>(ObjectRef<TRef> reference) where TRef : TProperty;
		IObjectDefinitionBuilder<TObject> ToDefaultReferenceOfType<TReferencedType>() where TReferencedType : TProperty;
		IObjectDefinitionBuilder<TObject> ToDefaultReference();
		IObjectDefinitionBuilder<TObject> ToInlineDefinition<TInnerObject>(Action<IObjectDefinitionBuilder<TInnerObject>> innerObjectBuildAction) where TInnerObject : TProperty;
	}
}