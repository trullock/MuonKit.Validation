using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable.LessThanEq
{
	[TestFixture]
	public class When_validation_a_nullable_property_as_less_than_or_equal_to_a_sclar
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task test_1_less_than_or_equal_to_4_returns_true()
		{
			var testClass = new TestClass(1);

			var validationReport = await this.validator.Validate(testClass); 

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public async Task test_8_less_than_or_equal_to_4_returns_false()
		{
			var testClass = new TestClass(8);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("LessThanEq");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("4");
		}

		[Test]
		public async Task test_4_less_than_or_equal_to_4_returns_true()
		{
			var testClass = new TestClass(4);

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		private class TestClass
		{
			public int? Value { get; }

			public TestClass(int value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsLessThanOrEqualTo(4));
			}
		}
	}
}