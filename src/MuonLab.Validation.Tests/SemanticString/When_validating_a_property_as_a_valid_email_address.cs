using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.SemanticString
{
	[TestFixture]
	public class When_validating_a_property_as_a_valid_email_address
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task ensure_nulls_fail_validation()
		{
			var testClass = new TestClass(null);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("ValidEmail");
		}

		[Test]
		public async Task ensure_empty_string_fail_validation()
		{
			var testClass = new TestClass(string.Empty);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("ValidEmail");
		}

		[Test]
		public async Task ensure_valid_email_passes_validation()
		{
			var testClass = new TestClass("trullock@gmail.com");

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public async Task ensure_invalid_email2_fails_validation()
		{
			var testClass = new TestClass("trullock@gmail@com");
			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("ValidEmail");
		}

		[Test]
		public async Task ensure_invalid_email3_fails_validation()
		{
			var testClass = new TestClass("muonlab.com");
			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("ValidEmail");
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
				Ensure(x => x.value.IsAValidEmailAddress());
			}
		}
	}
}