using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Enumerables
{
	[TestFixture]
	public class when_validating_an_enumerable_contains_elements
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task an_empty_list_should_be_false()
		{
			var testClass = new TestClass();

			var report = await this.validator.Validate(testClass);

			report.IsValid.ShouldBeFalse();
		}


		[Test]
		public async Task an_non_empty_list_should_be_true()
		{
			var testClass = new TestClass
			                	{
			                		List = new[] { "an item" }
			                	};

			var report = await this.validator.Validate(testClass);

			report.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public IEnumerable List { get; set; }
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.List.ContainsElements());
			}
		}
	}
}