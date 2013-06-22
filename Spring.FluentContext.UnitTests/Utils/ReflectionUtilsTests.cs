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
using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext.UnitTests.Utils
{
	[TestFixture]
	public class ReflectionUtilsTests
	{
		class NestingHolder { public NestingType Nested { get; set; } }

		[Test]
		public void Get_property_path_returns_selected_property_path()
		{
			var path = ReflectionUtils.GetPropertyPath((NestingHolder n) => n.Nested.Other.Text);
			Assert.That(path, Is.EqualTo("Nested.Other.Text"));
		}

		[Test]
		public void Get_property_path_throws_if_unsupported_expression_is_used_in_selector()
		{
			var ex = Assert.Throws<ArgumentException>(() => ReflectionUtils.GetPropertyPath((NestingHolder n) => n.Nested.Other.ToString()));
			Assert.That(ex.Message, Is.StringStarting("Lambda expression can contain only property access expressions."));
		}

		[Test]
		public void Get_property_path_throws_if_unsupported_expression_is_used_in_any_part_of_selector()
		{
			var ex = Assert.Throws<ArgumentException>(() => ReflectionUtils.GetPropertyPath((DateTime d) => d.AddDays(1).Second));
			Assert.That(ex.Message, Is.StringStarting("Lambda expression can contain only property access expressions."));
		}
	}
}