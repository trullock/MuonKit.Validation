using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	public sealed class when_nesting_validation_arrays
	{
		[Test]
		public void CorrectPropertyChainGenerated()
		{
			var outerClass = new OuterClass
			{
				InnerClasses = new[] {new InnerClass
				{
					InnerInnerClasses = new[] {new InnerInnerClass
					{
						InnerInnerInnerClasses = new [] {new InnerInnerInnerClass
						{
							Property = "Hello"
						}}
					}}
				}}
			};

			var validator = new OuterClassValidator();

			var validationReport = validator.Validate(outerClass);

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			var error1 = ReflectionHelper.PropertyChainToString(violations[0].Property, '.');

			error1.ShouldEqual("InnerClasses[0].InnerInnerClasses[0].InnerInnerInnerClasses[0].Property");
		}

		public class OuterClassValidator : Validator<OuterClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerClasses.AllSatisfy(new InnerClassValidator()));
			}
		}

		public class InnerClassValidator : Validator<InnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerInnerClasses.AllSatisfy(new InnerInnerClassValidator()));
			}
		}

		public class InnerInnerClassValidator : Validator<InnerInnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerInnerInnerClasses.AllSatisfy(new InnerInnerInnerClassValidator()));
			}
		}

		public class InnerInnerInnerClassValidator : Validator<InnerInnerInnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Property.IsNotEqualTo("Hello"));
			}
		}

		public class OuterClass
		{
			public IList<InnerClass> InnerClasses { get; set; }
		}

		public class InnerClass
		{
			public IList<InnerInnerClass> InnerInnerClasses { get; set; }
		}

		public class InnerInnerClass
		{
			public IList<InnerInnerInnerClass> InnerInnerInnerClasses { get; set; }
		}

		public class InnerInnerInnerClass
		{
			public string Property { get; set; }
		}
	}
}