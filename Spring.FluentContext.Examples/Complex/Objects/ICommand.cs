namespace Spring.FluentContext.Examples.Complex.Objects
{
	public interface ICommand
	{
		string Message { get; set; }

		void Execute();
	}
}