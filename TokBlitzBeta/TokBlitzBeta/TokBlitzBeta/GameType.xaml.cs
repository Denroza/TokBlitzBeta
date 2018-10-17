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
	public partial class GameType : ContentPage
	{
		public GameType ()
		{
			InitializeComponent ();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bgSP.png");
            TokGroupButton.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.bTG.png");
            CategoryButton.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.bC.png");
            CustomButton.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.bCG.png");
            CreateButton.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.btnC.png");
        }

        private void TokGroupButton_Tapped(object sender, EventArgs e) {
            GameLogic.Logic.GetGameType("TokGroup");
            GameLogic.Logic.GetCategory("");
            if (pick_type.IsVisible == false && CategoryButton.IsVisible == true && CustomButton.IsVisible == true && CreateButton.IsVisible == false)
            {
                pick_type.IsVisible = true;
                CategoryButton.IsVisible = false;
                CustomButton.IsVisible = false;
                CreateButton.IsVisible = true;

            }
            else
            {
                pick_type.IsVisible = false;
                CategoryButton.IsVisible = true;
                CustomButton.IsVisible = true;
                CreateButton.IsVisible = false;
            }

        }

        private void CategoryButton_Tapped(object sender, EventArgs e)
        {
            GameLogic.Logic.GetGameType("Category");
            GameLogic.Logic.GetTokGroup("");
            if (pick_category.IsVisible == false && TokGroupButton.IsVisible == true && CreateButton.IsVisible == false && CustomButton.IsVisible == true)
            {
                pick_category.IsVisible = true;
                TokGroupButton.IsVisible = false;
               CreateButton.IsVisible = true;
               CustomButton.IsVisible = false;
            }
            else
            {
                pick_category.IsVisible = false;
                TokGroupButton.IsVisible = true;
                CreateButton.IsVisible = false;
                CustomButton.IsVisible = true;

            }
        }

        private void CustomButton_Tapped(object sender, EventArgs e)
        {
           

          
        }
        private void CreateButton_Tapped(object sender, EventArgs e)
        {
            if (GameLogic.Logic.SelectedGametype() == "TokGroup")
            {
                GameLogic.Logic.GetTokGroup(pick_type.SelectedItem.ToString());
               // DisplayAlert("Choosen Tok Group", pick_type.SelectedItem.ToString(), "Ok");
            }
            else {
                GameLogic.Logic.GetCategory(pick_category.SelectedItem.ToString());

            }
                var dif_page = new NavigationPage(new GameMode());

            Application.Current.MainPage = dif_page;

        }
    }
}