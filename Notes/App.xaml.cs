using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using Notes.IRepositories;

namespace Notes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            DependencyService.Get<ISqLiteDatabaseConnection>().GetConnectionWithDatabase();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
