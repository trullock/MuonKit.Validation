using System.Collections.Generic;
using System.Linq;

namespace MuonLab.Validation
{
	public sealed class ValidationReport
	{
		public IEnumerable<IViolation> Violations { get; }

		public bool IsValid => !this.Violations.Any();

		public ValidationReport()
		{
			this.Violations = new List<IViolation>();
		}

		public ValidationReport(IEnumerable<IViolation> violations)
		{
			this.Violations = violations;
		}
	}
}