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
using System.Threading;
using Spring.Context;

namespace Spring.FluentContext.Examples.Complex
{
	class ComplexExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Endpoint>();

			ctx.RegisterDefault<DisplayCommand>()
				.AsPrototype(); //every request for DisplayCommand should return new instance

			ctx.RegisterDefault<RepeatingInterceptor>();
			ctx.RegisterDefault<DelayingInterceptor>()
				.BindConstructorArg<int>().ToValue(3000);

			ctx.RegisterDefaultProxyFactory<ICommand>()
				.TargetingDefaultOfType<DisplayCommand>()
				.ReturningPrototypes() //every request for ICommand should return new instance of proxy (comment it and type few lines during program run to see change)
				.AddInterceptorByDefaultReference<DelayingInterceptor>()
				.AddInterceptorByDefaultReference<RepeatingInterceptor>(); //Repeating interceptor is called after DelyingInterceptor, so there would be no delays between repeats

			ctx.RegisterDefault<Sender>()
				.DependingOnDefault<Consumer>()
				.Autowire(); //autowiring endpoint dependency

			ctx.RegisterDefault<Consumer>()
				.Autowire() //autowiring endpoint dependency
				.BindLookupMethodNamed<ICommand>("GetCommand").ToRegisteredDefault() //method is protected so it is not possible to use lambda to get it
				.CallOnInit(c => c.Start());

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var sender = ctx.GetObject<Sender>();
			sender.Run();
			//to let background tasks to finish
			Thread.Sleep(3000);
		}
	}
}

