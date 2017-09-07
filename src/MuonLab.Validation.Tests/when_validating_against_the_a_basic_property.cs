using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class when_validating_against_the_a_basic_property
	{
		private TestValidator validator;
		private ValidationReport report;

		[SetUp]
		public async Task SetUp()
		{
			this.validator = new TestValidator();
			this.report = await this.validator.Validate(new TestClass());
		}

		[Test]
		public void the_validation_report_should_be_valid()
		{
			report.IsValid.ShouldBeTrue();
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Age.Satisfies(p => true, "should work!"));
			}
		}
	}
}