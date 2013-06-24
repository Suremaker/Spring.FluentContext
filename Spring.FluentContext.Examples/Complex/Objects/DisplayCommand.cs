using System;

namespace Spring.FluentContext.Examples.Complex.Objects
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