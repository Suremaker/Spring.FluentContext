using Spring.Context;

namespace Spring.FluentContext
{
	public static class ContextExtensions
	{
		public static T GetObject<T>(this IApplicationContext ctx, string name)
		{
			return (T) ctx.GetObject(name, typeof (T));
		}
	}
}
