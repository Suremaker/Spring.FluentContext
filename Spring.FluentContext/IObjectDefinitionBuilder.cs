using System;
using System.Linq.Expressions;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext
{
	public interface IObjectDefinitionBuilder<TObject>
	{
		IObjectDefinitionBuilder<TObject> AsPrototype();
		IObjectDefinitionBuilder<TObject> AsSingleton();
		IObjectDefinitionBuilder<TObject> Autowire();
		IObjectDefinitionBuilder<TObject> Autowire(AutoWiringMode mode);
		IObjectDefinitionBuilder<TObject> CheckDependencies();
		IObjectDefinitionBuilder<TObject> CheckDependencies(DependencyCheckingMode mode);

		IPropertyDefinitionBuilder<TObject, TProperty> BindProperty<TProperty>(Expression<Func<TObject, TProperty>> propertySelector);
		IPropertyDefinitionBuilder<TObject, TProperty> BindPropertyNamed<TProperty>(string propertyName);

		ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>(int argIndex);
		ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>();
		ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethod<TResult>(Expression<Func<TObject, TResult>> methodSelector);
		ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethodNamed<TResult>(string methodName);
		ObjectRef<TObject> GetReference();
	}
}