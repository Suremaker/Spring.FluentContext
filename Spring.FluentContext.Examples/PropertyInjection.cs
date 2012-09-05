using System;
using Spring.Context;

namespace Spring.FluentContext.Examples
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

	class Address
	{
		public string Street { get; set; }
		public string PostCode { get; set; }
		public string City { get; set; }

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", Street, PostCode, City);
		}
	}

	internal class PropertyInjection : Example
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
