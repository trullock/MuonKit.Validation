using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.String
{
	[TestFixture]
	public class When_validating_a_property_as_not_null_or_empty
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

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;
			validationReport.Violations.First().Error.Key.ShouldEqual("Required");
		}

		[Test]
		public void ensure_empty_string_fail_validation()
		{
			var testClass = new TestClass(string.Empty);

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("Required");
		}

		[Test]
		public void ensure_not_null_or_empty_passes_validation()
		{
			var testClass = new TestClass("a");

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;

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
				Ensure(x => x.value.IsNotNullOrEmpty());
			}
		}
	}
}