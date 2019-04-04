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
	public partial class GameTypes : ContentPage
	{
		public GameTypes ()
		{
			InitializeComponent ();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.bg.png");
            Panel.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.panel.png");
            Home.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.home.png");
            Back.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.return.png");
            Sign.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Labels.type.png");
            ContinueButton.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.conti.png");

        }
        TokGamesApiClient apiClient = new TokGamesApiClient();
        public void BackTapped(object sender, EventArgs e)
        {
                ButtonEvents.BactToGameType();
        }
        public void HomeTapped(object sender, EventArgs e)
        {
            ButtonEvents.BactToHomePage();
        }
        public async void ContinueTapped(object sender, EventArgs e) {
            await apiClient.GetGameToksAsync(new GameToksQuery() { tok_group=Selection.SetSelectedType(),tok_type=Types.Items[Types.SelectedIndex], count=-1 });
            LoadingBit.IsVisible = true;
            Fetch.IsRunning = true;
            await Task.Delay(TokGamesApiClient.SetGameToks().Count*500);
            string[] listitems = new string[TokGamesApiClient.SetGameToks().Count];
            for (int i = 0; i < TokGamesApiClient.SetGameToks().Count; i++)
            {
                listitems[i] = TokGamesApiClient.SetGameToks()[i].PrimaryFieldText;
            }
            LoadingBit.IsVisible = false;
            Fetch.IsRunning = false;
            await DisplayAlert("Results", listitems.Count().ToString(),"ok");
             for (int i = 0; i < listitems.Count(); i++)
            {
                await DisplayAlert("Results", listitems[i], "ok");
            }
        }
    }
}