using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class when_validating_against_the_a_nested_parameter_rule
	{
		private TestClassWrapperValidator validator;
		private ValidationReport report;

		[SetUp]
		public async Task SetUp()
		{
			this.validator = new TestClassWrapperValidator();
			this.report = await this.validator.Validate(new TestClassWrapper());
		}

		[Test]
		public void the_validation_report_should_be_invalid()
		{
			report.IsValid.ShouldBeFalse();
		}

		public class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Satisfies(p => false, "should fail"));
			}
		}

		public class TestClassWrapperValidator : Validator<TestClassWrapper>
		{
			private TestClassValidator classValidator;

			public TestClassWrapperValidator()
			{
				this.classValidator = new TestClassValidator();
			}

			protected override void Rules()
			{
				Ensure(x => x.TestClass.Satisfies(classValidator));
			}
		}
	}
}