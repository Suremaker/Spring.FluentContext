using System;
using System.Collections.Generic;

namespace Spring.FluentContext.Examples.CollectionInjection.Objects
{
	class Distributor : IWorker
	{
		public IDictionary<string, Shop> DistributionMap { get; set; }

		public void WorkOn(string item)
		{
			Console.WriteLine("Distributing {0} to {1}...\n", item, DistributionMap[item]);
		}
	}
}