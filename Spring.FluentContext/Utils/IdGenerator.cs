using System;

namespace Spring.FluentContext.Utils
{
	/// <summary>
	/// Object definition id generator.
	/// </summary>
	/// <typeparam name="T">Type for which id would be generated.</typeparam>
	public static class IdGenerator<T>
	{
		/// <summary>
		/// Generates default id for <c>T</c> type.
		/// </summary>
		/// <returns>Generated id.</returns>
		public static string GetDefaultId()
		{
			return IdGenerator.GetDefaultId(typeof(T));
		}

		/// <summary>
		/// Generates unique id for <c>T</c> type.
		/// </summary>
		/// <returns>Generated id.</returns>
		public static string GetUniqueId()
		{
			return IdGenerator.GetUniqueId(typeof(T));
		}
	}
	
	/// <summary>
	/// Object definition id generator.
	/// </summary>
	public static class IdGenerator
	{
		/// <summary>
		/// Generates default id for type.
		/// </summary>
		/// <returns>Generated id.</returns>
		public static string GetDefaultId(Type type)
		{
			return string.Format("_def_{0}_{1}", type.Name, (uint)type.GetHashCode());
		}

		/// <summary>
		/// Generates unique id for type.
		/// </summary>
		/// <returns>Generated id.</returns>
		public static string GetUniqueId(Type type)
		{
			return string.Format("_uni_{0}_{1}", type.Name, Guid.NewGuid());
		}
	}
}
