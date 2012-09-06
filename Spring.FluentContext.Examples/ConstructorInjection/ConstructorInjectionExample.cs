using Spring.Context;

namespace Spring.FluentContext.Examples.ConstructorInjection
{
	internal class ConstructorInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Cat>()
				.BindConstructorArg<string>().ToValue("Kitty");

			ctx.RegisterDefault<PersonWithCat>()
				.BindConstructorArg<string>().ToValue("Josephine") //binds string type argument to constant value
				.BindConstructorArg<Cat>().ToRegisteredDefault(); //binds Cat type argument to registered Cat instance

			return ctx;
		}
		protected override void RunExample(IApplicationContext ctx)
		{
			ctx.GetObject<PersonWithCat>().Introduce();
		}
	}
}