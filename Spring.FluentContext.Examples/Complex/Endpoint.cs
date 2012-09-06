using System;
using System.Collections.Concurrent;

namespace Spring.FluentContext.Examples.Complex
{
	class Endpoint : IEndpoint
	{
		private readonly BlockingCollection<string> _queue = new BlockingCollection<string>();
		private bool _disposed;

		public void Send(string text)
		{
			_queue.Add(text);
		}

		public string Receive()
		{
			while (!_disposed)
			{
				string result;
				if (_queue.TryTake(out result, 500))
					return result;
			}
			throw new ObjectDisposedException("_queue");
		}

		public void Dispose()
		{
			_disposed = true;
		}
	}
}