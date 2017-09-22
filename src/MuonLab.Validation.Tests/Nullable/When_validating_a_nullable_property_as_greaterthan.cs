using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Nullable
{
	[TestFixture]
	public class When_validating_a_nullable_property_as_greaterthan
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task ensure_0_returns_false()
		{
			var testClass = new TestClass(0);

			var validationReport = await this.validator.Validate(testClass);
			var violations = validationReport.Violations.ToArray();

			violations[0].Error.Key.ShouldEqual("GreaterThanEq");
			violations[0].Error.Replacements["arg0"].Value.ShouldEqual("1");
		}

		[Test]
		public async Task ensure_1_returns_true()
		{
			var testClass = new TestClass(1);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Test]
		public async Task ensure_null_returns_true()
		{
			var testClass = new TestClass(null);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public int? NullableInt { get; set; }

			public TestClass(int? value)
			{
				this.NullableInt = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				When(x => x.NullableInt.HasValue(), () => Ensure(x => x.NullableInt.Value.IsGreaterThanOrEqualTo(1)));
			}
		}
	}
}