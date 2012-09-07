using Spring.FluentContext.Binders;

namespace Spring.FluentContext.Builders
{
	public interface ILookupMethodDefinitionBuilder<TObject, in TResult> 
		: IReferenceBinder<TObject, TResult>
	{
	}
}