using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokBlitzBeta.GamePlay;
using TokBlitzBeta.Model;

using Microsoft.AspNetCore.SignalR.Client;
using Plugin.Connectivity;
using Plugin.SimpleAudioPlayer;
namespace TokBlitzBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameType : ContentPage
    {
        bool isPlayer1;

        MainViewModel.MainViewModel mainv = new MainViewModel.MainViewModel();
        GamePlayer me = new GamePlayer();
        GameInfo gameInfo = new GameInfo();
        HubConnection connection;
        TokGamesApiClient apiClient = new TokGamesApiClient();
        TokketUser tokket = new TokketUser();
        public GameType()
        {
            InitializeComponent();
            LoadingLogo.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.logo_tokblitz.png");
            background1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bg1.png");
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.bg.png");
            Panel.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.panel.png");
            Home.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.home.png");
            Back.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.return.png");
            Sign.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Labels.type.png");
            //Qoutes.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.q.png");
            Variety.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.variety.png");
            Category.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.category.png");
            Multiplayer.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.MP.png");
            //    UserGenerated.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.UG.png");
            SavedGames.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.SG.png");

            S1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.FS.png");
            S2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.FS.png");
            S3.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.FS.png");
            Play.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.p.png");
            //=======================================//
            L1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.1.png");
            L2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.2.png");
            L3.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.3.png");
            L4.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.4.png");
            //=========================================//
            BuildCategory();
            LoadGames();
            // Sayings.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.s.png");
            if (!String.IsNullOrEmpty(UserData.GetUserLabel()) && !String.IsNullOrEmpty(UserData.GetUserProfileUrl()))
            {
                UserData.SetUserDataValues(Application.Current.Properties["UserLabel"].ToString(), Application.Current.Properties["UserPhoto"].ToString(), Application.Current.Properties["Id"].ToString());
            }
        }


        MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();
        int TapCounter = 0;
        int TapCounter1 = 0;
        int TapCounter2 = 0;
        public void Variety_Clicked(object sender, EventArgs e) {
            TapCounter++;
            BGMusics.Clicking_Sound().Play();
            if (TapCounter == 1)
            {
                Flow.GetPlayerMode(false);
                Flow.IsGameInCategory(false);
                SaveOrLoad.ALoadedGame(false);
                string[] listitems = { "" };
                Flow.GameSelection(1);

                // var data = LocalSelection.ConvertQuotesAndSayings();
                // LocalSelection.QuotesAndSayingsSelected();
                //  LoadingBit.IsVisible = true;
                // Fetch.IsRunning = true;



                Menus.IsVisible = false;
                DifficutyMenu.IsVisible = true;


            }
        }

        public async void BackTapped(object sender, EventArgs e) {

            TapCounter = 0;
            TapCounter1 = 0;
            TapCounter2 = 0;
            BGMusics.Clicking_Sound().Play();
            if (Menus.IsVisible && DifficutyMenu.IsVisible == false && CategorySection.IsVisible == false && CategorySectionSelected.IsVisible == false)
            {
                var navPage = new NavigationPage(new MainPage());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
            }
            else {
                Menus.IsVisible = true;
                DifficutyMenu.IsVisible = false;
                CategorySection.IsVisible = false;
                CategorySectionSelected.IsVisible = false;
              Slots.IsVisible = false;
            }

        }
        public async void HomeTapped(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
            var navPage = new NavigationPage(new MainPage());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;

        }
        public async void CategoryTapped(object sender, EventArgs e)
        {
            Flow.IsGameInCategory(true);
            BGMusics.Clicking_Sound().Play();
            SaveOrLoad.ALoadedGame(false);
            Menus.IsVisible = false;
            DifficutyMenu.IsVisible = false;
            CategorySection.IsVisible = true;

        }
        public void LoadSaveData(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
            SaveOrLoad.ALoadedGame(true);
            Menus.IsVisible = false;
            Slots.IsVisible = true;


        }



        public async void L1Tapped(object sender, EventArgs e)
        {
            TapCounter1++;
            if (TapCounter1 == 1)
            {
                BGMusics.Clicking_Sound().Play();
                BGMusics.Menu_BGM().Stop();
                BGMusics.InGame_BGM().Play();
                Flow.StarSetter(1);
                Flow.IsGameInCategory(false);

                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
                BGMusics.Loading_Sound().Play();
                LocalSelection.ConvertQuotesAndSayings();
             
                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
                L1.IsEnabled = false;
                
                var navPage = new NavigationPage(new Blank1());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
                

            }
        }
        public async void L2Tapped(object sender, EventArgs e)
        {
            TapCounter1++;
            if (TapCounter1 == 1)
            {
                BGMusics.Clicking_Sound().Play();
                BGMusics.Menu_BGM().Stop();
                BGMusics.InGame_BGM().Play();
                Flow.StarSetter(2);
                Flow.IsGameInCategory(false);
                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;

                BGMusics.Loading_Sound().Play();
                LocalSelection.ConvertQuotesAndSayings();
                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
                L2.IsEnabled = false;
                var navPage = new NavigationPage(new Blank1());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
            }
        }
        public async void L3Tapped(object sender, EventArgs e)
        {
            TapCounter1++;
            if (TapCounter1 == 1)
            {
                BGMusics.Clicking_Sound().Play();
                BGMusics.Menu_BGM().Stop();
                BGMusics.InGame_BGM().Play();
                Flow.StarSetter(3);
                Flow.IsGameInCategory(false);
                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
                BGMusics.Loading_Sound().Play();
                LocalSelection.ConvertQuotesAndSayings();

                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
                L3.IsEnabled = false;
                var navPage = new NavigationPage(new Blank1());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
            }
        }
        public async void L4Tapped(object sender, EventArgs e)
        {
            TapCounter1++;
            if (TapCounter1 == 1)
            {
                BGMusics.Clicking_Sound().Play();
                BGMusics.Menu_BGM().Stop();
                BGMusics.InGame_BGM().Play();
                Flow.StarSetter(4);
                Flow.IsGameInCategory(false);
                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
                BGMusics.Loading_Sound().Play();
                LocalSelection.ConvertQuotesAndSayings();
                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
                L4.IsEnabled = false;
                var navPage = new NavigationPage(new Blank1());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
            }
        }

        #region Multiplayer Code
        public async void Multiplayer_Tapped(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
            BGMusics.Menu_BGM().Stop();
            BGMusics.InGame_BGM().Play();
            Flow.GetPlayerMode(true);
            SaveOrLoad.ALoadedGame(false);
            if (TapCounter > 1)
                return;
            LoadingBit.IsVisible = true;
            Fetch.IsRunning = true;
            string CacheEmail = Application.Current.Properties["UserEmail"].ToString();
            string CachePassword = Application.Current.Properties["UserPassword"].ToString();
            try
            {
                if (string.IsNullOrEmpty(CacheEmail) || string.IsNullOrEmpty(CachePassword))
                {
                    await DisplayAlert("No Account", "Please Log in", "OK");
                    LoadingBit.IsVisible = false;
                    Fetch.IsRunning = false;
                    return;
                }
                var link = await apiClient.LoginEmailPasswordAsync(CacheEmail, CachePassword);
                var userdata = await apiClient.GetUserAsync(link.User.LocalId);
            }
            catch (Exception ex)
            {
                LoadingBit.IsVisible = false;
                Fetch.IsRunning = false;
                await DisplayAlert("No Account", "Please Log in", "OK");
                return;
            }
            if (string.IsNullOrEmpty(UserData.GetUserLabel()))
            {
                LoadingBit.IsVisible = false;
                Fetch.IsRunning = false;
                await DisplayAlert("No Account", "Please Log in", "OK");
                return;
            }

            try
            {
                me.UserName = UserData.GetUserLabel()?.Trim();

                me.UserId = Application.Current.Properties["UserId"].ToString();


                lblMessages.Text = "Getting connection info...";

                SignalRConnectionInfo connectionInfo = await SignalRService.GetSignalRConnectionInfo();
                var url = connectionInfo.Url;
                lblMessages.Text = $"Connecting...";

                connection = new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(connectionInfo.AccessToken);
                })
                .Build();

                //connection.Closed += async (error) =>
                //{
                //    //Optional code: only used for retrying to connect
                //    //await Task.Delay(new Random().Next(0, 5) * 1000);
                //    //await connection.StartAsync();
                //};
                #region Connection
                //Waiting for other player
                connection.On<GamePlayer>("waiting", (player) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (player.UserId == me.UserId)
                        {

                            Fetch.IsRunning = true;
                            lblMessages.Text = "Waiting for players...";

                        }
                    });
                });

                //2 player game successfully created (method name MUST be 'newgame'
                connection.On<GameInfo>("newgame", (info) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {

                        gameInfo = info;
                        var currentPlayer = info.Players.Find(x => x.UserId == me.UserId);
                        if (currentPlayer.PlayerNumber == 1)
                        {
                            isPlayer1 = true;
                            ///      btnStartTimer.IsEnabled = true;
                        }
                        else
                        {
                            isPlayer1 = false;
                            //         btnStartTimer.IsEnabled = false;
                        }

                        MultiPlayerSettings.IsThisPlayer1(isPlayer1);
                        //Fill label with game content
                        //      lblGameContent.Text = $"Room Id: {info.RoomId}{Environment.NewLine}Player 1: {info.Players[0].UserName}{Environment.NewLine}Player 2: {info.Players[1].UserName}{Environment.NewLine}Toks:{Environment.NewLine}";
                        lblMessages.Text = $"Room Id: {info.RoomId}{Environment.NewLine}Player 1: {info.Players[0].UserName}{Environment.NewLine}Player 2: {info.Players[1].UserName}{Environment.NewLine}Toks:{Environment.NewLine}";
                        if (MultiPlayerSettings.YupThisIsPlayer1())
                        {
                            MultiPlayerSettings.OtherPlayersData(info.Players[1].UserName, info.Players[1].UserPhoto);
                        }
                        else
                        {
                            MultiPlayerSettings.OtherPlayersData(info.Players[0].UserName, info.Players[0].UserPhoto);
                        }
                        for (int i = 0; i < info.Toks.Count; ++i)
                        {
                            mainv.OnlineQoutes.Add(new OnlineQoutes()
                            {
                                QouteID = info.Toks[i].Id,
                                QoutePhrase = info.Toks[i].PrimaryFieldText,
                                QoutesSource = info.Toks[i].SecondaryFieldText,
                                Category = info.Toks[i].Category
                            });
                        }
                        MultiPlayerSettings.GetGameInfo(gameInfo);
                        Selection.GetOnlineList(mainv.OnlineQoutes);
                        var navPage = new NavigationPage(new Blank2());
                        Application.Current.MainPage = navPage;

                    });
                });
                #endregion

                BGMusics.Loading_Sound().Play();
                await connection.StartAsync();
                Fetch.IsRunning = false;
                lblMessages.Text = $"Connected.";
                await SignalRService.MultiplayerSetup(me);
                Btn_Find.IsVisible = true;
            }
            catch (Exception ex)
            {
                //        lblMessages.Text = ex.Message;
                await DisplayAlert("Error", ex.ToString(), "Ok");
            }


            /*
            var navPage = new NavigationPage(new Multiplayer());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;

        */

        }
        #endregion

        private async void BtnStopWaiting_Clicked(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();

            lblMessages.Text = "Stopping waiting...";
            await SignalRService.MultiplayerStopWaiting(me); ;
            LoadingBit.IsVisible = false;
            Fetch.IsRunning = false;
            Btn_Find.IsVisible = false;
        }

        public async Task StartOnlineFeed()
        {
            //  await apiClient.GetGameToksAsync(new GameToksQuery() { tok_group = "Quote", count = -1 });
            try
            {
                var data = await apiClient.GetGameToksAsync(values: null);
                while (data == null)
                {
                    data = await apiClient.GetGameToksAsync(values: null);
                }

            }
            catch (NullReferenceException)
            {
                LoadingBit.IsVisible = false;
                Fetch.IsRunning = false;
                await DisplayAlert("loading Failed", "Game Failed to load, Please check your internet connection", "Ok");
            }

        }

        public async void Play_Clicked(object sender, EventArgs eventArgs) {
            TapCounter2++;
            if (TapCounter2 == 1)
            {
                BGMusics.Clicking_Sound().Play();
                BGMusics.Menu_BGM().Stop();
                BGMusics.InGame_BGM().Play();

                SaveOrLoad.ALoadedGame(false);
                CategoryLoad.SaveThisNow(CategoryName.Text);
                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
                BGMusics.Loading_Sound().Play();
                


            

                LoadingBit.IsVisible = true;
                Fetch.IsRunning = true;
               
                var navPage = new NavigationPage(new Blank1());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;

                /*
                 * 
                 *     for (int k = 0; k < 5; k++)
                {
                   await DisplayAlert("Result", CategoryLoad.LoadSelectedToks()[k].ToString(), "OK");
                }
            await DisplayAlert("Result", CategoryLoad.LoadSelectedToks()[0].ToString(), "OK");
         await DisplayAlert("Result", Selection.ThisIsThePhrase(), "OK");

                
                    
              */
            }
        }


        void BuildCategory() {
            Image[,] image = new Image[10, 3];
            string[] names = {"Ability","Adversity","Aging","Attitude","Courage","Commitment",
                    "Creativity","Education","Faith","Family","Friendship","God","Goverment","Health","Humor","Kindness",
                    "Leadership","Love","Money","Opportunity","Patience","Perseverance","Peace","Politics","Quality",
                    "Service","Success","Time","Wisdom","Work"};
            Label[,] labels = new Label[10, 3];
            TapGestureRecognizer[,] recognizers = new TapGestureRecognizer[10, 3];
            for (int i = 0; i < 3; i++) {
                CategorySection.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            }
            for (int i = 0; i < 10; i++) {
                CategorySection.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            int pos = 0;
            for (int row = 0; row < 10; row++) {
                for (int coloumn = 0; coloumn < 3; coloumn++) {

                    image[row, coloumn] = new Image { Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.BlackButton.png"),
                        BackgroundColor = Color.Transparent, };
                    labels[row, coloumn] = new Label { Text = names[pos], FontFamily = "Face Off M54.ttf#Face Off M54", TextColor = Color.White, BackgroundColor = Color.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
                    int i = pos;
                    recognizers[row, coloumn] = new TapGestureRecognizer();
                    recognizers[row, coloumn].Tapped += (sender, e) => {
                        BGMusics.Clicking_Sound().Play();

                        CategoryLoad.LoadCategoryBy(names[i]);
                        CategoryName.Text = names[i];
                        TotalToks.Text = "Total Toks: " + CategoryLoad.SetTotalToks();
                        ToksLeft.Text = "Toks Left: " + CategoryLoad.SetToksLeft();
                        RankSetter(names[i]);
                        CategorySection.IsVisible = false;
                        CategorySectionSelected.IsVisible = true;

                    };
                    labels[row, coloumn].GestureRecognizers.Add(recognizers[row, coloumn]);
                    image[row, coloumn].GestureRecognizers.Add(recognizers[row, coloumn]);
                    CategorySection.Children.Add(image[row, coloumn], coloumn, row);
                    CategorySection.Children.Add(labels[row, coloumn], coloumn, row);
                    pos++;
                }
            }
        }

        public void Check_Clicked(object sender, EventArgs args) {
            if (Application.Current.Properties.ContainsKey(CategoryName.Text.ToUpper()))
            {
                DisplayAlert("Result", Application.Current.Properties[CategoryName.Text.ToUpper()].ToString(), "OK");

            }
            else {
                DisplayAlert("Result", "NONE", "OK");
            }
           
        }

        void RankSetter(string category)
        {

            if (Application.Current.Properties.ContainsKey(category.ToLower()))
            {
                Application.Current.Properties[category.ToLower()] = string.Empty;

            }
            else {

                Application.Current.Properties.Add(category.ToLower(), string.Empty);
            }
            Application.Current.SavePropertiesAsync();
            string CheckCategory = Application.Current.Properties[category.ToLower()].ToString();

            if (string.IsNullOrEmpty(CheckCategory))
            {
                if (CategoryLoad.SetWhatPart() == "PART 1")
                {

                    S1.IsVisible = false;
                    S2.IsVisible = true;
                    S3.IsVisible = false;
                    TheTitle.Text = "AIR MAJOR";
                }
                if (CategoryLoad.SetWhatPart() == "PART 2")
                {
                    S1.IsVisible = true;
                    S2.IsVisible = false;
                    S3.IsVisible = true;
                    TheTitle.Text = "MASTER AIR MAJOR";
                }
                if (CategoryLoad.SetWhatPart() == "PART 3")
                {

                    S1.IsVisible = true;
                    S2.IsVisible = true;
                    S3.IsVisible = true;
                    TheTitle.Text = "SENIOR AIR MAJOR";
                    Application.Current.Properties.Add(category, CategoryLoad.SetWhatPart());
                  
                }
            }
            else
            {
                S1.IsVisible = true;
                S2.IsVisible = true;
                S3.IsVisible = true;
                TheTitle.Text = "SENIOR AIR MAJOR";
            }
            Application.Current.SavePropertiesAsync();
        }

        void LoadGames(){

            int slotsAvailbale = 3;
            Image[] SlotHolder = new Image[slotsAvailbale];
            Label[] SlotName = new Label[slotsAvailbale];
            string[] text = new string[slotsAvailbale];
            string[] setting = new string[slotsAvailbale];
            string[] theValues = new string[slotsAvailbale];
            int[] star = new int[slotsAvailbale];
            TapGestureRecognizer[] recognizer = new TapGestureRecognizer[slotsAvailbale];
            for (int i=0;i<slotsAvailbale;i++) {
                SaveSlots.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1,GridUnitType.Star) });
            }

            for (int i=0;i<slotsAvailbale;i++) {
                SaveOrLoad.LoadGameInSelectedSlot("SD"+(i+1));
                if (string.IsNullOrEmpty(SaveOrLoad.SetLoadedDataValues()))
                {
                    text[i] = "Saved Game " + (i + 1);
                }
                else {
                    if (SaveOrLoad.SetLoadedGameType() == "Variety")
                    {
                        switch (SaveOrLoad.SetLoadedSettings())
                        {
                            case "1": setting[i] = "Easy"; star[i] = 1; break;
                            case "2": setting[i] = "Moderate"; star[i] = 2; break;
                            case "3": setting[i] = "Challenging" ; star[i] = 3; break;
                            case "4": setting[i] = "Difficult"; star[i] = 4; break;
                        }
                    }
                    else {
                        setting[i] = SaveOrLoad.SetLoadedSettings();
                    }
                    theValues[i] = SaveOrLoad.SetLoadedDataValues();
                    text[i] = SaveOrLoad.SetSaveName() + Environment.NewLine + SaveOrLoad.SetLoadedGameType() + Environment.NewLine + setting[i];
                }
                SlotHolder[i] = new Image {  Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.BlackButton.png"),
                    BackgroundColor = Color.Transparent, Aspect = Aspect.AspectFit
                };
                SlotName[i] = new Label { Text = text[i], FontFamily = "Face Off M54.ttf#Face Off M54", FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label)) , TextColor = Color.White, BackgroundColor = Color.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
                int mark = i;
                recognizer[i] = new TapGestureRecognizer();
                recognizer[i].Tapped += async (sender, args) =>
                {
                    BGMusics.Clicking_Sound().Play();
                    if (!string.IsNullOrEmpty(theValues[mark]))
                    {
                        if (!Flow.GameInCategory())
                        {
                            Flow.StarSetter(star[mark]);
                            Flow.IsGameInCategory(false);
                            LocalSelection.ConvertQuotesAndSayings();
                            SaveOrLoad.GetPhrases(theValues[mark].Split('-'));
                            var navPage = new NavigationPage(new Blank1());
                            await Task.Delay(200);
                            await navPage.FadeTo(0, 250);
                            await Task.Delay(200);
                            await navPage.FadeTo(1, 250);
                            Application.Current.MainPage = navPage;

                        }
                        else
                        {
                            Flow.IsGameInCategory(true);
                            LocalSelection.ConvertQuotesAndSayings();
                            SaveOrLoad.GetPhrases(theValues[mark].Split('-'));
                            var navPage = new NavigationPage(new Blank1());
                            await Task.Delay(200);
                            await navPage.FadeTo(0, 250);
                            await Task.Delay(200);
                            await navPage.FadeTo(1, 250);
                            Application.Current.MainPage = navPage;
                        }
                    }
                    else {
                        await DisplayAlert("Warning!","No Save Game Found","OK");
                    }
                    
                };
                SlotName[i].GestureRecognizers.Add(recognizer[i]);
                SaveSlots.Children.Add(SlotHolder[i],0,i);
                SaveSlots.Children.Add(SlotName[i],0,i);
            }



        }
        

    }
}