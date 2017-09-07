using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable
{
	[TestFixture]
	public class When_validating_a_null_property_as_equal_to_another_null_property
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task should_be_valid()
		{
			var testClass = new TestClass();

			var validationReport = await this.validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}


		private class TestClass
		{
			public string Value { get; set; }
			public string Value2 { get; set; }

		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsEqualTo(x.Value2));
			}
		}
	}
}