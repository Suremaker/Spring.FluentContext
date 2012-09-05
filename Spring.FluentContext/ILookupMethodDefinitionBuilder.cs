using Spring.FluentContext.Binders;

namespace Spring.FluentContext
{
	public interface ILookupMethodDefinitionBuilder<TObject, in TResult> 
		: IReferenceBinder<TObject, TResult>
	{
	}
}