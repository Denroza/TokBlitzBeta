using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokBlitzBeta.GamePlay;
namespace TokBlitzBeta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameLevel : ContentPage
	{
		public GameLevel ()
		{
			InitializeComponent ();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.bg.png");
            Panel.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.panel.png");
            Home.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.home.png");
            Back.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.return.png");
            Sign.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Labels.level.png");
            L1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.1.png");
            L2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.2.png");
            L3.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.3.png");
            L4.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.4.png");

        }

        public async void L1Tapped(object sender, EventArgs e)
        {
            Flow.StarSetter(1);

            var navPage = new NavigationPage(new Blank1());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;

        }
        public async void L2Tapped(object sender, EventArgs e)
        {
            Flow.StarSetter(2);
            var navPage = new NavigationPage(new Blank1());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;
        }
        public async void L3Tapped(object sender, EventArgs e)
        {
            Flow.StarSetter(3);
            var navPage = new NavigationPage(new Blank1());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;
        }
        public async void L4Tapped(object sender, EventArgs e)
        {
            Flow.StarSetter(4);
            var navPage = new NavigationPage(new Blank1());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;
        }
        public void BackTapped(object sender, EventArgs e)
        {
            ButtonEvents.BactToGameType();
        }
        public void HomeTapped(object sender, EventArgs e)
        {
            ButtonEvents.BactToHomePage();
        }
    }
}