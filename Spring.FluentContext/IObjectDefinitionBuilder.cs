using System;
using System.Linq.Expressions;

namespace Spring.FluentContext
{
	public interface IObjectDefinitionBuilder<TObject>
	{
		IObjectDefinitionBuilder<TObject> AsPrototype();
		IObjectDefinitionBuilder<TObject> AsSingleton();

		IPropertyDefinitionBuilder<TObject, TProperty> BindProperty<TProperty>(Expression<Func<TObject, TProperty>> propertySelector);
		IPropertyDefinitionBuilder<TObject, TProperty> BindPropertyByName<TProperty>(string propertyName);

		ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>(int argIndex);
		ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>();
		IObjectDefinitionBuilder<TObject> BindLookupMethod<TResult>(Expression<Func<TObject, TResult>> methodSelector, string objectId);
		IObjectDefinitionBuilder<TObject> BindLookupMethodByName(string methodName, string objectId);
	}
}