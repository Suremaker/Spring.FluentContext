using System;
using Spring.Context;

namespace Spring.FluentContext.Examples
{
	class Cat
	{
		private readonly string _name;

		public Cat(string name)
		{
			_name = name;
		}

		public override string ToString()
		{
			return _name;
		}
	}

	class PersonWithCat
	{
		public string Name { get; private set; }
		public Cat Cat { get; private set; }

		public PersonWithCat(string name, Cat cat)
		{
			Name = name;
			Cat = cat;
		}

		public void Introduce()
		{
			Console.WriteLine("Hello, I am {0}. I have a nice cat called {1}.", Name, Cat);
		}
	}

	internal class ConstructorInjection : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Cat>().BindConstructorArg<string>().ToValue("Kitty");

			ctx.RegisterDefault<PersonWithCat>()
				.BindConstructorArg<string>().ToValue("Josephine")
				.BindConstructorArg<Cat>().ToRegisteredDefault();

			return ctx;
		}
		protected override void RunExample(IApplicationContext ctx)
		{
			ctx.GetObject<PersonWithCat>().Introduce();
		}
	}
}