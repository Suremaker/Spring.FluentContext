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

namespace Spring.FluentContext.Examples.LookupMethodInjection
{
	internal class LookupMethodInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<ArithmenticMeanCalculator>();

			ctx.RegisterDefault<CreditsCalculator>()
				.BindConstructorArg<double>().ToValue(2.5)
			//the line below instruct Spring to override GetMeanCalculator() method with one returning ArithmeticMeanCalculator instance registered above
				.BindLookupMethod(c => c.GetMeanCalculator()).ToRegisteredDefaultOf<ArithmenticMeanCalculator>();

			ctx.RegisterDefaultAlias<ICreditsCalculator>().ToRegisteredDefault<CreditsCalculator>();

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var calc = ctx.GetObject<ICreditsCalculator>();
			CalculateCredits(calc, "Josh", 2.4, 4.3, 5.8);
			CalculateCredits(calc, "John", 2.4, 1.3, 3.2);
		}

		private void CalculateCredits(ICreditsCalculator calc, string person, params double[] points)
		{
			Console.WriteLine("{0} has {1} with his points: {2}",
				person,
				calc.IsAcceptable(points) ? "passed" : "NOT passed",
				string.Join(", ", points));

		}
	}
}