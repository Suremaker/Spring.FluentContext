namespace Spring.FluentContext
{
	public interface IProxyFactoryDefinitionBuilder<TObject>
	{
		IProxyFactoryDefinitionBuilder<TObject> Targeting(string objectId);
		IProxyFactoryDefinitionBuilder<TObject> TargetingDefaultReference<TReferencedType>() where TReferencedType : TObject;
		IProxyFactoryDefinitionBuilder<TObject> Targeting<TReferencedType>(ObjectRef<TReferencedType> reference) where TReferencedType : TObject;
		IProxyFactoryDefinitionBuilder<TObject> AddInterceptor(string objectId);
		IProxyFactoryDefinitionBuilder<TObject> AddInterceptor<TInterceptorType>(ObjectRef<TInterceptorType> reference);
		IProxyFactoryDefinitionBuilder<TObject> AddInterceptorDefaultReference<TInterceptorType>();
		ObjectRef<TObject> GetReference();
	}
}