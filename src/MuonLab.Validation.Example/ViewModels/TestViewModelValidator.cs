namespace MuonLab.Validation.Example.ViewModels
{
	public class TestViewModelValidator : Validator<TestViewModel>
	{
		IExampleValidationService exampleValidationService;

		public TestViewModelValidator(IExampleValidationService exampleValidationService) 
		{
			this.exampleValidationService = exampleValidationService;
		}

		protected override void Rules()
		{
			this.Ensure(x => x.Email.IsNotNullOrEmpty()).And(()=>
				this.Ensure(x => x.Email.IsAValidEmailAddress()));

			this.Ensure(x => x.Password.HasMinimumLength(8));

			this.Ensure(x => x.Password.IsNotNullOrEmpty()).And(() => 
				this.Ensure(x => x.ConfirmPassword.IsEqualTo(x.Password)));
		}
	}
}