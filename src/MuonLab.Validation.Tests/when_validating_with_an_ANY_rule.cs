using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests
{
	public class when_validating_with_an_ANY_rule
	{
		[Fact]
		public void when_the_first_condition_is_true_the_second_shoudlnt_be_run()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(1, 2);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.Count().ShouldEqual(0);
		}

		[Fact]
		public void when_the_first_condition_is_false_and_the_second_is_true_there_should_be_no_errors()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(2, 3);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.Count().ShouldEqual(0);
		}

		[Fact]
		public void whe_all_conditions_are_false_all_errors_should_show()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(0, 2);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.Count().ShouldEqual(2);
		}

		class TestClass
		{
			public int Value { get; set; }
			public int Value2 { get; set; }

			public TestClass(int value, int value2)
			{
				this.Value = value;
				this.Value2 = value2;
			}
		}

		class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Any(() =>
				{
					Ensure(x => x.Value.IsEqualTo(1));
					Ensure(x => x.Value2.IsEqualTo(3));
				});
			}
		}
	}
}