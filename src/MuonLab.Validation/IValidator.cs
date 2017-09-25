using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MuonLab.Validation
{
	public interface IValidator<TEntity> : IValidator
	{
		Task<ValidationReport> Validate(TEntity entity);
		Task<ValidationReport> Validate<TOuter>(TEntity entity, Expression<Func<TOuter, TEntity>> prefix);
	}

	public interface IValidator
	{
		Task<ValidationReport> Validate(object entity);
	}
}