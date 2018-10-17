using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokBlitzBeta.GameLogic;
namespace TokBlitzBeta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WrongPage : ContentPage
	{
		public WrongPage ()
		{
			InitializeComponent ();
            Round.Text = Execution.CurrentRound().ToString();
            RoundTotal.Text = Execution.TotalRound().ToString();
            TurnChecker();
        }


        private void TurnChecker()
        {
            if (Logic.SetDifficulty().Equals("Easy"))
            {

                Execution.QuestionCountReset();
                Execution.DoNextStage(true);
            }
            else if (Logic.SetDifficulty().Equals("Moderate"))
            {
                if (Execution.QuestionCount() > 2)
                {
                    Execution.QuestionCountReset();
                    Execution.DoNextStage(true);
                }
                else
                {

                    Execution.DoNextStage(false);
                }

            }
            else
            {

                if (Execution.QuestionCount() > 3)
                {
                    Execution.QuestionCountReset();
                    Execution.DoNextStage(true);
                }
                else
                {

                    Execution.DoNextStage(false);
                }
            }
        }
            private async void Button_Clicked(object sender, EventArgs e)
        {

            //   var navPage = new NavigationPage(new SinglePlayerInGame());
            if (Execution.CurrentRound() < Execution.TotalRound())
            {
                var navPage = new NavigationPage(new Sample());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
            }
            else {

                var navPage = new NavigationPage(new SummaryPage());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
            }
        }
    }
}