using Spring.Context;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System;
using AopAlliance.Intercept;

namespace Spring.FluentContext.Examples
{
	public interface IEndpoint : IDisposable
	{
		void Send(string text);

		string Receive();
	}

	class Endpoint:IEndpoint
	{
		private BlockingCollection<string> _queue = new BlockingCollection<string>();
		private bool _disposed = false;

		public void Send(string text)
		{
			_queue.Add(text);
		}

		public string Receive()
		{
			while(!_disposed)
			{
				string result;
				if(_queue.TryTake(out result, 500))
					return result;
			}
			throw new ObjectDisposedException("_queue");
		}

		public void Dispose()
		{
			_disposed = true;
		}
	}

	public interface ICommand
	{
		string Message { get; set; }

		void Execute();
	}

	class DisplayCommand : ICommand
	{
		public void Execute()
		{
			Console.WriteLine("> " + Message.ToUpper());
		}

		public string Message { get; set; }		
	}

	public abstract class Consumer
	{
		private IEndpoint _endpoint;

		public Consumer(IEndpoint endpoint)
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
				while(true)
				{
					var message = _endpoint.Receive();

					var command = GetCommand();
					command.Message = message;
					Task.Factory.StartNew(command.Execute);
				}
			}
			catch(ObjectDisposedException)
			{
				Console.WriteLine("Consumer stopped.");
			}
		}

		protected abstract ICommand GetCommand();
	}

	class Sender
	{
		private IEndpoint _endpoint;

		public Sender(IEndpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Run()
		{
			Console.WriteLine("Please write text. Empty line stops sending.");
			string text;
			while((text = Console.ReadLine()) != string.Empty)
				_endpoint.Send(text);
			_endpoint.Dispose();
		}
	}

	class DelayingInterceptor : IMethodInterceptor
	{
		private int _time;

		public DelayingInterceptor(int time)
		{
			_time = time;
		}

		public object Invoke(IMethodInvocation invocation)
		{
			if(!invocation.Method.IsSpecialName)
				Thread.Sleep(_time);
			return invocation.Proceed();
		}
	}

	class RepeatingInterceptor:IMethodInterceptor
	{
		public object Invoke(IMethodInvocation invocation)
		{
			invocation.Proceed();
			return invocation.Proceed();
		}		
	}

	class BiggerExample: Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();
			ctx.RegisterDefault<Endpoint>();
			ctx.RegisterDefault<DisplayCommand>().AsPrototype();
			ctx.RegisterDefault<RepeatingInterceptor>();
			ctx.RegisterDefault<DelayingInterceptor>()
				.BindConstructorArg<int>().ToValue(5000);

			ctx.RegisterDefaultProxyFactory<ICommand>()
				.TargetingDefaultOfType<DisplayCommand>()
				.ReturningPrototypes()
				.AddInterceptorByDefaultReference<DelayingInterceptor>()
				.AddInterceptorByDefaultReference<RepeatingInterceptor>();

			ctx.RegisterDefault<Sender>().Autowire();
			ctx.RegisterDefault<Consumer>().Autowire()
				.BindLookupMethodNamed<ICommand>("GetCommand").ToRegisteredDefault();

			return ctx;
		}
		
		protected override void RunExample(IApplicationContext ctx)
		{
			var consumer = ctx.GetObject<Consumer>();
			var sender = ctx.GetObject<Sender>();
			consumer.Start();
			sender.Run();
		}
	}
}

