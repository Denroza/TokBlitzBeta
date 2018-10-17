using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TokBlitzBeta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameMode : ContentPage
	{
		public GameMode ()
		{
			InitializeComponent ();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bgSP.png");
            StarEasy.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.starshine.png");
            StarModerate.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.star.png");
            StarChallenge.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.star.png");
            StartButton.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.bGS.png");
            StarModerateShine.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.starshine.png");
            StarChallengeShine.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.starshine.png");
          StarModerateShine.Opacity = 0;
         StarChallengeShine.Opacity = 0;
            LabelDifficulty.Text = "EASY";
            GameLogic.Logic.GetDifficulty("Easy");
            GameLogic.Logic.SetDifficulty();

        }

        private async void SelectEasy(object sender, EventArgs e) {
            LabelDifficulty.Text = "EASY";
            GameLogic.Logic.GetDifficulty("Easy");
            GameLogic.Logic.SetDifficulty();
            await StarModerateShine.FadeTo(0, 4000);
            await StarChallengeShine.FadeTo(0, 4000);
         //  await DisplayAlert("Check", GameLogic.Logic.SetDifficulty(), "Ok");
            //StarModerate.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.star.png");
            //StarChallenge.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.star.png");
        }
        private async void SelectModerate(object sender, EventArgs e) {
            LabelDifficulty.Text = "MODERATE";
            GameLogic.Logic.SetDifficulty();
            GameLogic.Logic.GetDifficulty("Moderate");
            await StarModerateShine.FadeTo(1, 4000);
            await StarChallengeShine.FadeTo(0, 4000);

          //  await DisplayAlert("Check", GameLogic.Logic.SetDifficulty(), "Ok");
            // StarModerate.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.starshine.png");
            //  StarChallenge.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.star.png");

        }
        private async void SelectChallenge() {
            LabelDifficulty.Text = "CHALLENGING";
            GameLogic.Logic.GetDifficulty("Challenging");
            GameLogic.Logic.SetDifficulty();
            await StarModerateShine.FadeTo(1, 4000);
            await StarChallengeShine.FadeTo(1, 4000);
            //await DisplayAlert("Check", GameLogic.Logic.SetDifficulty(), "Ok");
            // StarModerate.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.starshine.png");
            // StarChallenge.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.starshine.png");

        }
      
        private async void Start_Tapped(object sender, EventArgs e)
        {
           await DisplayAlert("Difficulty", GameLogic.Logic.SetDifficulty(), "OK");
            if (GameLogic.Logic.setPlayers() == false)
            {
                var navPage = new NavigationPage(new Sample());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
            }
            else {
                var navPage = new NavigationPage(new MultiplayerInGame());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;


            }


        }

    }
}