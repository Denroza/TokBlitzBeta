using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Lottie.Forms;
using TokBlitzBeta.GamePlay;
using Plugin.Connectivity;
using Plugin.SimpleAudioPlayer;
using System.Timers;

using Microsoft.AspNetCore.SignalR.Client;
using TokBlitzBeta.Model;

namespace TokBlitzBeta
{
    public partial class MainPage : ContentPage
    {

        bool isPlayer1;

        MainViewModel.MainViewModel mainv = new MainViewModel.MainViewModel();
        GamePlayer me = new GamePlayer();
        GameInfo gameInfo = new GameInfo();
        HubConnection connection;
        TokGamesApiClient apiClient = new TokGamesApiClient();
        TokketUser tokket = new TokketUser();

        public MainPage()
        {
            InitializeComponent();
            
            LoadMainForm();
            ShoppingSmash();
            SaveOrLoad.ALoadedGame(false);
            Game.DefaultSmash();
            PointSystem.ResetScore();
            if (CrossConnectivity.Current.IsConnected) { AutomaticLogIn(); }
            Flow.ResetRound();
            Flow.DidTheGameStart(false);
            BGMusics.Menu_BGM().Play();

        }

        int TapCounter = 0;
        public async void Play_Tapped(object sender, EventArgs e) {
            BGMusics.Clicking_Sound().Play();
            AccountCache();
            AutomaticLogIn();
            if (CrossConnectivity.Current.IsConnected && string.IsNullOrEmpty(Application.Current.Properties["UserEmail"].ToString()) && string.IsNullOrEmpty(Application.Current.Properties["UserPassword"].ToString()))
            {
                UserControl.IsVisible = true;
            }
            else {
                await Application.Current.MainPage.FadeTo(0, 300);
                var navPage = new NavigationPage(new GameType());
                Application.Current.MainPage = navPage;
                await Task.Delay(250);
                await navPage.FadeTo(0, 400);
                await navPage.FadeTo(1, 300);
            }

        }

        public void ShowSettings(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
            SettingsPopUp.IsVisible = true;
        }
        public void ShowShop(object sender, EventArgs e)
        {

            BGMusics.Clicking_Sound().Play();
            SettingsPopUp.IsVisible = false;
            ShopPopUp.IsVisible = true;
        }


        public void CloseScreen(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
            SettingsPopUp.IsVisible = false;
            ShopPopUp.IsVisible = false;
            PrivacyPopUp.IsVisible = false;
        }
        public void ShopNextClicked(object sender, EventArgs e) {
            BGMusics.Clicking_Sound().Play();
            if (ShopSmash.IsVisible)
            {
                ShopSmash.IsVisible = false;
                ShopOthers.IsVisible = true;
            }
            else {

                ShopSmash.IsVisible = true;
                ShopOthers.IsVisible = false;
            }
        }
        public void CloseUser(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
            UserControl.IsVisible = false;
        }
       

        public async Task StartOnlineFeed() {
            //  await apiClient.GetGameToksAsync(new GameToksQuery() { tok_group = "Quote", count = -1 });
            try
            {
                var data = await apiClient.GetGameToksAsync(values: null);
                while (data == null)
                {
                    data = await apiClient.GetGameToksAsync(values: null);
                }

            }
            catch (NullReferenceException) {
                LoadingBit.IsVisible = false;
                Fetch.IsRunning = false;
                await DisplayAlert("loading Failed", "Game Failed to load, Please check your internet connection", "Ok");
            }

        }
        public async void LogInAccount(object sender, EventArgs args) {
            BGMusics.Clicking_Sound().Play();
            string email = User.Text;
            string password = Password.Text;
            Loader.IsVisible = true;
            Fetching.IsRunning = true;
            try {
                var link = await apiClient.LoginEmailPasswordAsync(email, password);
                //  await DisplayAlert("Result", link.User.LocalId, "ok");

                var userdata = await apiClient.GetUserAsync(link.User.LocalId);
               // await DisplayAlert("Result", userdata.Id, "ok");
                Loader.IsVisible = false;
                Fetching.IsRunning = false;
                if (Application.Current.Properties.ContainsKey("UserLabel"))
                {
                    Application.Current.Properties["UserLabel"] = userdata.DisplayName;
                }
                else
                {
                    Application.Current.Properties.Add("UserLabel", userdata.DisplayName);
                }
                if (Application.Current.Properties.ContainsKey("UserPhoto"))
                {
                    Application.Current.Properties["UserPhoto"] = userdata.UserPhoto;
                }
                else
                {
                    Application.Current.Properties.Add("UserPhoto", userdata.UserPhoto);
                }
                if (Application.Current.Properties.ContainsKey("UserEmail"))
                {
                    Application.Current.Properties["UserEmail"] = email;
                }
                else
                {
                    Application.Current.Properties.Add("UserEmail", email);
                }
                if (Application.Current.Properties.ContainsKey("UserPassword"))
                {
                    Application.Current.Properties["UserPassword"] = password;
                }
                else
                {
                    Application.Current.Properties.Add("UserPassword", password);
                }
                if (Application.Current.Properties.ContainsKey("UserId"))
                {
                    Application.Current.Properties["UserId"] = Guid.NewGuid().ToString();
                }
                else
                {
                    Application.Current.Properties.Add("UserId", Guid.NewGuid().ToString());
                }
                if (Application.Current.Properties.ContainsKey("Id"))
                {
                    Application.Current.Properties["Id"] = userdata.Id;
                }
                else
                {
                    Application.Current.Properties.Add("Id", userdata.Id);
                }


                await Application.Current.SavePropertiesAsync();
                UserData.SetUserDataValues(Application.Current.Properties["UserLabel"].ToString(), Application.Current.Properties["UserPhoto"].ToString(), Application.Current.Properties["Id"].ToString());
                await DisplayAlert("Result", "Log in Successful", "ok");
                UserControl.IsVisible = false;
            }
            catch (Exception) {
                Loader.IsVisible = false;
                Fetching.IsRunning = false;
                await DisplayAlert("Alert", "Invalid Email or Password", "Ok");
            }

            //  var account = await apiClient.GetUserAsync(link.User.LocalId);

        }

        public void AccountCache() {
            if (!Application.Current.Properties.ContainsKey("UserEmail") || !Application.Current.Properties.ContainsKey("UserPassword"))
            {
                if (Application.Current.Properties.ContainsKey("UserEmail"))
                {
                    Application.Current.Properties["UserEmail"] = "";
                }
                else
                {
                    Application.Current.Properties.Add("UserEmail", "");
                }
                if (Application.Current.Properties.ContainsKey("UserPassword"))
                {
                    Application.Current.Properties["UserPassword"] = "";
                }
                else
                {
                    Application.Current.Properties.Add("UserPassword", "");
                }
                Application.Current.SavePropertiesAsync();
            }
            else {
                User.Text = Application.Current.Properties["UserEmail"].ToString();
                Password.Text = Application.Current.Properties["UserPassword"].ToString();
            }
        }

        /*    public void HomeTapped(object sender, EventArgs e)
            {
                BGMusics.LoadSounds();
                TheDifficulties.IsVisible = false;

                Modes.IsVisible = true;
            }
            */
        public async void AutomaticLogIn() {
            Loader.IsVisible = true;
            Fetching.IsRunning = true;
            AccountCache();
            string CacheEmail = Application.Current.Properties["UserEmail"].ToString();
            string CachePassword = Application.Current.Properties["UserPassword"].ToString();
            try {
                if (!String.IsNullOrWhiteSpace(CacheEmail) && !String.IsNullOrWhiteSpace(CachePassword)) {
                    var link = await apiClient.LoginEmailPasswordAsync(CacheEmail, CachePassword);
                    var userdata = await apiClient.GetUserAsync(link.User.LocalId);
                    Loader.IsVisible = false;
                    Fetching.IsRunning = false;
                    if (Application.Current.Properties.ContainsKey("UserLabel"))
                    {
                        Application.Current.Properties["UserLabel"] = userdata.DisplayName;

                    }
                    else
                    {
                        Application.Current.Properties.Add("UserLabel", userdata.DisplayName);
                    }
                    if (Application.Current.Properties.ContainsKey("UserPhoto"))
                    {
                        Application.Current.Properties["UserPhoto"] = userdata.UserPhoto;
                    }
                    else
                    {
                        Application.Current.Properties.Add("UserPhoto", userdata.UserPhoto);
                    }
                    if (Application.Current.Properties.ContainsKey("UserEmail"))
                    {
                        Application.Current.Properties["UserEmail"] = CacheEmail;
                    }
                    else
                    {
                        Application.Current.Properties.Add("UserEmail", CacheEmail);
                    }
                    if (Application.Current.Properties.ContainsKey("UserPassword"))
                    {
                        Application.Current.Properties["UserPassword"] = CachePassword;
                    }
                    else
                    {
                        Application.Current.Properties.Add("UserPassword", CachePassword);
                    }
                    if (Application.Current.Properties.ContainsKey("Id"))
                    {
                        Application.Current.Properties["Id"] = userdata.Id;
                    }
                    else
                    {
                        Application.Current.Properties.Add("Id", userdata.Id);
                    }
                    await Application.Current.SavePropertiesAsync();
                    UserData.SetUserDataValues(Application.Current.Properties["UserLabel"].ToString(), Application.Current.Properties["UserPhoto"].ToString(), Application.Current.Properties["Id"].ToString());

                }
            } catch (NullReferenceException) {
                await DisplayAlert("Error Alert", "Something happened on Automatic Login", "Ok");
            }
            Loader.IsVisible = false;
            Fetching.IsRunning = false;
        }


        public void CheckSounds(object sender, EventArgs _event){

         
            if (chk_sounds.IsVisible)
            {
                chk_sounds.IsVisible = false;
                BGMusics.EnableSoundEffect(false);
                BGMusics.SoundEffects();
             
            }
            else {
                chk_sounds.IsVisible = true;
                BGMusics.EnableSoundEffect(true);
                BGMusics.SoundEffects();

            }

        }

        public void CheckMusic(object sender, EventArgs _event)
        {
         
            
            if (chk_music.IsVisible)
            {
                chk_music.IsVisible = false;
                BGMusics.EnableBackgroundMusic(false);
                BGMusics.BackgroundEffects();
             
            }
            else
            {
                chk_music.IsVisible = true;
                BGMusics.EnableBackgroundMusic(true);
                BGMusics.BackgroundEffects();
                BGMusics.Menu_BGM().Play();
            }

        }



        void ShoppingSmash(){
            Pane1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.panel.png");
            Pane2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.panel.png");
            Pane3.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.panel.png");
            Pane4.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.panel.png");
            Pane5.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.panel.png");
            Pane6.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.panel.png");
            Pane7.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.panel.png");
            Pane8.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.panel.png");
            Smash1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.icon_smash.png");
            Smash2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.icon_smash.png");
            Smash3.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.icon_smash.png");
            Smash4.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.icon_smash.png");
            Buy1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.buy.png");
            Buy2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.buy.png");
            Buy3.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.buy.png");
            Buy4.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.buy.png");
            Buy5.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.buy.png");
            Buy6.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.buy.png");
            Buy7.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.buy.png");
            Buy8.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.buy.png");
            NoAds.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.noads.png");
            Book.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.educational.png");
            LeftArrow.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.al.png");
            RightArrow.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Shop.ar.png");
        }
        

        void LoadMainForm() {
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bg1.png");
            LoadingLogo.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.logo_tokblitz.png");
            LoadingLogo1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.logo_tokblitz.png");
            background1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bg1.png");
            Logo.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.logo_tokblitz.png");
            Shop.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.SS.shop1.png");
            // Home.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.home.png");

            Play.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.play.png");
            Settings.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.SS.settings1.png");
            Close.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.close.png");
            Close1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.close.png");
           // Close2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.close.png");
            CloseUsers.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.close.png");
            Login.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.l.png");
            SignUp.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.SU.png");
            PanelSetting.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.panel.png");
            PanelSetting1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.panel.png");
            PanelSetting2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.panel.png");
            //ViewWeb.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.panel.png");

            Blimp.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.blimp.png");
            City.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.building.png");
            Cloud.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.clouds.png");
            Plane.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.plane.png");
            box_music.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.checkbox.png");
            box_sounds.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.checkbox.png");
            chk_music.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.check.png");
            chk_sounds.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.check.png");
            chk_sounds.IsVisible = BGMusics.SoundEffectSet();
            chk_music.IsVisible = BGMusics.BackgroundMusicSet() ;

            Policy.Source = "https://tokkepedia.com/support";

            var tapGesture = new TapGestureRecognizer();
            var htapGesture = new TapGestureRecognizer();

            htapGesture.Tapped += (sender, _events) =>
            {
                BGMusics.Clicking_Sound().Play();
                if (!HTPPopUp.IsVisible)
                {
                    HTPPopUp.IsVisible = true;
                }
                else {
                    HTPPopUp.IsVisible = false;
                }
            };
            htp.GestureRecognizers.Add(htapGesture);
            HTPPopUp.GestureRecognizers.Add(htapGesture); 
            tapGesture.Tapped += (sender, _event) => {
                BGMusics.Clicking_Sound().Play();
                if (!PrivacyPopUp.IsVisible)
                {
                    PrivacyPopUp.IsVisible = true;
                }
                else {
                    PrivacyPopUp.IsVisible = false;
                }
            
            };
            List<String> page = new List<string>() { "Guess The Blank", "Spell The Word", "The Less guess, the better!", "Use Lighning Strikes as Hint" };

        //    var HowToPlay = new CarouselView() { ItemsSource = page};
         //   HTP.Children.Add(HowToPlay,1,1);
            policy.GestureRecognizers.Add(tapGesture);
            PrivacyPopUp.GestureRecognizers.Add(tapGesture);

            var BlimpAnimation = new Animation();
            var animationBlimpUp = new Animation(v => Blimp.TranslationY = v, 20, -20);
            var animationBlimpDown = new Animation(v=>Blimp.TranslationY = v ,-20,20);

            BlimpAnimation.Add(0,0.5,animationBlimpUp);
            BlimpAnimation.Add(0.5,1,animationBlimpDown);

            Animation animationCloud = new Animation(v => Cloud.TranslationX = v, -100, 1000);
            animationCloud.Commit(this, "SimpleCloud", 16, 100000, Easing.Linear, (v, c) => Cloud.TranslationX =0, () => true);
            BlimpAnimation.Commit(this,"BlimpAnimate",16,4000,Easing.Linear,(v,c) => Blimp.TranslationY = 20, ()=> true);
            //animationBlimp.Commit(this, "SimpleBlimp", 32, 10000, Easing.Linear, (v, c) => Blimp.TranslationX = 10, () => true);
            Animation animationPlane = new Animation(v => Plane.TranslationX = v, 100, -1000);
            animationPlane.Commit(this, "SimplePlane", 32, 10000, Easing.Linear, (v, c) => Plane.TranslationX = 100, () => true);
            #region menus
            //        L1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.1.png");
            //        L2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.2.png");
            //          L3.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.3.png");
            //          L4.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.4.png");
            //         SinglePlayer.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.SP.png");
            //         Multiplayer.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.MP.png");
            #endregion


        }

    }
}
