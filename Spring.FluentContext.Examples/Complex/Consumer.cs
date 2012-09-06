using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spring.FluentContext.Examples.Complex
{
	public abstract class Consumer
	{
		private readonly IEndpoint _endpoint;

		protected Consumer(IEndpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Start()
		{
			new Thread(RunConsumer).Start();
		}

		private void RunConsumer()
		{
			try
			{
				while (true)
				{
					var message = _endpoint.Receive();

					var command = GetCommand();
					command.Message = message;
					Task.Factory.StartNew(command.Execute);
				}
			}
			catch (ObjectDisposedException)
			{
				Console.WriteLine("Consumer stopped.");
			}
		}

		protected abstract ICommand GetCommand();
	}
}