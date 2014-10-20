using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Enumerables
{
	public class When_validating_for_duplicates
	{
		[Test]
		public void InnerViolationsShouldBeReportedCorrectly()
		{
			var testClass = new TestClass
			                	{
									List = new[] { new InnerClass { Num = 1 }, new InnerClass { Num = 1 }, new InnerClass { Num = 3 } }
			                	};

			var testClassValidator = new TestClassValidator();

			var validationReport = testClassValidator.Validate(testClass);

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			var error1 = ReflectionHelper.PropertyChainToString(violations[0].Property, '.');
			error1.ShouldEqual("List[0].Name");

			var error2 = ReflectionHelper.PropertyChainToString(violations[1].Property, '.');
			error2.ShouldEqual("List[2].Name");
		}

		public class TestClass
		{
			public IList<InnerClass> List { get; set; }
		}

		public class InnerClass
		{
			public int Num { get; set; }
		}

		public class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.List.Satisfies(l => l.Distinct().Count() == l.Count, "contains duplicates"));
			}
		}
	}
}