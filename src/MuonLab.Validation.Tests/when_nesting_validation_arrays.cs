using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	public sealed class when_nesting_validation_arrays
	{
		[Test]
		public void CorrectPropertyChainGeneratedFromChildList()
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

			var validator = new OuterClassChildListValidator();

			var validationReport = validator.Validate(outerClass);

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			var error1 = ReflectionHelper.PropertyChainToString(violations[0].Property, '.');

			error1.ShouldEqual("InnerClasses[0].InnerInnerClasses[0].InnerInnerInnerClasses[0].Property");
		}

		[Test]
		public void CorrectPropertyChainGeneratedFromCollection()
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
						},new InnerInnerInnerClass
						{
							Property = "Hello"
						},new InnerInnerInnerClass()}
					}}
				}}
			};

			var validator = new OuterClassCollectionValidator();

			var validationReport = validator.Validate(outerClass);

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			var error1 = ReflectionHelper.PropertyChainToString(violations[0].Property, '.');

			error1.ShouldEqual("InnerClasses[0].InnerInnerClasses[0].InnerInnerInnerClasses[0].Property");
		}

		public class OuterClassChildListValidator : Validator<OuterClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerClasses.AllSatisfy(new InnerClassChildListValidator()));
			}
		}

		public class InnerClassChildListValidator : Validator<InnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerInnerClasses.AllSatisfy(new InnerInnerClassChildListValidator()));
			}
		}

		public class InnerInnerClassChildListValidator : Validator<InnerInnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerInnerInnerClasses.AllSatisfy(new InnerInnerInnerClassChildListValidator()));
			}
		}

		public class InnerInnerInnerClassChildListValidator : Validator<InnerInnerInnerClass>
		{
			protected override void Rules()
			{
				When(x => x.IsNotNull(), () => Ensure(x => x.Property.IsNullOrIsEmpty()));
			}
		}

		public class OuterClassCollectionValidator : Validator<OuterClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerClasses.AllSatisfy(new InnerClassCollectionValidator()));
			}
		}

		public class InnerClassCollectionValidator : Validator<InnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerInnerClasses.AllSatisfy(new InnerInnerClassCollectionValidator()));
			}
		}

		public class InnerInnerClassCollectionValidator : Validator<InnerInnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerInnerInnerClasses.DoesNotHaveDuplicates(y => y.Property));
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