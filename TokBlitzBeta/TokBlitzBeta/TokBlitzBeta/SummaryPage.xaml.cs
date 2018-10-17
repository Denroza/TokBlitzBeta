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
	public partial class SummaryPage : ContentPage
	{
		public SummaryPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {

            var navPage = new NavigationPage(new MainPage());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;

        }
    }
}