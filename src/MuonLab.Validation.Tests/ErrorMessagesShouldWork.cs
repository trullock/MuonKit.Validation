using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class ErrorMessagesShouldWork
	{
		TestValidator validator;
		ValidationReport report;

		[SetUp]
		public async Task SetUp()
		{
			this.validator = new TestValidator();
			this.report = await this.validator.Validate(new TestClass { Age = 12 });
		}

		[Test]
		public void the_validation_report_should_be_valid()
		{
			report.Violations.First().Error.Key.ShouldEqual("Key");
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Age.ShouldProduceErrorMessage(10));
			}
		}
	}

	public static class TestExtensions
	{
		public static ICondition<int> ShouldProduceErrorMessage(this int self, int someArg)
		{
			return self.Satisfies(x => false, "Key");
		}
	}
}