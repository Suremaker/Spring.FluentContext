namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for factory method definition builder.
	/// </summary>
	/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
	/// <typeparam name="TBuilder">Type of builder returned after binding.</typeparam>
	public interface IGenericFactoryMethodDefinitionBuilder<TBuilder, TFactoryObject>
	{
		/// <summary>
		/// Specifies object with default id for <c>TObject</c> type to be factory object on which factory method would be executed.
		/// </summary>
		/// <returns>Next build stage.</returns>
		TBuilder OfRegisteredDefault();

		/// <summary>
		/// Specifies object with <c>objectId</c> id to be factory object on which factory method would be executed.
		/// </summary>
		/// <param name="objectId">Object definition id.</param>
		/// <returns>Next build stage.</returns>
		TBuilder OfRegistered(string objectId);

		/// <summary>
		/// Specifies object referenced with <c>reference</c> id to be factory object on which factory method would be executed.
		/// </summary>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Next build stage.</returns>
		TBuilder OfRegistered(IObjectRef<TFactoryObject> reference);
	}
}