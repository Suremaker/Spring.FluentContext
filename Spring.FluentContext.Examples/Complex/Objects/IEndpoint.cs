using System;

namespace Spring.FluentContext.Examples.Complex.Objects
{
	public interface IEndpoint : IDisposable
	{
		void Send(string text);

		string Receive();
	}
}