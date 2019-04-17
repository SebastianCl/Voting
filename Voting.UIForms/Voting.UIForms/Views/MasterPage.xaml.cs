namespace Voting.UIForms.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()//cuando la pagina ya esta disponible para cargar en memoria
        {
            base.OnAppearing();
            App.Navigator = this.Navigator;//luego de estar logueamos a traves de la
            //aplication.current.mainpage para login
            //navigator luego de estar logueado 
            App.Master = this;
        }

    }
}