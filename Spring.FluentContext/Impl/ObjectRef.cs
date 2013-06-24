using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	internal class ObjectRef<T> : IObjectRef<T>
	{
		public ObjectRef(string id)
		{
			Id = id;
		}

		public string Id { get; private set; }

		private bool Equals(ObjectRef<T> other)
		{
			return string.Equals(Id, other.Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != GetType())
				return false;
			return Equals((ObjectRef<T>)obj);
		}

		public override int GetHashCode()
		{
			return (Id != null ? Id.GetHashCode() : 0);
		}

		public object DefinitionObject { get { return new RuntimeObjectReference(Id); } }
	}
}