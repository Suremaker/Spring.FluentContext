using System;
using Spring.Context;

namespace Spring.FluentContext.Examples
{
	internal class LookupMethodInjection : Example
	{
		protected override IApplicationContext CreateContext()
		{
			return new FluentApplicationContext();
		}
		protected override void RunExample(IApplicationContext ctx)
		{
		}
	}
}