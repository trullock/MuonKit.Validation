using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Exceptions
{
	[TestFixture]
	public class When_failing_to_validate_a_parameter
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task ensure_exception_is_caught_and_reported()
		{
			var testClass = new TestClass();

			var validationReport = await this.validator.Validate(testClass);

			var errorDescriptor = validationReport.Violations.First().Error;
			errorDescriptor.Key.ShouldEqual("ValidationError");
		}
		

		private class TestClass
		{
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Satisfies(b => Throw(), "foo"));
			}

			static bool Throw()
			{
				throw new Exception();
			}
		}
	}
}