using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.NullProperties
{
	[TestFixture]
	public class when_validating_a_null_property
	{
		private TestClassValidator validator;
		private ValidationReport report;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
			this.report = Task.Run(() => this.validator.Validate(new TestClass())).Result;
		}

		[Test]
		public void the_validation_report_should_be_valid()
		{
			report.IsValid.ShouldBeTrue();
		}

		public class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Name.Satisfies(p => true, "should work!"));
			}
		}
	}
}