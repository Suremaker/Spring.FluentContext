namespace Spring.FluentContext.Utils
{
	internal static class IdGenerator<T>
	{
		public static string GetDefaultId()
		{
			var type = typeof(T);
			return string.Format("default_{0}_{1}", type.Name, type.GetHashCode());
		}		
	}
}
