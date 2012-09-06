using System;

namespace Spring.FluentContext.Examples.Complex
{
	class DisplayCommand : ICommand
	{
		public void Execute()
		{
			Console.WriteLine("> " + Message.ToUpper());
		}

		public string Message { get; set; }		
	}
}