using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.EnsureAsync
{
	public class When_validating_a_property_as_false
	{
		[Fact]
		public void ensure_false_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(false);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void ensure_true_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(true);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("BeFalse");
		}

		private class TestClass
		{
			public bool value { get; set; }

			public TestClass(bool value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.Satisfies(v => AsyncFalseCheck(v), "BeFalse"));
			}

			async Task<bool> AsyncFalseCheck(bool b)
            {
                await Task.Delay(1);
				return b == false;
			}
		}
	}
}