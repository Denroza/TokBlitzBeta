using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TokBlitzBeta.GameLogic;
namespace TokBlitzBeta
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Execution.ResetRound();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.Homescreen.png");
            btnSP.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.bSP.png");
            btnMulti.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.bMP.png");
            ResetSelections();
                // buttonSingle.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bt.png");

        }
        private async void Single_player_Tapped(object sender, EventArgs e) {
            GameLogic.Logic.isMultiplayer(false);
            var navPage = new NavigationPage(new GameType());
            ButtonAnimation.ButtonAnimate.ButtonImageAnimation(btnSP);
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;
        }
        private async void Multiplayer_player_Tapped(object sender, EventArgs e) {
            GameLogic.Logic.isMultiplayer(true);
            var navPage = new NavigationPage(new GameType());
            ButtonAnimation.ButtonAnimate.ButtonImageAnimation(btnMulti);
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;

        }
        public void ResetSelections() {
            Logic.GetCategory("");
            Logic.GetDifficulty("");
            Logic.GetGameType("");
            Logic.GetTokGroup("");
          
        }

    
    }
}
