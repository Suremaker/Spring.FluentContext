using Spring.FluentContext.Definitions;

namespace Spring.FluentContext.Binders
{
	/// <summary>
	/// Interface for reference binder.
	/// </summary>
	/// <typeparam name="TBuilder">Type of builder returned after binding is done.</typeparam>
	/// <typeparam name="TTargetType">Type of binding.</typeparam>
	public interface IReferenceBinder<out TBuilder, in TTargetType>
	{
		/// <summary>
		/// Specifies object with <c>objectId</c> id as binding target.
		/// </summary>
		/// <param name="objectId">Id of registered object.</param>
		/// <returns>Builder.</returns>
		TBuilder ToRegistered(string objectId);

		/// <summary>
		/// Specifies object referenced with <c>reference</c> as binding target.
		/// </summary>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Builder.</returns>
		TBuilder ToRegistered(IObjectRef<TTargetType> reference);

		/// <summary>
		/// Specifies object with default id for <c>TReferencedType</c> type as binding target.
		/// </summary>
		/// <typeparam name="TReferencedType">Type of bound object.</typeparam>
		/// <returns>Builder.</returns>
		TBuilder ToRegisteredDefaultOf<TReferencedType>() where TReferencedType : TTargetType;

		/// <summary>
		/// Specifies object with default id for <c>TTargetType</c> type as binding target.
		/// </summary>
		/// <returns>Builder.</returns>
		TBuilder ToRegisteredDefault();
	}
}