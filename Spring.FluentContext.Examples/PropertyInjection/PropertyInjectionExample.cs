using Spring.Context;
using Spring.FluentContext.Examples.PropertyInjection.Objects;

namespace Spring.FluentContext.Examples.PropertyInjection
{
	internal class PropertyInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Person>()
				.BindProperty(p => p.Name).ToValue("John") //binds Person.Name to constant value
				.BindProperty(p => p.Address).ToRegisteredDefault(); //binds Person.Address to other object registered in context

			ctx.RegisterDefault<Address>()
				.BindProperty(a => a.City).ToValue("London")
				.BindProperty(a => a.PostCode).ToValue("AB1 12CD")
				.BindProperty(a => a.Street).ToValue("Some Street");

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			ctx.GetObject<Person>().ShowDetails();
		}
	}
}
