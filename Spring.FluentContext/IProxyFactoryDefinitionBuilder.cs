namespace Spring.FluentContext
{
	public interface IProxyFactoryDefinitionBuilder<TObject>
	{
		IProxyFactoryDefinitionBuilder<TObject> Targeting(string objectId);
		IProxyFactoryDefinitionBuilder<TObject> TargetingDefaultOfType<TReferencedType>() where TReferencedType : TObject;
		IProxyFactoryDefinitionBuilder<TObject> Targeting<TReferencedType>(ObjectRef<TReferencedType> reference) where TReferencedType : TObject;
		IProxyFactoryDefinitionBuilder<TObject> AddInterceptor(string objectId);
		IProxyFactoryDefinitionBuilder<TObject> AddInterceptor<TInterceptorType>(ObjectRef<TInterceptorType> reference);
		IProxyFactoryDefinitionBuilder<TObject> AddInterceptorByDefaultReference<TInterceptorType>();
		ObjectRef<TObject> GetReference();
	}
}