using Spring.Context;

namespace Spring.FluentContext.Examples.PropertyInjection
{
	internal class PropertyInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Person>()
				.BindProperty(p => p.Name).ToValue("John")
				.BindProperty(p => p.Address).ToRegisteredDefault();

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
