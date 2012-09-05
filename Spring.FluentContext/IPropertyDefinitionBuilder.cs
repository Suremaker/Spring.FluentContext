using Spring.FluentContext.Binders;

namespace Spring.FluentContext
{
	public interface IPropertyDefinitionBuilder<TObject, in TProperty>
		: IReferenceBinder<TObject, TProperty>,
		IValueBinder<TObject, TProperty>,
		IInlineDefinitionBinder<TObject, TProperty>
	{
	}
}