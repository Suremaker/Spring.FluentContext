using System;

namespace Spring.FluentContext.Examples
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Usage examples of Spring.FluentContext:\n---------------------------------------");
			var examples = new Example[]
				{
					new ConstructorInjection(), 
					new PropertyInjection(),
					new LookupMethodInjection(), 
					new AdvancedPropertySetterInjection(), 					
					new AdvancedLookupMethodInjection()
				};

			foreach (var example in examples)
				example.Show();
		}
	}
}
