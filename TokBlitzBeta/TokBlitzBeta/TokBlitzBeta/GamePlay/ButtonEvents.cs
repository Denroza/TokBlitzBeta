using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TokBlitzBeta.GamePlay
{
    class ButtonEvents
    {
        public static async void BactToHomePage() {

            var navPage = new NavigationPage(new MainPage());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;

        }
        public static async void BactToGameType()
        {

            var navPage = new NavigationPage(new GameType());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;

        }
    }
}
