using System;

namespace Spring.FluentContext.Examples.ConstructorInjection.Objects
{
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
}