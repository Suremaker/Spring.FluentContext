namespace Spring.FluentContext.Examples.Complex
{
	public interface ICommand
	{
		string Message { get; set; }

		void Execute();
	}
}