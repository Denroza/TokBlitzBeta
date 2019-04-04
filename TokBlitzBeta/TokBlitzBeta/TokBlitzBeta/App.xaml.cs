using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokBlitzBeta.GamePlay;
using Plugin.Connectivity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TokBlitzBeta.GamePlay;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TokBlitzBeta
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            BGMusics.LoadGameSounds();
            MainPage = new MainPage();
            
        }

        protected override async void OnStart()
        {

            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
