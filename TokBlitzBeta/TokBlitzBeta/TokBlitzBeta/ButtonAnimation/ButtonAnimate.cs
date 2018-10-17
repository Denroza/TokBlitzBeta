using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TokBlitzBeta.ButtonAnimation
{
    public class ButtonAnimate
    {
        public static async void MenuButtonAnimation(Button button) {
            await Task.Delay(200);
            await button.FadeTo(0, 250);
            await Task.Delay(200);
            await button.FadeTo(1, 250);
        }

        public static async void ButtonImageAnimation(Image image) {
            await Task.Delay(200);
            await image.FadeTo(0, 250);
            await Task.Delay(200);
            await image.FadeTo(1, 250);
        }
    
    }
}
