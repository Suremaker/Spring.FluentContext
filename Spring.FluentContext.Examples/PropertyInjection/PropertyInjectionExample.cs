//
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
using Spring.Context;

namespace Spring.FluentContext.Examples.PropertyInjection
{
	internal class PropertyInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Person>()
				.BindProperty(p => p.Name).ToValue("John") //binds Person.Name to constant value
				.BindProperty(p => p.Address).ToRegisteredDefault(); //binds Person.Address to other object registered in context

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
