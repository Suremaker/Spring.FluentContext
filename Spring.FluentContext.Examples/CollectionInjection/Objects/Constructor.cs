using System;

namespace Spring.FluentContext.Examples.CollectionInjection.Objects
{
	class Constructor : IWorker, IDisposable
	{
		public void WorkOn(string item)
		{
			Console.WriteLine("Constructing {0}...", item);
		}

		public void Dispose()
		{
			Console.WriteLine("Cleaning {0} - this is an example that inline objects are also properly destroyed by context", typeof(Constructor).Name);
		}
	}
}