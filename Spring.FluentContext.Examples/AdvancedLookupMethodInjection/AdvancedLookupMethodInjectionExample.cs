//
//  Author:
//    Wojciech Kotlarski wojciech.kotlarski@gmail.com
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

namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	internal class AdvancedLookupMethodInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Pig>()
				.AsPrototype() //every request will return a new instance of Pig
				.BindConstructorArg<string>().ToValue("Small Piggy");

			ctx.RegisterDefault<Cow>()
				.AsPrototype();

			ctx.RegisterDefault<Barn>()
				.AsPrototype();

			ctx.RegisterDefault<Pigstry>()
				.AsSingleton(); //The Pigstry is registered as singleton, so every request returns the same instance (what is visible on second RunFarm())

			ctx.RegisterNamed<Farm>("pigFarm")
				.BindLookupMethod(f => f.CreateAnimal()).ToRegisteredDefaultOfType<Pig>()
				.BindLookupMethod(f => f.CreateShelter()).ToRegisteredDefaultOfType<Pigstry>();

			ctx.RegisterNamed<Farm>("cowFarm")
				.BindLookupMethod(f => f.CreateAnimal()).ToRegisteredDefaultOfType<Cow>()
				.BindLookupMethod(f => f.CreateShelter()).ToRegisteredDefaultOfType<Barn>();

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			IFarm cowFarm = ctx.GetObject<IFarm>("cowFarm");
			IFarm pigFarm = ctx.GetObject<IFarm>("pigFarm");

			Console.WriteLine("\nRunning cow farm");
			cowFarm.RunFarm();

			Console.WriteLine("\nRunning pig farm");
			pigFarm.RunFarm();

			Console.WriteLine("\nRunning cow farm for second time");
			cowFarm.RunFarm();

			Console.WriteLine("\nRunning pig farm for second time");
			pigFarm.RunFarm();
		}
	}
}