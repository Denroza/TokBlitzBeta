using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokBlitzBeta.GamePlay;
using System.Timers;
using Lottie.Forms;
using Plugin.Connectivity;
using Plugin.SimpleAudioPlayer;
using Microsoft.AspNetCore.SignalR.Client;

namespace TokBlitzBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Blank2 : ContentPage
    {
        Timer GameTimer;
        bool isPlayer1;
        bool GameIsTimed = true, TimerIsRunning, TimerP1IsVisible, TimerP2IsVisible, onlyAllowRoomId = false;
        int roundNumP1 = 1, guessNumP1 = 1, roundNumP2 = 1, guessNumP2 = 1, scoreNumP1 = 0,scoreNumP2 =0, timeCount=1;
        MainViewModel.MainViewModel mainv = new MainViewModel.MainViewModel();
        GamePlayer me = new GamePlayer();
        GameInfo gameInfo = MultiPlayerSettings.ReadGameInfo();
        HubConnection connection;
        Label[,] labeled;
        string[] AllWords;
        Label[,] BoardLabel = new Label[10, 5];
        Frame[,] frames = new Frame[10, 5];
        Frame[,] frameschoice;
        Image[,] framed;
        Image[,] FrameWord = new Image[10, 5];
        Image[] FramedAnswers;
        string[] TheAnswers;
        TapGestureRecognizer[,] gridTap;
        TapGestureRecognizer[] AnswerTap;
        Label[] AnswerLabel;
        char[] lettersdivide;
        string reserveletters;
        int here = 1;
        int inhere = 1;
        int mistakepoint = 0;
        int currentcount = 0;
        int GuesssCount = 1;
        int GuesssCount1 = 1;
        bool TimeIsUp = true;
        int time = 1;
        bool TimeFreezed = false;
        bool IsCorrect = false;
        int TimeDeduct = 0;
        string ChosenPhrase;
        bool ToNextRound = false;
        public Blank2()
        {

            InitializeComponent();
            if (Flow.SetPlayerMode())
            {
                player2.IsVisible = true;
              if (!Flow.YesItStartedAlready()) {
                 //   TimerOn();
                    Flow.DidTheGameStart(true);
              }
                GameConnect();
          //  SetTimer();

            }
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.InGame.png");
            Panel.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.panelhead.png");
            Shop.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.inShop.png");
            Home.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.home.png");
            Smash.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.smash.png");
            Clock.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.clock.png");
            Footer.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Animated.Parts.footer.png");
            Part1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.part1.png");
            Part2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.part2.png");
            Part3.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.part2.png");
            Part1_1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.part1.png");
            Part2_1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.part2.png");
            Part3_1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.part2.png");
            SmashNumber.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.circmon.png");
            PanelSetting.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.panel.png");
            ContinueButton.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.conti.png");
            ContinueButton1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.exit.png");
            Player1Name.Text = UserData.GetUserLabel();
            Player2Name.Text = MultiPlayerSettings.ShowOtherPlayerName();
            if (MultiPlayerSettings.YupThisIsPlayer1())
            {
                if (String.IsNullOrEmpty(UserData.GetUserProfileUrl()))
                {
                    ProPic1.Source = ImageSource.FromUri(new Uri("http://polrestabessurabaya.com/images/personil/2e49b55fbe7a196f05b4b3c031253210.jpg"));
                }
                else
                {
                    ProPic1.Source = ImageSource.FromUri(new Uri(UserData.GetUserProfileUrl()));
                }
            }
            else {
                if (String.IsNullOrEmpty(MultiPlayerSettings.ShowOtherPlayerPicture()))
                {
                    ProPic2.Source = ImageSource.FromUri(new Uri("http://polrestabessurabaya.com/images/personil/2e49b55fbe7a196f05b4b3c031253210.jpg"));
                }
                else
                {
                    ProPic2.Source = ImageSource.FromUri(new Uri(MultiPlayerSettings.ShowOtherPlayerPicture()));
                }
            }
            
            


            TotalScore.TextColor = Color.Goldenrod;
            TotalScore.FontSize = 16;
            TotalScore2.TextColor = Color.Goldenrod;
            TotalScore2.FontSize = 16;
            PopulateGameBoard();
            PopulateAnswer();

            AnswerBank();

            

               

                if (MultiPlayerSettings.YupThisIsPlayer1())
                {
                    round1.Text = "Round: " + (Flow.CurrentRound()+1);
                    guess1.Text = "Guess: " + guessNumP1;
                    TotalScore.Text = scoreNumP1.ToString();
                }
                else
                {
                    round1.Text = "Round: " + (Flow.CurrentRound1() + 1);
                    guess1.Text = "Guess: " + guessNumP2;
                    TotalScore.Text = scoreNumP2.ToString();

                }




                SmashCount.Text = Game.SmashCounter().ToString();


            
            //Multiplayer
            // TotalScore2.Text = "0";

            if (!MultiPlayerSettings.YupThisIsPlayer1())
            {
                round2.Text = "Round: " +roundNumP2;
                guess2.Text = "Guess: " + guessNumP2 ;
                TotalScore2.Text = scoreNumP2.ToString();
            }
            else
            {
                round2.Text = "Round: " + roundNumP1;
                guess2.Text = "Guess: " + guessNumP1;
                TotalScore2.Text = scoreNumP1.ToString();
            }
         
          
            //

            QouteCategory.Text = Selection.ShowCategory().ToUpper();
            QouteSource.Text = Selection.ShowSource();
            SmashCount.TextColor = Color.DarkRed;
            SmashCount.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            TheTimer.TextColor = Color.DarkRed;
            TheTimer.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            ResultScore.FontFamily = "Face Off M54.ttf#Face Off M54";
            Player1Name.FontFamily = "Face Off M54.ttf#Face Off M54";
            Player2Name.FontFamily = "Face Off M54.ttf#Face Off M54";
            ResultText.FontFamily = "Face Off M54.ttf#Face Off M54";
            QouteCategory.FontFamily = "Face Off M54.ttf#Face Off M54";
            QouteSource.FontFamily = "Face Off M54.ttf#Face Off M54";

            // Flow.GetRound(Flow.CurrentRound());
            TimerOn();

            //  SaveOrLoad.ALoadedGame(false);
            Flow.GameStarting(true);


        }

        //========================================================//
        public void PopulateGameBoard()
        {
            Game game = new Game();
            TokGamesApiClient apiClient = new TokGamesApiClient();
            Selection selection = new Selection();
            Selection.PhraseSelectionInitiate();

            Random random = new Random();
          
            
                AllWords = Selection.SelectedPhrase();
            
            //====//
            if (CrossConnectivity.Current.IsConnected)
            {


                    ChosenPhrase = Selection.SetOnlineQoutes()[Flow.CurrentRound()].QoutePhrase;
                    Selection.GetOnlineID(Selection.SetOnlineQoutes()[Flow.CurrentRound()].QouteID);

                string[] splitter = { " ", "/", "...", "-",";",":" };
                AllWords = ChosenPhrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

            }


            //====//

            string[] WordsWithBlank = Game.SetBlanks(AllWords);

            Creation.InitalizeRowAndColoumn(AllWords);
            //===//

            //====//
            int wordcount = WordsWithBlank.Count();
            int setrow = Creation.SetRow();
            int setcoloumn = Creation.SetColoumn(); ;
            int NumberofChar = Selection.PhraseMaximumCharacter();
            var SetFontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
            if (WordsWithBlank.Count() < 25)
            {
                if (NumberofChar < 7)
                {
                    SetFontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
                }
                else if (NumberofChar > 6 && NumberofChar < 13)
                {
                    SetFontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
                }
                else
                {
                    SetFontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
                }
            }
            else
            {
                if (NumberofChar < 7)
                {
                    SetFontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                }
                else if (NumberofChar > 6 && NumberofChar < 11)
                {
                    SetFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                }
                else if (NumberofChar > 10 && NumberofChar < 14)
                {
                    SetFontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                }
                else
                {
                    SetFontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
                }

            }

            // if (wordcount > 1 && wordcount < 33)
            //  {




            int numrow = 0;
            int addrow = 0;
            ///
            if (wordcount < 4)
            {
                numrow = 1;
                addrow = 2;
                for (int row = 0; row < setrow + 2; row++)
                {
                    if (!row.Equals(1) || !row.Equals(setrow + 2))
                    {

                        GameBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Auto) });

                    }
                    else
                    {

                        GameBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    }

                }
                for (int coloumn = 0; coloumn < setcoloumn; coloumn++)
                {
                    GameBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }
            }

            else if (wordcount > 3 && wordcount < 19)
            {
                for (int row = 0; row < setrow; row++)
                {


                    GameBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });




                }
                for (int coloumn = 0; coloumn < setcoloumn; coloumn++)
                {
                    GameBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }


            }
            else
            {

                for (int row = 0; row < setrow; row++)
                {


                    GameBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });




                }
                for (int coloumn = 0; coloumn < setcoloumn; coloumn++)
                {
                    GameBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }
            }




            for (int row = numrow; row < setrow + addrow; row++)
            {
                for (int coloumn = 0; coloumn < setcoloumn; coloumn++)
                {
                    if (wordcount > currentcount)
                    {
                        if (!WordsWithBlank[currentcount].Equals(""))
                        {


                            FrameWord[row, coloumn] = new Image
                            {
                                Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.wordframe.png"),
                                Aspect = Aspect.AspectFill
                            };

                            BoardLabel[row, coloumn] = new Label
                            {
                                Text = WordsWithBlank[currentcount],
                                VerticalTextAlignment = TextAlignment.Center,

                                // BackgroundColor = Color.FromRgb(138, 138, 138),
                                FontSize = SetFontSize,
                                HorizontalTextAlignment = TextAlignment.Center,

                            };
                            frames[row, coloumn] = new Frame
                            {

                                BackgroundColor = Color.Transparent,


                            };


                        }
                        else
                        {

                            FrameWord[row, coloumn] = new Image
                            {
                                Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.blank.png"),
                                VerticalOptions = LayoutOptions.EndAndExpand,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Aspect = Aspect.AspectFit
                            };


                            BoardLabel[row, coloumn] = new Label
                            {
                                Text = "______",
                                VerticalTextAlignment = TextAlignment.Center,
                                TextColor = Color.Black,
                                HorizontalTextAlignment = TextAlignment.Center,
                                IsVisible = false
                            };

                            frames[row, coloumn] = new Frame
                            {
                                BackgroundColor = Color.Transparent,
                                BorderColor = Color.Transparent
                            };
                        }


                        currentcount++;

                    }
                    else
                    {


                        FrameWord[row, coloumn] = new Image
                        {
                            Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.blankn.png"),
                            Aspect = Aspect.AspectFill
                        };

                        BoardLabel[row, coloumn] = new Label
                        {
                            Text = "______",

                            VerticalTextAlignment = TextAlignment.End,
                            HorizontalTextAlignment = TextAlignment.Center,
                            IsVisible = false
                        };

                        frames[row, coloumn] = new Frame
                        {
                            BackgroundColor = Color.Transparent,
                            BorderColor = Color.Transparent
                        };

                    }
                    BoardLabel[row, coloumn].FontFamily = "Face Off M54.ttf#Face Off M54";
                    BoardLabel[row, coloumn].TextColor = Color.Black;
                    GameBoard.Children.Add(FrameWord[row, coloumn], coloumn, row);
                    GameBoard.Children.Add(frames[row, coloumn], coloumn, row);
                    GameBoard.Children.Add(BoardLabel[row, coloumn], coloumn, row);



                }
            }


        }
        //========================================================//
        public void PopulateAnswer()
        {

            Game game = new Game();
            var children = answer.Children.ToList();
            answer.ColumnDefinitions.Clear();
            answer.RowDefinitions.Clear();
            foreach (var childs in children)
            {
                answer.Children.Remove(childs);
            }

            TheAnswers = Game.SetToBoardLetters();
            string Firstword = Game.SetFirstWord();
           

            AnswerLabel = new Label[Firstword.Length];
            FramedAnswers = new Image[Firstword.Length];

            answer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (int NumberOfGrid = 0; NumberOfGrid < Firstword.Length; NumberOfGrid++)
            {

                answer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                if (NumberOfGrid.Equals(0))
                {

                    if (Firstword.ToCharArray().Count().Equals(0))
                    {
                        FramedAnswers[NumberOfGrid] = new Image
                        {
                            Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png"),
                        };
                        AnswerLabel[NumberOfGrid] = new Label
                        {
                            Text = " ",
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            TextColor = Color.GhostWhite

                        };

                    }
                    else
                    {
                        FramedAnswers[NumberOfGrid] = new Image
                        {
                            Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fws.png"),
                        };
                        AnswerLabel[NumberOfGrid] = new Label
                        {
                            Text = Firstword.ToCharArray()[0].ToString().ToUpper(),

                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            TextColor = Color.GhostWhite

                        };


                    }
                }
                else
                {
                    FramedAnswers[NumberOfGrid] = new Image
                    {
                        Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png"),
                    };
                    AnswerLabel[NumberOfGrid] = new Label
                    {
                        Text = " ",
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.GhostWhite

                    };

                }


                AnswerLabel[NumberOfGrid].FontFamily = "ProximaNovaCond-Xbold.otf#ProximaNovaCond-Xbold";
                AnswerLabel[NumberOfGrid].FontSize = 30;
                answer.Children.Add(FramedAnswers[NumberOfGrid], NumberOfGrid, 0);
                answer.Children.Add(AnswerLabel[NumberOfGrid], NumberOfGrid, 0);
            }
        }
        //========================================================//
        public void AnswerBank()
        {

            here = inhere;
            int difficulty = Flow.DifficultyIdentifier();
            var children = answer.Children.ToList();
            var orphans = choices.Children.ToList();
            Random rand = new Random();
            Creation.AnswerBankLoader();
            string Firstword = Game.SetFirstWord();
            if (Firstword.Equals(null))
            {
                var navPage = new NavigationPage(new GameType());
                Application.Current.MainPage = navPage;
            }

            PointSystem.GuessCounter(Firstword.Length);
            int InitializedRow = Creation.AnswerBankRow();
            if (difficulty < 3)
            {
                InitializedRow = 1;
            }
            int InitializedColoumn = Creation.AnswerBankColoumn();
            labeled = new Label[InitializedRow, InitializedColoumn];
            frameschoice = new Frame[InitializedRow, InitializedColoumn];
            string[,] vars = new string[InitializedRow, InitializedColoumn];
            framed = new Image[InitializedRow, InitializedColoumn];
            gridTap = new TapGestureRecognizer[InitializedRow, InitializedColoumn];
            choices.RowDefinitions.Clear();
            choices.ColumnDefinitions.Clear();
            foreach (var kids in orphans)
            {
                choices.Children.Remove(kids);
            }
            for (int row = 0; row < InitializedRow; row++)
            {
                choices.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            for (int coloumn = 0; coloumn < InitializedColoumn; coloumn++)
            {
                choices.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            char[] thisletters = new char[Firstword.Count()];
            int randomrow = rand.Next(0, InitializedRow);
            int randomcoloumn = rand.Next(0, InitializedColoumn);
            lettersdivide = Firstword.ToCharArray();
            string[] holders = new string[Firstword.Length];
            int[] J = new int[Firstword.Length];
            int[] I = new int[Firstword.Length];
            AnswerTap = new TapGestureRecognizer[Firstword.Length];

            for (int row = 0; row < InitializedRow; row++)
            {
                for (int coloumn = 0; coloumn < InitializedColoumn; coloumn++)
                {
                    vars[row, coloumn] = "";
                }
            }
            for (int i = 0; i < lettersdivide.Count(); i++)
            {
                AnswerTap[i] = new TapGestureRecognizer();
                bool check = true;
                while (check)
                {
                    if (vars[randomrow, randomcoloumn].Equals(""))
                    {

                        if (i >= 1 && inhere > i)
                        {
                            vars[randomrow, randomcoloumn] = "0";
                            int NG = i;
                            AnswerTap[i].Tapped += (sender, e) =>
                            {


                            };
                        }
                        else
                        {
                            vars[randomrow, randomcoloumn] = lettersdivide[i].ToString().ToLower();
                            if (i > 0)
                            {
                                int NG = i;
                                AnswerTap[i].Tapped += (sender, e) =>
                                {
                                    LoadSoundsLetters();
                                    if (!AnswerLabel[NG].Text.Equals(" ") && NG < lettersdivide.Length)
                                    {


                                        AnswerLabel[NG].Text = " ";
                                        FramedAnswers[NG].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png");
                                        labeled[J[NG], I[NG]].IsVisible = true;
                                        framed[J[NG], I[NG]].IsVisible = true;

                                        for (int looping = 1; looping < lettersdivide.Length; looping++)
                                        {
                                            if (AnswerLabel[looping].Text.Equals(" "))
                                            {
                                                here = looping;
                                                break;
                                            }
                                        }

                                    }
                                    else { here = NG; }
                                };
                            }
                        }
                        check = false;
                    }
                    else
                    {
                        randomcoloumn = rand.Next(0, InitializedColoumn);
                        randomrow = rand.Next(0, InitializedRow);
                    }
                }
                AnswerLabel[i].GestureRecognizers.Add(AnswerTap[i]);
                randomcoloumn = rand.Next(0, InitializedColoumn);
                randomrow = rand.Next(0, InitializedRow);
            }

            if (inhere < 2)
            {
                if (Firstword.Count().Equals(1))
                {
                    here = 0;
                }
                {
                    reserveletters = Firstword.ToCharArray()[0].ToString().ToUpper();
                }
            }

            Color color = Color.Transparent;
            for (int row = 0; row < InitializedRow; row++)
            {
                bool hideit = false;
                for (int coloumn = 0; coloumn < InitializedColoumn; coloumn++)
                {

                    if (vars[row, coloumn].Equals(""))
                    {
                        vars[row, coloumn] = Game.RandomLetters().ToString().ToUpper();

                        framed[row, coloumn] = new Image
                        {
                            Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.frame.png"),
                            Aspect = Aspect.AspectFit

                        };
                        hideit = false;
                    }
                    if (vars[row, coloumn].Equals("0"))
                    {
                        vars[row, coloumn] = "";

                        framed[row, coloumn] = new Image
                        {
                            Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.frame1.png"),
                            Aspect = Aspect.AspectFit

                        };
                        hideit = true;

                    }
                    if (!vars[row, coloumn].Equals("0") && !vars[row, coloumn].Equals(""))
                    {

                        framed[row, coloumn] = new Image
                        {
                            Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.frame.png"),
                            Aspect = Aspect.AspectFit

                        };
                        hideit = false;

                    }

                    labeled[row, coloumn] = new Label
                    {
                        Text = vars[row, coloumn].ToUpper(),
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,

                        TextColor = Color.GhostWhite

                    };
                    if (hideit)
                    {
                        labeled[row, coloumn].IsVisible = false;


                    }

                    gridTap[row, coloumn] = new TapGestureRecognizer();
                    int getJ = row;
                    int getI = coloumn;
                    gridTap[row, coloumn].Tapped += async (sender, e) =>
                    {
                        LoadSoundsLetters();
                        var checkee = vars[getJ, getI];
                        J[here] = getJ;
                        I[here] = getI;
                        holders[here] = checkee;
                        AnswerLabel[here].Text = checkee.ToUpper();
                        FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png");
                        AnswerLabel[here].FontSize = 30;

                        labeled[getJ, getI].IsVisible = false;

                        framed[getJ, getI].IsVisible = false;
                        for (int looping = 1; looping < lettersdivide.Length; looping++)
                        {
                            if (AnswerLabel[looping].Text.Equals(" "))
                            {
                                here = looping;
                                break;
                            }
                        }
                        while (!AnswerLabel[here].Text.Equals(" ") && here < lettersdivide.Count() - 1)
                        {
                            here++;
                        }
                        int counting = 1;

                        for (int checking = 1; checking < lettersdivide.Length; checking++)
                        {
                            if (!AnswerLabel[checking].Text.Equals(" "))
                            {
                                counting++;

                            }
                        }
                        string[] collected = new string[lettersdivide.Length];
                        for (int checking = 0; checking < lettersdivide.Length; checking++)
                        {
                            collected[checking] = AnswerLabel[checking].Text;
                        }



                        //   DisplayAlert("Letters", string.Join("", collected).ToLower(),"Ok");
                        if (counting.Equals(Firstword.Count()))
                        {
                            if (string.Join("", collected).ToUpper().Equals(Firstword.ToUpper()))
                            {


                                Flow.QuestionCounter(Flow.QuestionCount());
                                PointSystem.PointCount(100, mistakepoint, lettersdivide.Count());
                             
                                TimeIsUp = false;
                                IsCorrect = true;
                                ResultPopUp.IsVisible = true;
                                ResultSound();
                                ResultImage.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.correct.png");
                                ResultScore.Text = "Score: " + PointSystem.ShowRoundScore();
                                ResultText.Text = Selection.ThisIsThePhrase();
                                TurnChecker();

                            }
                            else
                            {
                                ResultSound();
                                int QC = Flow.QuestionCounter(Flow.QuestionCount());
                                //  ismistake = true;
                                mistakepoint++;
                                GuesssCount++;
                                //     guess1.Text = "Guess:   " + GuesssCount;
                                try {
                                    if (MultiPlayerSettings.YupThisIsPlayer1())
                                    {
                                        GameMessage newMessage = new GameMessage() { Type = "guess_p1", RoomId = gameInfo.RoomId, Content = false };
                                        await SignalRService.SendMessage(newMessage);
                                    }
                                    else {
                                        GameMessage newMessage = new GameMessage() { Type = "guess_p2", RoomId = gameInfo.RoomId, Content = false };
                                        await SignalRService.SendMessage(newMessage);
                                    }
                                }
                                catch (Exception ex) {
                                    await DisplayAlert("Error", ex.ToString(), "Ok");
                                }
                                if (Firstword.Length < 3)
                                {
                                   await DisplayAlert("Alert", "Wrong Answer", "Continue");
                                }
                                //==========================//

                                inhere += 1;
                                reserveletters = "";
                                foreach (var child in children)
                                {
                                    answer.Children.Remove(child);
                                }
                                answer.ColumnDefinitions.Clear();
                                answer.RowDefinitions.Clear();
                                PopulateAnswer();

                                for (int i = 0; i < inhere; i++)
                                {
                                    AnswerLabel[i].Text = Firstword.ToCharArray()[i].ToString().ToUpper();
                                    if (i > 0)
                                    {
                                        AnswerLabel[i].TextColor = Color.White;
                                    }
                                    AnswerLabel[i].FontSize = 25;
                                    FramedAnswers[i].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fws.png");
                                    reserveletters += AnswerLabel[i].Text;
                                }

                                foreach (var child in children)
                                {
                                    answer.Children.Remove(child);
                                }
                                answer.ColumnDefinitions.Clear();
                                answer.RowDefinitions.Clear();

                                for (int x = 0; x < J.Length; x++)
                                {
                                    for (int y = 0; y < I.Length; y++)
                                    {
                                        labeled[J[x], I[y]].IsVisible = true;
                                        framed[J[x], I[y]].IsVisible = true;

                                    }

                                }

                                here = inhere;
                                for (int a = 1; a < inhere; a++)
                                {
                                    int tester = 1;
                                    for (int x = 0; x < InitializedRow; x++)
                                    {
                                        for (int y = 0; y < InitializedColoumn; y++)
                                        {

                                            if (Firstword.ToCharArray()[a].ToString().ToUpper().Equals(labeled[x, y].Text.ToUpper()) && tester.Equals(1))
                                            {
                                                labeled[x, y].IsVisible = false;
                                                framed[x, y].IsVisible = false;
                                                tester++;
                                            }
                                        }

                                    }
                                }
                                for (int b = 0; b < lettersdivide.Length; b++)
                                {
                                    if (AnswerLabel[b].Text.Equals(" "))
                                    {
                                        AnswerLabel[b].GestureRecognizers.Add(AnswerTap[b]);
                                    }
                                }


                                /*            choices.RowDefinitions.Clear();
                                            choices.ColumnDefinitions.Clear();
                                            foreach (var kids in orphans)
                                            {
                                                choices.Children.Remove(kids);
                                            }
                                  AnswerBank();*/

                                //===============================//
                                if (GuesssCount > PointSystem.CountOfGuesses())
                                {
                                    time = 0;
                                    TimeIsUp = false;
                                    PointSystem.PointCount(100, mistakepoint, lettersdivide.Count());
                                    //      var navPage = new NavigationPage(new WrongPage());
                                    //       Application.Current.MainPage = navPage;

                                    ResultPopUp.IsVisible = true;
                                    ResultSound();
                                    ResultImage.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.x.png");


                                    ResultScore.Text = "Score: " + PointSystem.ShowRoundScore();
                                    ResultText.Text = Selection.ThisIsThePhrase();
                                    TurnChecker();
                                }
                                else
                                {
                                  
                                 await   DisplayAlert("Wrong Answer", (Game.GuessRemaing(lettersdivide.Count()) - GuesssCount) + " Guess Remaining", "OK");
                                }
                            }
                        }

                    };
                    labeled[row, coloumn].FontFamily = "ProximaNovaCond-Xbold.otf#ProximaNovaCond-Xbold";
                    labeled[row, coloumn].FontSize = 30;
                    labeled[row, coloumn].GestureRecognizers.Add(gridTap[row, coloumn]);
                    choices.Children.Add(framed[row, coloumn], coloumn, row);
                    choices.Children.Add(labeled[row, coloumn], coloumn, row);
                }

            }

        }

        public  async void SmashIt(object sender, EventArgs e)
        {
            LoadSoundsMenu();
            Random random = new Random();
            int difficulty = Flow.DifficultyIdentifier();
            int InitializedRow = Creation.AnswerBankRow();



            string FirstWord = Game.SetFirstWord();
            if (difficulty < 3)
            {
                InitializedRow = 1;
            }
            int InitializedColoumn = Creation.AnswerBankColoumn();
            string[,] Words = new string[InitializedRow, InitializedColoumn];
            int ranrow = random.Next(0, InitializedRow);
            int rancoloumn = random.Next(0, InitializedColoumn);
            for (int row = 0; row < InitializedRow; row++)
            {
                for (int coloumn = 0; coloumn < InitializedColoumn; coloumn++)
                {
                    Words[row, coloumn] = "";
                }
            }
            for (int letter = 1; letter < FirstWord.Length; letter++)
            {
                int count = 1;
                int counter = 1;
                for (int let = 1; let < FirstWord.Length; let++)
                {
                    if (FirstWord[letter].ToString().ToLower().Equals(FirstWord[let].ToString().ToLower()))
                    {
                        count++;
                    }

                }
                for (int row = 0; row < InitializedRow; row++)
                {
                    for (int coloumn = 0; coloumn < InitializedColoumn; coloumn++)
                    {



                        if (FirstWord[letter].ToString().ToLower().Equals(labeled[row, coloumn].Text.ToLower()) && counter < count)
                        {
                            counter++;
                            Words[row, coloumn] = "0";
                        }


                    }
                }
            }

            bool decrease = true;
            int letters = 0;
            while (Words[ranrow, rancoloumn].Equals("0") || labeled[ranrow, rancoloumn].Text.Equals(""))
            {
                ranrow = random.Next(0, InitializedRow);
                rancoloumn = random.Next(0, InitializedColoumn);

                if (letters.Equals(100))
                {
                    decrease = false;
                    break;
                }
                letters++;

            }


            if (Game.SmashCounter() > 0 && decrease)
            {

                Game.DeductSmash(1);
                SmashCount.Text = Game.SmashCounter().ToString();
                labeled[ranrow, rancoloumn].Text = "";
                labeled[ranrow, rancoloumn].IsEnabled = false;
                choices.Children.Remove(framed[ranrow, rancoloumn]);
                Words[ranrow, rancoloumn] = "0";

                SmashDeduction.Text = "-1";
                await SmashDeduction.TranslateTo(25, -50, 250, null);
                await SmashDeduction.FadeTo(100, 300, null);

                Device.BeginInvokeOnMainThread(() =>
                {

                   // Lighting.IsVisible = false;

                    SmashDeduction.TranslationX = 0;
                    SmashDeduction.TranslationY = 0;
                    //  SmashDeduction.Opacity = 100;
                    SmashDeduction.Text = "";
                });
            }
            else
            {
             await   DisplayAlert("Stop!", "NO STRIKES LEFT", "Return");
            }



        }

        public async void TimerOn()
        {
           
            while (true) {
             await  Task.Delay(1000);
                TheTimer.Text = time++.ToString();
            } 
        }

        public void TimeFreeze(object sender, EventArgs e)
        {
            LoadSoundsMenu();
            if (Game.SmashCounter() >= 5)
            {
                TimeFreezed = true;
                TimeIsUp = false;
            }
            else
            {

                DisplayAlert("Stop!", "NO STRIKES LEFT", "Return");

            }
        }


        private void TurnChecker()
        {
            int difficulty = Flow.DifficultyIdentifier();

            switch (difficulty)
            {
                case 1: Flow.QuestionCountReset(); break;
                case 2: Flow.QuestionCountReset(); break;
                case 3: Flow.QuestionCountReset(); break;
                case 4: Flow.QuestionCountReset(); break;
            }



        }
        public async void ContinueTapped(object sender, EventArgs e)
        {
            Flow.GetRound(Flow.CurrentRound());
            LoadSoundsMenu();
            ResultPopUp.IsVisible = false;
            SaveOrLoad.ALoadedGame(false);
            int difficulty = Flow.DifficultyIdentifier();
            var children = answer.Children.ToList();
            var orphans = choices.Children.ToList();
            var houses = GameBoard.Children.ToList();
            Random rand = new Random();
            Creation.AnswerBankLoader();
            string Firstword = Game.SetFirstWord();
            if (Firstword.Equals(null))
            {
                var navPage = new NavigationPage(new Blank2());
                Application.Current.MainPage = navPage;
            }

            PointSystem.GuessCounter(Firstword.Length);
            int[] J = new int[Firstword.Length];
            int[] I = new int[Firstword.Length];
            int InitializedRow = Creation.AnswerBankRow();
            if (difficulty < 3)
            {
                InitializedRow = 1;
            }
            int InitializedColoumn = Creation.AnswerBankColoumn();
            if (Flow.CurrentRound() < 5)
            {

                if (GuesssCount > PointSystem.CountOfGuesses() || IsCorrect)
                {

                    here = 1;
                    inhere = 1;
                    mistakepoint = 0;
                    currentcount = 0;
                    GuesssCount = 1;
                    IsCorrect = false;
                    reserveletters = "";
                    GameBoard.RowDefinitions.Clear();
                    GameBoard.ColumnDefinitions.Clear();
                    foreach (var items in houses)
                    {
                        GameBoard.Children.Remove(items);
                    }
                    GameBoard.RowDefinitions.Clear();
                    GameBoard.ColumnDefinitions.Clear();
                    PopulateGameBoard();
                    answer.ColumnDefinitions.Clear();
                    answer.RowDefinitions.Clear();
                    foreach (var child in children)
                    {
                        answer.Children.Remove(child);
                    }
                    answer.ColumnDefinitions.Clear();
                    answer.RowDefinitions.Clear();
                    PopulateAnswer();
                    choices.RowDefinitions.Clear();
                    choices.ColumnDefinitions.Clear();
                    foreach (var kids in orphans)
                    {
                        choices.Children.Remove(kids);
                    }
                    choices.RowDefinitions.Clear();
                    choices.ColumnDefinitions.Clear();
                    AnswerBank();
                    try
                    {
                        if (MultiPlayerSettings.YupThisIsPlayer1())
                        {

                            GameMessage newMessage = new GameMessage() { Type = "round_p1", RoomId = gameInfo.RoomId };
                            await SignalRService.SendMessage(newMessage);


                        }
                        else
                        {


                            GameMessage newMessage = new GameMessage() { Type = "round_p2", RoomId = gameInfo.RoomId };
                            await SignalRService.SendMessage(newMessage);

                        }

                        if (MultiPlayerSettings.YupThisIsPlayer1())
                        {
                            scoreNumP1 = PointSystem.TotalScore();
                            GameMessage newMessageScore = new GameMessage() { Type = "score_p1", RoomId = gameInfo.RoomId, Content = scoreNumP1 };
                            await SignalRService.SendMessage(newMessageScore);

                        }
                        else
                        {

                            scoreNumP2 = PointSystem.TotalScore();
                            GameMessage newMessageScore = new GameMessage() { Type = "score_p2", RoomId = gameInfo.RoomId, Content = scoreNumP2 };
                            await SignalRService.SendMessage(newMessageScore);

                        }
                        if (MultiPlayerSettings.YupThisIsPlayer1())
                        {
                            GameMessage newMessage = new GameMessage() { Type = "guess_p1", RoomId = gameInfo.RoomId, Content = true };
                            await SignalRService.SendMessage(newMessage);
                        }
                        else
                        {
                            GameMessage newMessage = new GameMessage() { Type = "guess_p2", RoomId = gameInfo.RoomId, Content = true };
                            await SignalRService.SendMessage(newMessage);
                        }
                    }
                    catch (Exception ex) {
                        await DisplayAlert("Error",ex.ToString(),"Ok");
                    }
                    MultiPlayerSettings.GetOtherUserIngameData(Convert.ToInt32(TotalScore2.Text),guess2.Text,round2.Text);

                      

                    /*  
                    var navPage = new NavigationPage(new Blank2());
                   await Task.Delay(200);
                   await navPage.FadeTo(0, 250);
                   await Task.Delay(200);
                   await navPage.FadeTo(1, 250);
                   Application.Current.MainPage = navPage;
                       */


                }
                else
                {

                    ResultPopUp.IsVisible = false;
                    GuesssCount++;

                    inhere += 1;
                    reserveletters = "";
                    foreach (var child in children)
                    {
                        answer.Children.Remove(child);
                    }
                    answer.ColumnDefinitions.Clear();
                    answer.RowDefinitions.Clear();
                    PopulateAnswer();

                    for (int i = 0; i < inhere; i++)
                    {
                        AnswerLabel[i].Text = Firstword.ToCharArray()[i].ToString().ToUpper();
                        if (i > 0)
                        {
                            AnswerLabel[i].TextColor = Color.White;
                        }
                        AnswerLabel[i].FontSize = 25;
                        FramedAnswers[i].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fws.png");
                        reserveletters += AnswerLabel[i].Text;
                    }

                    foreach (var child in children)
                    {
                        answer.Children.Remove(child);
                    }
                    answer.ColumnDefinitions.Clear();
                    answer.RowDefinitions.Clear();

                    for (int x = 0; x < J.Length; x++)
                    {
                        for (int y = 0; y < I.Length; y++)
                        {
                            labeled[J[x], I[y]].IsVisible = true;
                            framed[J[x], I[y]].IsVisible = true;

                        }

                    }
                    for (int x = 0; x < InitializedRow; x++)
                    {
                        for (int y = 0; y < InitializedColoumn; y++)
                        {
                            labeled[x, y].IsVisible = true;
                            framed[x, y].IsVisible = true;
                        }
                    }
                    here = inhere;
                    for (int a = 1; a < inhere; a++)
                    {
                        int tester = 1;

                        for (int x = 0; x < InitializedRow; x++)
                        {
                            for (int y = 0; y < InitializedColoumn; y++)
                            {

                                if (Firstword.ToCharArray()[a].ToString().ToUpper().Equals(labeled[x, y].Text.ToUpper()) && tester.Equals(1))
                                {
                                    labeled[x, y].IsVisible = false;
                                    framed[x, y].IsVisible = false;
                                    tester++;
                                }
                            }

                        }
                    }
                    for (int b = 0; b < lettersdivide.Length; b++)
                    {
                        if (AnswerLabel[b].Text.Equals(" "))
                        {
                            AnswerLabel[b].GestureRecognizers.Add(AnswerTap[b]);
                        }
                    }
                 

                    // TimerOn();
               
              

                }


            }
            else
            {

                var navPage = new NavigationPage(new SummaryPage());
                await Task.Delay(200);
                await navPage.FadeTo(0, 250);
                await Task.Delay(200);
                await navPage.FadeTo(1, 250);
                Application.Current.MainPage = navPage;
            }


        }
        public async void ExitTapped(object sender, EventArgs e)
        {

            LoadSoundsMenu();
            var navPage = new NavigationPage(new MainPage());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;



        }


       
        public void ExitOrSave(object sender, EventArgs e)
        {
            LoadSoundsMenu();
            TimeFreezed = true;
            TimeIsUp = false;

            SavingGame.IsVisible = true;


        }
        public async void ShopSetting(object sender, EventArgs e)
        {
            LoadSoundsMenu();
            for (int i=5;Selection.SetOnlineQoutes().Count>0;i-- ) {
                await DisplayAlert("Check", Selection.SetOnlineQoutes()[i-1].QoutePhrase, "Ok");
            }
       
        }

        public void LoadSoundsMenu()
        {
            var ClickedSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            ClickedSound.Load("dropsound.wav");
            ClickedSound.Play();
        }
        public void LoadSoundsLetters()
        {
            var ClickedSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            ClickedSound.Load("resetsound.wav");
            ClickedSound.Play();
        }
        public void ResultSound()
        {
            var ClickedSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            if (IsCorrect == true) { ClickedSound.Load("correctsound.mp3"); }
            else
            {
                if (TimeIsUp == true) { ClickedSound.Load("timeup.wav"); }
                else
                {
                    ClickedSound.Load("wrongsound.wav");
                }
            }

            ClickedSound.Play();
        }
     
        //=============================================//
        public async void GameConnect()
        {
                                                                                                                                                                                                                                                                                                                                                                                                                                             
          
            try
            {
                me.UserName = UserData.GetUserLabel()?.Trim();
                me.UserId = Guid.NewGuid().ToString();

                SignalRConnectionInfo connectionInfo = await SignalRService.GetSignalRConnectionInfo();
                var url = connectionInfo.Url;
                //   lblMessages.Text = $"Connecting...";

                connection = new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(connectionInfo.AccessToken);
                })
                .Build();
             
                #region Rounds and guesses

                connection.On<GameMessage>("log", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                            var newMessage = $"{message.Player.UserName}: {message.Type}";
                            //      lblMessages.Text += Environment.NewLine + newMessage;
                        }
                    });
                });

                connection.On<GameMessage>("round_p1", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                        

                            if (MultiPlayerSettings.YupThisIsPlayer1())
                            {
                                var num = ++roundNumP1;
                             
                                // lblRoundP1.Text = (num).ToString();
                                round1.Text = "Round: " + (num).ToString();

                            }
                            else
                            {
                                var num = ++roundNumP1;
                             
                                // lblRoundP1.Text = (num).ToString();
                                round2.Text = "Round: " + (num).ToString();

                            }




                        }
                    });
                });
                connection.On<GameMessage>("guess_p1", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {

                         
                            if (MultiPlayerSettings.YupThisIsPlayer1())
                            {


                                bool NextRound = Convert.ToBoolean(message.Content);
                                if (NextRound)
                                {
                                    guessNumP1 = 0;
                                }
                                var num = ++guessNumP1;
                              

                                //      lblGuessP1.Text = (num).ToString();
                                guess1.Text = "Guess: " + (num).ToString();

                            }
                            else
                            {

                                bool NextRound = Convert.ToBoolean(message.Content);
                                if (NextRound)
                                {
                                    guessNumP1 = 0;
                                }

                                var num = ++guessNumP1;
                              
                                //      lblGuessP1.Text = (num).ToString();
                                guess2.Text = "Guess: " + (num).ToString();

                            }

                        }
                    });
                });
                connection.On<GameMessage>("score_p1", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {


                            if (MultiPlayerSettings.YupThisIsPlayer1())
                            {
                                scoreNumP1 = (int)message.Content;
                                var num = scoreNumP1;
                                TotalScore.Text = (num).ToString();

                            }
                            else
                            {
                                scoreNumP1 = (int)message.Content;
                                var num = scoreNumP1;
                                TotalScore2.Text = (num).ToString();

                            }

                        }
                    });
                });
                connection.On<GameMessage>("round_p2", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                          
                              
                            if (!MultiPlayerSettings.YupThisIsPlayer1())
                            {
                                var num = ++roundNumP2;
                                //          lblRoundP2.Text = (num).ToString();
                           
                                round1.Text = "Round: " + (num).ToString();

                            }
                            else
                            {
                                var num = ++roundNumP2;
                                //          lblRoundP2.Text = (num).ToString();
                           
                                round2.Text = "Round: " + (num).ToString();


                            }



                        }
                    });
                });
                connection.On<GameMessage>("guess_p2", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                          
                            if (!MultiPlayerSettings.YupThisIsPlayer1())
                            {
                                bool NextRound = Convert.ToBoolean(message.Content);
                                if (NextRound)
                                {
                                    guessNumP2 = 0;
                                }

                                var num = ++guessNumP2;
                                //        lblGuessP2.Text = (num).ToString();
                          
                                guess1.Text = "Guess: " + (num).ToString();

                            }
                            else
                            {
                                bool NextRound = Convert.ToBoolean(message.Content);
                                if (NextRound)
                                {
                                    guessNumP2 = 0;
                                }
                                var num = ++guessNumP2;
                                //        lblGuessP2.Text = (num).ToString();
                             
                                guess2.Text = "Guess: " + (num).ToString();


                            }

                        }
                    });
                });
                connection.On<GameMessage>("score_p2", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {


                            if (!MultiPlayerSettings.YupThisIsPlayer1())
                            {
                                scoreNumP2 = (int)message.Content;
                                var num = scoreNumP2;

                                //      lblGuessP1.Text = (num).ToString();
                                TotalScore.Text = (num).ToString();

                            }
                            else
                            {
                                scoreNumP2 = (int)message.Content;
                                var num = scoreNumP2;

                                //      lblGuessP1.Text = (num).ToString();
                                TotalScore2.Text = (num).ToString();

                            }

                        }
                    });
                });
                #endregion

                //2 player "Elapsed" function
                connection.On<GameMessage>("time", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                   //     if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            //allowMessage = false;

                        if (allowMessage)
                        {
                            var num = (int)message.Content;
                            timeCount += num;
                            //  lblTimer.Text = (timeCount).ToString();
                           TheTimer.Text = (timeCount).ToString();
                        }
                    });
                });

                //2 player "Start/Stop" function
                connection.On<GameMessage>("startstoptimer", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                       // if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                        //    allowMessage = false;

                        if (allowMessage)
                        {
                            var timerIsRunning = true;

                            if (timerIsRunning)
                            {
                                if (isPlayer1)
                                    GameTimer.Stop();

                           //     TimerIsRunning = false;
                                //            btnStartTimer.Text = "Start Timer";
                            }
                            else
                            {
                                if (isPlayer1)
                                    GameTimer.Start();

                             //   TimerIsRunning = true;
                                //           btnStartTimer.Text = "Stop Timer";
                            }
                        }
                    });
                });

                await connection.StartAsync();
           
            }
            catch (Exception ex)
            {
                //        lblMessages.Text = ex.Message;
            }
        }
        #region Multiplayer Time Settings
        
        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
          
   
       
                if (timeCount > 0)
                {
                    GameMessage newMessage = new GameMessage() { Type = "time", Content = 1 };
                    await SignalRService.SendMessage(newMessage);
                }
            
            
        }

        public void SetTimer()
        {
         

            if (GameTimer != null)
                GameTimer.Stop();

            if (GameIsTimed)
            {
                TimerP1IsVisible = true;
                TimerP2IsVisible = true;

                GameTimer = new Timer();
                GameTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                GameTimer.Interval = 1000;
            }
            else
            {
                TimerP1IsVisible = false;
                TimerP2IsVisible = false;
            }
        }
        #endregion


    }



}