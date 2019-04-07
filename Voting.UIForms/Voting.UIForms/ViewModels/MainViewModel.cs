namespace Voting.UIForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;
        public LoginViewModel Login { get; set; }

        public EventsViewModel Events { get; set; }

        public MainViewModel()
        {
            instance = this;
        }

        //Singleton
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
