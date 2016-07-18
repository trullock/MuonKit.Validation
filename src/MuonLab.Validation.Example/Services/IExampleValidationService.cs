using System.Threading.Tasks;

namespace MuonLab.Validation.Example.Services
{
	public interface IExampleValidationService
	{
		Task<bool> Exists(string email);
	}

	class ExampleValidationService : IExampleValidationService
	{
		public Task<bool> Exists(string email)
		{
			return Task.FromResult(false);
		}
	}
}