using System;

namespace Spring.FluentContext.Examples.PropertyInjection
{
	class Person
	{
		public string Name { get; set; }
		public Address Address { get; set; }

		public void ShowDetails()
		{
			Console.WriteLine("Name: {0}\nAddress details: {1}", Name, Address);
		}
	}
}