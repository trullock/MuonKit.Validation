using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public sealed class Violation : IViolation
	{
		public Expression Property { get; }
		public ErrorDescriptor Error { get; }
		public object AttemptedValue { get; }

		public Violation(ErrorDescriptor error, Expression property, object attemptedValue)
		{
			this.Property = property;
			this.Error = error;
			this.AttemptedValue = attemptedValue;
		}
	}
}