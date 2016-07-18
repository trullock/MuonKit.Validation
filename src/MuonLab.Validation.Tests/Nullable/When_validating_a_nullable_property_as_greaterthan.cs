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
		public void ensure_0_returns_false()
		{
			var testClass = new TestClass(0);

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;
			var violations = validationReport.Violations.ToArray();

			violations[0].Error.Key.ShouldEqual("GreaterThanEq");
			violations[0].Error.Replacements["arg0"].Value.ShouldEqual("1");
		}

		[Test]
		public void ensure_1_returns_true()
		{
			var testClass = new TestClass(1);

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Test]
		public void ensure_null_returns_true()
		{
			var testClass = new TestClass(null);

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;

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