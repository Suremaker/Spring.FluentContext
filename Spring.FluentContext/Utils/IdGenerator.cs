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
			var type = typeof(T);
			return string.Format("_def_{0}_{1}", type.Name, (uint)type.GetHashCode());
		}

		/// <summary>
		/// Generates unique id for <c>T</c> type.
		/// </summary>
		/// <returns>Generated id.</returns>
		public static string GetUniqueId()
		{
			return string.Format("_uni_{0}_{1}", typeof(T).Name, Guid.NewGuid());
		}
	}
}
