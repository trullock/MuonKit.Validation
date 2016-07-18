using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class when_validating_against_the_parameter_with_no_property
	{
		private TestValidator validator;
		private ValidationReport report;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestValidator();
			this.report = Task.Run(() => this.validator.Validate(new TestClass())).Result;
		}

		[Test]
		public void the_validation_report_should_be_invalid()
		{
			report.IsValid.ShouldBeFalse();
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Satisfies(p => false, "should fail"));
			}
		}
	}
}