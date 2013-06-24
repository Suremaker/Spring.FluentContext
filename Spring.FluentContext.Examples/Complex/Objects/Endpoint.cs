using System;
using System.Collections.Generic;
using System.Threading;

namespace Spring.FluentContext.Examples.Complex.Objects
{
	class Endpoint : IEndpoint
	{
		private readonly Queue<string> _queue = new Queue<string>();
		private bool _disposed;
		private readonly AutoResetEvent _event = new AutoResetEvent(false);

		public void Send(string text)
		{
			lock (_queue)
				_queue.Enqueue(text);
			_event.Set();
		}

		public string Receive()
		{
			while (!_disposed)
			{
				string result;
				_event.WaitOne(500);
				if (TryReceive(out result))
					return result;
			}
			throw new ObjectDisposedException("_queue");
		}

		private bool TryReceive(out string result)
		{
			lock (_queue)
			{
				result = null;
				if (_queue.Count <= 0)
					return false;

				result = _queue.Dequeue();
				return true;
			}
		}

		public void Dispose()
		{
			_disposed = true;
		}
	}
}