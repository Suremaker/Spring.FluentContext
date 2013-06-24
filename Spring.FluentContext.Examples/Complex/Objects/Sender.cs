using System;

namespace Spring.FluentContext.Examples.Complex.Objects
{
	class Sender
	{
		private readonly IEndpoint _endpoint;

		public Sender(IEndpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Run()
		{
			Console.WriteLine("Please write text. Empty line stops execution.");
			string text;
			while((text = Console.ReadLine()) != string.Empty)
				_endpoint.Send(text);
			_endpoint.Dispose();
		}
	}
}