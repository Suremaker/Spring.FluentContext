//
//  Author:
//    Wojciech Kotlarski
//
//  Copyright (c) 2013, Wojciech Kotlarski
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
using System.Linq;
using Spring.Context;
using Spring.FluentContext.Definitions;
using Spring.FluentContext.Examples.CollectionInjection.Objects;

namespace Spring.FluentContext.Examples.CollectionInjection
{
	internal class CollectionInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			var painterRef = ctx.RegisterDefault<Painter>().GetReference();

			ctx.RegisterNamed<Polisher>("polisher");

			ctx.RegisterDefault<Distributor>()
				.BindProperty(d => d.DistributionMap).ToDictionary(dict =>
				{
					dict[Def.Value("Motorcycle")] = Def.Reference<Shop>();
					dict[Def.Value("Car")] = Def.Reference<Shop>();
					dict[Def.Value("Luxury Car")] = Def.Object<Shop>(s => s.BindConstructorArg<string>().ToValue("Luxury Store that nobody knows."));
				});

			ctx.RegisterDefault<Shop>()
				.UseConstructor<string>(name => new Shop(name))
				.BindConstructorArg().ToValue("Local store");

			ctx.RegisterDefault<Factory>()
				.UseConstructor<IWorker[]>(workers => new Factory(workers))
				.BindConstructorArg()
				.ToArray(
					Def.Object<Constructor>(),
					painterRef,
					Def.Reference<Polisher>("polisher"),
					Def.Reference<Distributor>()
				);

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var factory = ctx.GetObject<Factory>();
			factory.Create("Car");
			factory.Create("Motorcycle");
			factory.Create("Luxury Car");

			var shopsInContext = ctx.GetObjectsOfType(typeof(Shop)).Values.OfType<Shop>().Select(s => s.ToString()).ToArray();
			Console.WriteLine("Shops available in context: {0}", string.Join(",", shopsInContext));
			Console.WriteLine("Done...");
		}
	}
}
