using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.String
{
	[TestFixture]
	public class When_validating_a_property_for_minimum_length
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void ensure_nulls_fail_validation()
		{
			var testClass = new TestClass(null);

			var validationReport = this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("MinLength");
			validationReport.Violations.First().Error.Replacements["prop"].ShouldEqual("value");
			validationReport.Violations.First().Error.Replacements["arg0"].ShouldEqual("5");
		}

		[Test]
		public void ensure_strings_that_are_too_short_fail_validation()
		{
			var testClass = new TestClass("1234");

			var validationReport = this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("MinLength");
			validationReport.Violations.First().Error.Replacements["prop"].ShouldEqual("value");
			validationReport.Violations.First().Error.Replacements["arg0"].ShouldEqual("5");
		}

		[Test]
		public void ensure_strings_that_are_the_minimum_length_pass_validation()
		{
			var testClass = new TestClass("12345");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_strings_that_are_longer_than_the_minimum_length_pass_validation()
		{
			var testClass = new TestClass("123456");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		private class TestClass
		{
			public string value { get; set; }

			public TestClass(string value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.HasMinimumLength(5));
			}
		}
	}
}