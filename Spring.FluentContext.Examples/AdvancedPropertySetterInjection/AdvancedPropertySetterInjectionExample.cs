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

using System;
using Spring.Context;
using Spring.FluentContext.Examples.AdvancedPropertySetterInjection.Objects;

namespace Spring.FluentContext.Examples.AdvancedPropertySetterInjection
{
	internal class AdvancedPropertySetterInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			FluentApplicationContext ctx = new FluentApplicationContext();

			ctx.RegisterDefault<BobEngineer>();

			//the factories are registered with ids to allow having multiple instances of same class in context
			ctx.RegisterNamed<Factory>("standardFactory") 
				.BindProperty(f => f.Engineer).ToRegisteredDefaultOf<BobEngineer>()
				.BindProperty(f => f.Description).ToValue("Standard Factory");

			ctx.RegisterDefault<RobotEngineer>();

			ctx.RegisterNamed<Factory>("roboticFactory")
				.BindProperty(f => f.Engineer).ToRegisteredDefaultOf<RobotEngineer>()
				.BindProperty(f => f.Description).ToValue("Robotic Super Factory");

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			IFactory factory = ctx.GetObject<IFactory>("standardFactory");
			IFactory roboticFactory = ctx.GetObject<IFactory>("roboticFactory");

			BuildHouseUsing(factory);
			BuildHouseUsing(roboticFactory);
		}

		private void BuildHouseUsing(IFactory factory)
		{
			Console.WriteLine("\nUsing {0} to build house", factory.Description);
			factory.BuildHouse();
		}
	}
}