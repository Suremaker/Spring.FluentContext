namespace Spring.FluentContext
{
	public interface IObjectDefinitionBuilder<T>
	{
		IObjectDefinitionBuilder<T> AsPrototype();
		IObjectDefinitionBuilder<T> AsSingleton();
	}
}