using System;

namespace Spring.FluentContext.Examples.CollectionInjection.Objects
{
	class Polisher : IWorker
	{
		public void WorkOn(string item)
		{
			Console.WriteLine("Polishing {0}...", item);
		}
	}
}