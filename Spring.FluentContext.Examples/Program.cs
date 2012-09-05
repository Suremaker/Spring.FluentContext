using System;

namespace Spring.FluentContext.Examples
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Usage examples of Spring.FluentContext: \n\n");
			var examples = new Example[]
				{
					new AdvancedPropertySetterInjection(), 
					new ConstructorInjection(), 
					new AdvancedLookupMethodInjection()
				};

			foreach (var example in examples)
				example.Show();

			Console.ReadKey();
		}
	}
}
