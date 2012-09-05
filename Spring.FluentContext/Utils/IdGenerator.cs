using System;

namespace Spring.FluentContext.Utils
{
	public static class IdGenerator<T>
	{
		public static string GetDefaultId()
		{
			var type = typeof(T);
			return string.Format("_def_{0}_{1}", type.Name, (uint)type.GetHashCode());
		}

		public static string GetUniqueId()
		{
			return string.Format("_uni_{0}_{1}", typeof(T).Name, Guid.NewGuid());
		}
	}
}
