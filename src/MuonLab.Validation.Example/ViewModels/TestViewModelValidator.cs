namespace MuonLab.Validation.Example.ViewModels
{
	public class TestViewModelValidator : Validator<TestViewModel>
	{
		protected override void Rules()
		{
			this.Ensure(x => x.Email.IsNotNullOrEmpty()).And(() =>
				// You'd want your own rules here
				this.Ensure(x => x.Email.Matches(@"[a-z]\@[a-z]\.com", "Invalid")));

			this.Ensure(x => x.Password.HasMinimumLength(8));

			this.Ensure(x => x.Password.IsNotNullOrEmpty()).And(() =>
				this.Ensure(x => x.ConfirmPassword.IsEqualTo(x.Password)));
		}
	}
}