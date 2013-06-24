using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Spring.FluentContext.Examples.Complex.Objects
{
	public abstract class Consumer
	{
		private readonly IEndpoint _endpoint;
		private readonly List<Thread> _tasks = new List<Thread>();

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
					ProcessMessage();
					CleanupFinishedTasks();
				}
			}
			catch (ObjectDisposedException)
			{
				Console.WriteLine("Consumer stopped.");
			}
			WaitForTasks();
		}

		private void WaitForTasks()
		{
			foreach (var task in _tasks)
				task.Join();
		}

		private void CleanupFinishedTasks()
		{
			var finishedTasks = _tasks.Where(t => !t.IsAlive).ToArray();
			foreach (var finishedTask in finishedTasks)
			{
				_tasks.Remove(finishedTask);
				finishedTask.Join();
			}
		}

		private void ProcessMessage()
		{
			var message = _endpoint.Receive();

			var command = GetCommand();
			command.Message = message;
			var task = new Thread(command.Execute);
			task.Start();
			_tasks.Add(task);
		}

		protected abstract ICommand GetCommand();
	}
}