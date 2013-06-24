using System;

namespace Spring.FluentContext.Examples.CollectionInjection.Objects
{
	class Painter : IWorker
	{
		public void WorkOn(string item)
		{
			Console.WriteLine("Painting {0}...", item);
		}
	}
}