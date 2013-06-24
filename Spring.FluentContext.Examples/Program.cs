using System;
using Spring.FluentContext.Examples.AdvancedLookupMethodInjection;
using Spring.FluentContext.Examples.AdvancedPropertySetterInjection;
using Spring.FluentContext.Examples.CollectionInjection;
using Spring.FluentContext.Examples.Complex;
using Spring.FluentContext.Examples.ConstructorInjection;
using Spring.FluentContext.Examples.LookupMethodInjection;
using Spring.FluentContext.Examples.PropertyInjection;
using Spring.FluentContext.Examples.ProxyFactoryUsage;
using Spring.FluentContext.Examples.VariousCreationMethods;

namespace Spring.FluentContext.Examples
{
	static class Program
	{
		static void Main()
		{
			Console.WriteLine("Usage examples of Spring.FluentContext:\n---------------------------------------");
			var examples = new Example[]
			{
				new ConstructorInjectionExample(), 
				new PropertyInjectionExample(),
				new LookupMethodInjectionExample(), 
				new CollectionInjectionExample(),
				new VariousCreationMethodsExample(),
				new AdvancedPropertySetterInjectionExample(),
				new AdvancedLookupMethodInjectionExample(),
				new ProxyFactoryUsageExample(),
				new ComplexExample()
			};

			foreach (var example in examples)
				example.Show();
		}
	}
}
