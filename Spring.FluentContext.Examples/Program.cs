﻿//
//  Author:
//    Wojciech Kotlarski
//
//  Copyright (c) 2012, Wojciech Kotlarski
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.	 
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
//  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using Spring.FluentContext.Examples.AdvancedLookupMethodInjection;
using Spring.FluentContext.Examples.AdvancedPropertySetterInjection;
using Spring.FluentContext.Examples.Complex;
using Spring.FluentContext.Examples.ConstructorInjection;
using Spring.FluentContext.Examples.LookupMethodInjection;
using Spring.FluentContext.Examples.PropertyInjection;
using Spring.FluentContext.Examples.ProxyFactoryUsage;
using Spring.FluentContext.Examples.VariousCreationMethods;

namespace Spring.FluentContext.Examples
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Usage examples of Spring.FluentContext:\n---------------------------------------");
			var examples = new Example[]
			{
				new ConstructorInjectionExample(), 
				new PropertyInjectionExample(),
				new LookupMethodInjectionExample(), 
				new VariousCreationMethodsExample(),
				new AdvancedPropertySetterInjectionExample(), 					
				new AdvancedLookupMethodInjectionExample(),
				new ProxyFactoryUsageExample(),
				new ComplexExample()
			};

			foreach(var example in examples)
				example.Show();
		}
	}
}
