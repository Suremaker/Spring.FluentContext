using Spring.Context;
using Spring.FluentContext.Examples.ConstructorInjection.Objects;

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
				.UseConstructor((string name, Cat cat) => new PersonWithCat(name, cat))
				.BindConstructorArg().ToValue("Josephine") //binds string type argument to constant value
				.BindConstructorArg().ToRegisteredDefault(); //binds Cat type argument to registered Cat instance

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			ctx.GetDefaultObject<PersonWithCat>().Introduce();
		}
	}
}