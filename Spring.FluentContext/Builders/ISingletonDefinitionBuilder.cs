namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for singleton instances registration.
	/// </summary>
	public interface ISingletonDefinitionBuilder
	{
		/// <summary>
		/// Registers singleton <c>instance</c> with specified <c>id</c>.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>
		/// <param name="id">Id of registered object.</param>
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		IObjectRef<T> RegisterNamedSingleton<T>(string id, T instance);

		/// <summary>
		/// Registers singleton <c>instance</c> with default id.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>		
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		IObjectRef<T> RegisterDefaultSingleton<T>(T instance);

		/// <summary>
		/// Registers singleton <c>instance</c> with unique id.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>		
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		IObjectRef<T> RegisterUniquelyNamedSingleton<T>(T instance);
	}
}