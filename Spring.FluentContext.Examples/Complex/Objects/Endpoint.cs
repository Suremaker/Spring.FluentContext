//
//  Author:
//    Wojciech Kotlarski
//
//  Copyright (c) 2012, Wojciech Kotlarski
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.	 
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
//  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//

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