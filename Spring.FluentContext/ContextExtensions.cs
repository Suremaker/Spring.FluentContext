using Spring.Context;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext
{
	public static class ContextExtensions
	{
		public static T GetObject<T>(this IApplicationContext ctx, string name)
		{
			return (T)ctx.GetObject(name, typeof(T));
		}

		public static T GetObject<T>(this IApplicationContext ctx)
		{
			return ctx.GetObject<T>(IdGenerator<T>.GetDefaultId());
		}

		public static T GetObject<T>(this IApplicationContext ctx, ObjectRef<T> reference)
		{
			return ctx.GetObject<T>(reference.Id);
		}
	}
}
