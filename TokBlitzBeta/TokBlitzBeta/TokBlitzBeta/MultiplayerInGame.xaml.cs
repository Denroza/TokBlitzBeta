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
    public partial class MultiplayerInGame : ContentPage
    {
        public MultiplayerInGame()
        {
            InitializeComponent();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bgSP.png");
            profile1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.profile.png");
            profile2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.profile.png");
            timer.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.timer.png");
            board.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.screen.png");
            round.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.screen.png");
        }
    }
}