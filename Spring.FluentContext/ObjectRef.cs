namespace Spring.FluentContext
{
	public class ObjectRef<T>
	{
		public ObjectRef(string id)
		{
			Id = id;
		}

		public string Id { get; private set; }
	}
}