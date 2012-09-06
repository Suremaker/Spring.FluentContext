using System;

namespace Spring.FluentContext.Examples.Complex
{
	public interface IEndpoint : IDisposable
	{
		void Send(string text);

		string Receive();
	}
}