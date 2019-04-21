namespace Voting.UIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;
        public static string Error => Resource.Error;
        public static string EmailError => Resource.EmailError;
        public static string PasswordError => Resource.PasswordError;
        public static string LoginError => Resource.LoginError;
        public static string Login => Resource.Login;
        public static string Email => Resource.Email;
        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;
        public static string Password => Resource.Password;
        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;
        public static string Remember => Resource.Remember;
        public static string NewUser => Resource.NewUser;
        public static string Forgot => Resource.Forgot;
        public static string RegisterNewUser => Resource.RegisterNewUser;
        public static string FirstName => Resource.FirstName;
        public static string FirstNamePlaceHolder => Resource.FirstNamePlaceHolder;
        public static string LastName => Resource.LastName;
        public static string LastNamePlaceHolder => Resource.LastNamePlaceHolder;
        public static string Country => Resource.Country;
        public static string CountryPlaceHolder => Resource.CountryPlaceHolder;
        public static string City => Resource.City;
        public static string CityPlaceHolder => Resource.CityPlaceHolder;
        public static string Occupation => Resource.Occupation;
        public static string OccupationPlaceHolder => Resource.OccupationPlaceHolder;
        public static string Stratum => Resource.Stratum;
        public static string StratumPlaceHolder => Resource.StratumPlaceHolder;
        public static string Gender => Resource.Gender;
        public static string GenderPlaceHolder => Resource.GenderPlaceHolder;
        public static string Birthdate => Resource.Birthdate;
        public static string PasswordConfirm => Resource.PasswordConfirm;
        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;
        public static string FirstNameError => Resource.FirstNameError;
        public static string LastNameError => Resource.LastNameError;
        public static string EmailFormatError => Resource.EmailFormatError;
        public static string CountryError => Resource.CountryError;
        public static string CityError => Resource.CityError;
        public static string OccupationError => Resource.OccupationError;
        public static string GenderError => Resource.GenderError;
        public static string StratumError => Resource.StratumError;
        public static string BirthdateError => Resource.BirthdateError;
        public static string PasswordFormatError => Resource.PasswordFormatError;
        public static string PasswordConfirmError => Resource.PasswordConfirmError;
        public static string PasswordMatch => Resource.PasswordMatch;
        public static string About => Resource.About;
        public static string Menu => Resource.Menu;
        public static string AboutText => Resource.AboutText;
        public static string PasswordRecover => Resource.PasswordRecover;
        public static string EmailRecover => Resource.EmailRecover;
        public static string Ok => Resource.Ok;
        public static string Setup => Resource.Setup;
        public static string CloseSession => Resource.CloseSession;
        public static string SelectCandidate => Resource.SelectCandidate;
        public static string ModifyPassword => Resource.ModifyPassword;
        public static string ModifyUser => Resource.ModifyUser;
        public static string Save => Resource.Save;
        public static string UserUpdated => Resource.UserUpdated;
        public static string ChangePassword => Resource.ChangePassword;
        public static string CurrentPassword => Resource.CurrentPassword;
        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;
        public static string NewPassword => Resource.NewPassword;
        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;
        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;
        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;
        public static string PasswordCurrentErrorNull => Resource.PasswordCurrentErrorNull;
        public static string PasswordCurrentErrorMatch => Resource.PasswordCurrentErrorMatch;
    }
}
