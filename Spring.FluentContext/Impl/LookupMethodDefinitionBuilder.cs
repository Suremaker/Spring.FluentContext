using Spring.FluentContext.Builders;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext.Impl
{
	internal class LookupMethodDefinitionBuilder<TObject, TResult> : ILookupMethodDefinitionBuilder<TObject, TResult>
	{
		private readonly ObjectDefinitionBuilder<TObject> _builder;
		private readonly string _methodName;

		public LookupMethodDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder, string methodName)
		{
			_builder = builder;
			_methodName = methodName;
		}

		public IObjectDefinitionBuilder<TObject> ToRegistered(string objectId)
		{
			_builder.Definition.MethodOverrides.Add(new LookupMethodOverride(_methodName, objectId));
			return _builder;
		}

		public IObjectDefinitionBuilder<TObject> ToRegisteredDefault()
		{
			return ToRegistered(IdGenerator<TResult>.GetDefaultId());
		}

		public IObjectDefinitionBuilder<TObject> ToRegisteredDefaultOfType<TReferencedType>() where TReferencedType : TResult
		{
			return ToRegistered(IdGenerator<TReferencedType>.GetDefaultId());
		}

		public IObjectDefinitionBuilder<TObject> ToRegistered<TRef>(ObjectRef<TRef> reference) where TRef : TResult
		{
			return ToRegistered(reference.Id);
		}
	}
}