using System;
using System.Threading;

namespace Spring.FluentContext.Examples.AdvancedPropertySetterInjection.Objects
{
	class BobEngineer : IFactoryEngineer
	{
		public void Construct(string item)
		{
			Console.WriteLine("Hi, I am Bob. I am going to build nice {0} for you...", item);
			Thread.Sleep(250);
		}
	}
}