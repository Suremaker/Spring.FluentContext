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
			var path = ReflectionUtils.GetPropertyPath<Func<NestingHolder,string>>(n => n.Nested.Other.Text);
			Assert.That(path, Is.EqualTo("Nested.Other.Text"));
		}

		[Test]
		public void Get_property_path_throws_if_unsupported_expression_is_used_in_selector()
		{
			var ex = Assert.Throws<ArgumentException>(() => ReflectionUtils.GetPropertyPath<Func<NestingHolder, string>>(n => n.Nested.Other.ToString()));
			Assert.That(ex.Message, Does.StartWith("Lambda expression can contain only property access expressions."));
		}

		[Test]
		public void Get_property_path_throws_if_unsupported_expression_is_used_in_any_part_of_selector()
		{
			var ex = Assert.Throws<ArgumentException>(() => ReflectionUtils.GetPropertyPath<Func<DateTime, int>>(d => d.AddDays(1).Second));
			Assert.That(ex.Message, Does.StartWith("Lambda expression can contain only property access expressions."));
		}
	}
}