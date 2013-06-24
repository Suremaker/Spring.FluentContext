using System;
using Spring.Context;

namespace Spring.FluentContext.Examples
{
	abstract class Example
	{
		public void Show()
		{
			Console.WriteLine("\n\n{0}:\n-------------------\n", GetType().Name);
			using (var ctx = CreateContext())
				RunExample(ctx);
			Console.WriteLine("\n\n...press key to continue...");

			Console.ReadKey(true);
		}

		protected abstract IApplicationContext CreateContext();

		protected abstract void RunExample(IApplicationContext ctx);
	}
}