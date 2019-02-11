using Spring.Context;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext
{
	/// <summary>
	/// IApplication context extensions class.
	/// </summary>
	public static class ContextExtensions
	{

		/// <summary>
		/// Returns object of <c>T</c> type, registered with default name/id.
		/// </summary>
		/// <typeparam name="T">Type of object.</typeparam>
		/// <param name="ctx">Context.</param>
		/// <returns>Registered object.</returns>
		public static T GetDefaultObject<T>(this IApplicationContext ctx)
		{
			return ctx.GetObject<T>(IdGenerator<T>.GetDefaultId());
		}

		/// <summary>
		/// Returns object of <c>T</c> type, referenced by <c>reference</c>.
		/// </summary>
		/// <typeparam name="T">Type of object.</typeparam>
		/// <param name="ctx">Context.</param>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Registered object.</returns>
		public static T GetObject<T>(this IApplicationContext ctx, IObjectRef<T> reference)
		{
			return ctx.GetObject<T>(reference.Id);
		}
	}
}
