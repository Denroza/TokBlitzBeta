using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokBlitzBeta.GamePlay;
using System.Timers;
using System.Threading;
using Lottie.Forms;
using Plugin.Connectivity;
using CocosSharp;
using Plugin.Vibrate;
namespace TokBlitzBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Blank1 : ContentPage
    {
       
        Label[,] labeled;
        string[] AllWords;
        Label[,] BoardLabel = new Label[10, 5];
        Frame[,] frames = new Frame[10, 5];
        Frame[,] frameschoice;
        Image[,] framed;
        Image[,] FrameWord = new Image[10, 5];
        Image[] FramedAnswers;
        Image[] ImageDrop;
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
        bool TimeIsUp = true;
        int time; 
        bool TimeFreezed = false;
        bool IsCorrect = false;
        int TimeDeduct = 0;
        string ChosenPhrase;
        bool EnteredHere = false;
        int difficulty = Flow.DifficultyIdentifier();
        string[] JSON = { "lightning.json", "lightning1.json" , "lightning2.json", "lightning3.json", "lightning4.json" };
        public Blank1()
        {

            InitializeComponent();
         
            back.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.panel.png");
            PanelSetting2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.panel.png");
            Close1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.close.png");
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.InGame.png");
            background1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bgwrong.png");
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
            Player2Name.Text = "";
           
            if (String.IsNullOrEmpty(UserData.GetUserProfileUrl()))
            {
                ProPic1.Source = ImageSource.FromUri(new Uri("http://polrestabessurabaya.com/images/personil/2e49b55fbe7a196f05b4b3c031253210.jpg"));
            }
            else
            {
                ProPic1.Source = ImageSource.FromUri(new Uri(UserData.GetUserProfileUrl()));
            }
         

            TotalScore.TextColor = Color.Goldenrod;
            TotalScore.FontSize = 16;
        
            try {
               PopulateGameBoard();
                PopulateAnswer();
                AnswerBank();
            }
            catch (Exception ex) {
                Console.Write(ex.ToString());
            }



      
                TotalScore.Text = PointSystem.TotalScore().ToString();
                round1.Text = "Round:   " + (Flow.CurrentRound() + 1);
                guess1.Text = "Guess:   " + GuesssCount;
                SmashCount.Text = Game.SmashCounter().ToString();

  
         

            QouteCategory.Text = Selection.ShowCategory().ToUpper();
            QouteSource.Text = Selection.ShowSource();
            SmashCount.TextColor = Color.DarkRed;
            SmashDeduction.TextColor = Color.GhostWhite;
            SmashDeduction.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            SmashCount.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            TheTimer.TextColor = Color.DarkRed;
            TheTimer.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            ResultScore.FontFamily = "Face Off M54.ttf#Face Off M54";
            Player1Name.FontFamily = "Face Off M54.ttf#Face Off M54";
            ResultText.FontFamily = "Face Off M54.ttf#Face Off M54";
            QouteCategory.FontFamily = "Face Off M54.ttf#Face Off M54";
            QouteSource.FontFamily = "Face Off M54.ttf#Face Off M54";
            TimerOn();
            LoadGames();
            SavingUI();
            ShoppingSmash();
            Flow.GetRound(Flow.CurrentRound());
        
           
            if (Flow.GameInCategory()) {
                Home.IsEnabled = false;
            }

        }

        //========================================================//
        public  void PopulateGameBoard()
        {
            Game game = new Game();
            TokGamesApiClient apiClient = new TokGamesApiClient();
            Selection selection = new Selection();
        Selection.PhraseSelectionInitiate();
           
            Random random = new Random();
         
                AllWords = Selection.SelectedPhrase();
               /* while (String.IsNullOrEmpty(AllWords.ToString())) {
                    Selection.PhraseSelectionInitiate();
                    AllWords = Selection.SelectedPhrase();
                }
                */
            
    

            //====//
            /*   if (CrossConnectivity.Current.IsConnected)
               {
                   int randoming;
                   string[] splitter = { " ", "/", "...", "-" };
                   randoming = random.Next(0, Selection.SetOnlineQoutes().Count - 1);

                   ChosenPhrase = Selection.SetOnlineQoutes()[randoming].QoutePhrase;
                   while (!Selection.PhraseCountChecker(ChosenPhrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries).Count()) || !Selection.WordCounter(ChosenPhrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries)) || 
                       Selection.DuplicatePhrase(Selection.SetOnlineQoutes()[randoming].QouteID) || Selection.HasEnoughWordsToBeAnswered(ChosenPhrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries)) )
                   {
                       randoming = random.Next(0, Selection.SetOnlineQoutes().Count - 1);
                       ChosenPhrase = Selection.SetOnlineQoutes()[randoming].QoutePhrase;
                   }
                   Selection.GetOnlineID(Selection.SetOnlineQoutes()[randoming].QouteID);

                   AllWords = ChosenPhrase.Split(splitter,StringSplitOptions.RemoveEmptyEntries);
                       ChosenPhrase = LocalSelection.SetPhrases()[Flow.CurrentRound()];
            //Selection.GetOnlineID(Selection.SetOnlineQoutes()[Flow.CurrentRound()].QouteID);

            string[] splitter = { " ", "/", "...", "-", ";", ":" };
            AllWords = ChosenPhrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

               }*/



            // Selection.GetOnlineID(Selection.SetOnlineQoutes()[randoming].QouteID);




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
                                Aspect =Aspect.AspectFit
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
            if (CrossConnectivity.Current.IsConnected) {
             //   TheAnswers = await game.OnlineSetToBoardLetters();
          //      Firstword = await game.OnlineFirstWord(); ;
            }

            AnswerLabel = new Label[Firstword.Length];
            FramedAnswers = new Image[Firstword.Length];
            ImageDrop = new Image[Firstword.Length];
            answer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (int NumberOfGrid = 0; NumberOfGrid < Firstword.Length; NumberOfGrid++)
            {

                answer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                if (NumberOfGrid.Equals(0))
                {

                    if (Firstword.ToCharArray().Count().Equals(0))
                    {
                        ImageDrop[NumberOfGrid] = new Image
                        {
                            Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png"),
                        };
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
                        ImageDrop[NumberOfGrid] = new Image
                        {
                            Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fws.png"),
                        };
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
                    ImageDrop[NumberOfGrid] = new Image
                    {
                        Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png"),
                    };
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
                answer.Children.Add(ImageDrop[NumberOfGrid], NumberOfGrid, 0);
                answer.Children.Add(FramedAnswers[NumberOfGrid], NumberOfGrid, 0);
                answer.Children.Add(AnswerLabel[NumberOfGrid], NumberOfGrid, 0);
            }
        }
        //========================================================//
        public  void AnswerBank()
        {

            here = inhere;
            //int difficulty = Flow.DifficultyIdentifier();
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
                              Animation animation = new Animation(v => FramedAnswers[here].Opacity= v, 0, 2);
                               FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fss.png");
                                try
                                {
                                   animation.Commit(this, "SimpleAnimation", 16, 750, Easing.Linear, (v, c) => FramedAnswers[here].Opacity = 1, () => true);
                                }
                                catch (Exception e) { }
                             
                                int NG = i;
                                AnswerTap[i].Tapped += (sender, e) =>
                                {

                                    BGMusics.Letter_Sound().Play();
                                   
                                    //Frame has letter
                                    if (!AnswerLabel[NG].Text.Equals(" ") && NG < lettersdivide.Length)
                                    {


                                        FramedAnswers[here].Opacity = 100;
                                        FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png");
                                       animation = new Animation(v => FramedAnswers[here].Opacity = v, 0, 2);
            
                                        AnswerLabel[NG].Text = " ";
                                        FramedAnswers[NG].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fss.png");
                                        labeled[J[NG], I[NG]].IsVisible = true;
                                        framed[J[NG], I[NG]].IsVisible = true;
                                        here = NG;
                                        FramedAnswers[NG].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fss.png");
                                        try
                                        {
                                           animation.Commit(this, "SimpleAnimation", 16, 750, Easing.Linear, (v, c) => FramedAnswers[NG].Opacity = 1, () => true);
                                        }
                                        catch (Exception) { }

                                                                                /*
                                        FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png");
                                        FramedAnswers[NG].Scale = 1;
                                        AnswerLabel[NG].Text = " ";
                                        FramedAnswers[NG].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fss.png");
                                        labeled[J[NG], I[NG]].IsVisible = true;
                                        framed[J[NG], I[NG]].IsVisible = true;
                                        //FramedAnswers[NG].ScaleTo(1.5, 400, null);
                                   
                                        animation.Commit(this, "SimpleAnimation", 16, 750, Easing.Linear, (v, c) => FramedAnswers[NG].Scale = 1, () => true);
                                     
                                        for (int looping = 1; looping < lettersdivide.Length; looping++)
                                        {

                                            if (AnswerLabel[looping].Text.Equals(" "))
                                            {
                                              
                                                here = looping;
                                                FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fss.png");
                                                break;
                                            }
                                        }
                                 */

                                    }
                                    //Frame has no letter
                                    else {
                                        FramedAnswers[here].Scale = 1;
                                        FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png");
                                        try
                                        {
                                       animation = new Animation(v => FramedAnswers[here].Opacity = v, 0, 2);
                                        }
                                        catch (Exception) { }
                                        here = NG;
                                        FramedAnswers[NG].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fss.png");
                                        try {
                                            animation.Commit(this, "SimpleAnimation", 16, 750, Easing.Linear, (v, c) => FramedAnswers[NG].Opacity = 1, () => true);
                                        }
                                        catch (Exception) { }
                                        
                                    }
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

                    // Letters action code
                    gridTap[row, coloumn].Tapped += (sender, e) =>
                    {
                        BGMusics.Letter_Sound().Play();
                        Animation animation = new Animation(v => FramedAnswers[here].Opacity = v, 0, 2);
                        Animation animation1 = new Animation(v => background1.Opacity = v, 0, 0);
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
                                FramedAnswers[here].Scale = 1;
                                here = looping;
                             
                                FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fss.png");
                                try
                                {
                               animation.Commit(this, "SimpleAnimation", 16, 750, Easing.Linear, (v, c) => FramedAnswers[here].Opacity = 1, () => true);
                                }
                                catch (Exception) { }
                                    break;
                            }
                        }
                        while (!AnswerLabel[here].Text.Equals(" ") && here < lettersdivide.Count() - 1)
                        {

                            FramedAnswers[here].Scale = 1;
                            here++;
                            FramedAnswers[here].Scale = 1;
                            FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fns.png");
                            try {
                            animation.Commit(this, "SimpleAnimation", 16, 750, Easing.Linear, (v, c) => FramedAnswers[here].Opacity= 1, () => true);
                            }
                            catch (Exception) { }
                            
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

                                BGMusics.Correct_Sound().Play();
                                  Flow.QuestionCounter(Flow.QuestionCount());
                                PointSystem.PointCount(100, mistakepoint, lettersdivide.Count());
                                time = 0;
                                TimeIsUp = false;
                                IsCorrect = true;
                                ResultPopUp.IsVisible = true;
                               
                                ResultImage.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.correct.png");


                                ResultScore.Text = "Score: " + PointSystem.ShowRoundScore();
                                ResultText.Text = Selection.ThisIsThePhrase();
                                TurnChecker();
                               
                            }
                            else
                            {
                               
                                int QC = Flow.QuestionCounter(Flow.QuestionCount());
                                //  ismistake = true;
                                mistakepoint++;
                                GuesssCount++;
                                guess1.Text = "Guess:   " + GuesssCount;
                                if (Firstword.Length < 3)
                                {
                                    DisplayAlert("Alert", "Wrong Answer", "Continue");
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
                             

                                /*  choices.RowDefinitions.Clear();
                                            choices.ColumnDefinitions.Clear();
                                            foreach (var kids in orphans)
                                            {
                                                choices.Children.Remove(kids);
                                            }
                                  AnswerBank();*/

                                //===============================//
                                if (GuesssCount >= PointSystem.CountOfGuesses())
                                {
                                    time = 0;
                                    TimeIsUp = false;
                                    PointSystem.PointCount(100, mistakepoint, lettersdivide.Count());
                                    //      var navPage = new NavigationPage(new WrongPage());
                                    //       Application.Current.MainPage = navPage;

                                    ResultPopUp.IsVisible = true;
                                  
                                    ResultImage.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.x.png");
                                    BGMusics.Wrong_Sound().Play();

                               
                                    background1.Opacity = .75;
                                    ResultScore.Text = "Score: " + PointSystem.ShowRoundScore();
                                    ResultText.Text = Selection.ThisIsThePhrase();
                                    TurnChecker();
                                }
                                else
                                {
                                    CrossVibrate.Current.Vibration();
                                    FramedAnswers[here].Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.fss.png");
                                    try
                                    {
                                       
                                        animation.Commit(this, "SimpleAnimation", 16, 750, Easing.Linear, (v, c) => FramedAnswers[here].Opacity = 1, () => true);
                                    }
                                    catch (Exception) { }
                                    TimeDeduct += 5;
                                    if (!Flow.GameInCategory()) { 
                                    switch (difficulty)
                                    {
                                        case 1: time = 15 - TimeDeduct; break;
                                        case 2: time = 20 - TimeDeduct; break;
                                        case 3: time = 25 - TimeDeduct; break;
                                        case 4: time = 30 - TimeDeduct; break;
                                    }
                                    }
                                    else{
                                        if (Firstword.Length >= 10 && Firstword.Length <= 12)
                                        {
                                            time = 30 - TimeDeduct;
                                        }
                                        else if (Firstword.Length >= 7 && Firstword.Length <= 9)
                                        {
                                            time = 25 - TimeDeduct;
                                        }
                                        else if (Firstword.Length >= 5 && Firstword.Length <= 6)
                                        {
                                            time = 20 - TimeDeduct;
                                        }
                                        else {
                                            time = 15 - TimeDeduct;
                                        }
                                    }

                                    DisplayAlert("Wrong Answer", (Game.GuessRemaing(lettersdivide.Count()) - GuesssCount) + " Guess Remaining", "OK");
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

        public async void SmashIt(object sender, EventArgs e)
        {

            Random random = new Random();
            // int difficulty = Flow.DifficultyIdentifier();
            int InitializedRow = Creation.AnswerBankRow();



            string FirstWord = Game.SetFirstWord();

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
                BGMusics.Clicking_Sound().Play();
                BGMusics.Strike_Sound().Play();
                // AnimationView animation = Game.LightningAnimation();
                Lighting.AutoPlay = false;
                Lighting.Animation = JSON[0];//JSON[Flow.CurrentRound()-1]


                Lighting.IsVisible = true;
                Lighting.Play();

                await Task.Delay(500);




                if (!Flow.InTestMode())
                {
                    Game.DeductSmash(1);
                    SmashCount.Text = Game.SmashCounter().ToString();
                }


                labeled[ranrow, rancoloumn].Text = "";
                labeled[ranrow, rancoloumn].IsEnabled = false;
                choices.Children.Remove(framed[ranrow, rancoloumn]);
                Words[ranrow, rancoloumn] = "0";
                SmashDeduction.Text = "-1";
                await SmashDeduction.TranslateTo(25, -50, 300, null);
                await SmashDeduction.FadeTo(100, 300, null);



                Device.BeginInvokeOnMainThread(() =>
                {



                    SmashDeduction.TranslationX = 0;
                    SmashDeduction.TranslationY = 0;
                    //  SmashDeduction.Opacity = 100;
                    SmashDeduction.Text = "";
                });
                Lighting.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Stop!", "NO STRIKES LEFT", "Return");

            }

        }

        public async void TimerOn()
        {

            //  int difficulty = Flow.DifficultyIdentifier();
            string Firstword = Game.SetFirstWord();
            if (!Flow.GameInCategory())
            {
                switch (difficulty)
                {
                    case 1: time = 15 - TimeDeduct; break;
                    case 2: time = 20 - TimeDeduct; break;
                    case 3: time = 25 - TimeDeduct; break;
                    case 4: time = 30 - TimeDeduct; break;
                }
            }
            else
            {
                if (Firstword.Length >= 10 && Firstword.Length <= 12)
                {
                    time = 30 - TimeDeduct;
                }
                else if (Firstword.Length >= 7 && Firstword.Length <= 9)
                {
                    time = 25 - TimeDeduct;
                }
                else if (Firstword.Length >= 5 && Firstword.Length <= 6)
                {
                    time = 20 - TimeDeduct;
                }
                else
                {
                    time = 15 - TimeDeduct;
                }
            }

            while (!time.Equals(-1))
            {
                if (TimeFreezed)
                {
                    Game.DeductSmash(5);
                    SmashCount.Text = Game.SmashCounter().ToString();
                    break;
                }
                await Task.Delay(1000);
                TheTimer.Text = time--.ToString();

            }


            if (TimeIsUp)
            {
                BGMusics.TimeUp_Sound().Play();
                mistakepoint++;
                ResultPopUp.IsVisible = true;
                ResultImage.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.TU.png");
                if (GuesssCount >= PointSystem.CountOfGuesses())
                {
                    ResultScore.Text = "Score: " + PointSystem.ShowRoundScore();
                    ResultText.Text = Selection.ThisIsThePhrase();
                }
                else {
                    ResultScore.Text = "";
                    ResultText.Text = "";
                }
                    TurnChecker();

            }
        }

        public async void TimeFreeze(object sender, EventArgs e)
        {
          
           
                if (Game.SmashCounter() >= 5)
                {
                    BGMusics.Clicking_Sound().Play();
                    TimeFreezed = true;
                    TimeIsUp = false;
                    SmashDeduction.Text = "-5";
                    await SmashDeduction.TranslateTo(25, -50, 250, null);
                    await SmashDeduction.FadeTo(100, 300, null);
                    await Clock.FadeTo(0, 300, null);
                    await TheTimer.FadeTo(0, 300, null);
                    Device.BeginInvokeOnMainThread(() =>
                    {

                        Clock.IsEnabled = false;
                        TheTimer.IsEnabled = false;
                        SmashDeduction.TranslationX = 0;
                        SmashDeduction.TranslationY = 0;
                        //  SmashDeduction.Opacity = 100;
                        SmashDeduction.Text = "";
                    });
                }
                else
                {

                    await DisplayAlert("Stop!", "NO STRIKES LEFT", "Return");

                }
         }


        private void TurnChecker()
        {
         //   int difficulty = Flow.DifficultyIdentifier();

           
            string Firstword = Game.SetFirstWord();
            if (!Flow.GameInCategory())
            {
                switch (difficulty)
                {
                    case 1: Flow.QuestionCountReset(); break;
                    case 2: Flow.QuestionCountReset(); break;
                    case 3: Flow.QuestionCountReset(); break;
                    case 4: Flow.QuestionCountReset(); break;
                }
            }
            else
            {
                if (Firstword.Length >= 10 && Firstword.Length <= 12)
                {
                    Flow.QuestionCountReset();
                }
                else if (Firstword.Length >= 7 && Firstword.Length <= 9)
                {
                    Flow.QuestionCountReset();
                }
                else if (Firstword.Length >= 5 && Firstword.Length <= 6)
                {
                    Flow.QuestionCountReset();
                }
                else
                {
                    Flow.QuestionCountReset();
                }
            }



        }
        public async void ContinueTapped(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
            try
            {
               
                ResultPopUp.IsVisible = false;
          
              //  int difficulty = Flow.DifficultyIdentifier();
                var children = answer.Children.ToList();
                var orphans = choices.Children.ToList();

                Random rand = new Random();
                Creation.AnswerBankLoader();
                string Firstword = Game.SetFirstWord();
                if (Firstword.Equals(null))
                {
                    await Application.Current.MainPage.FadeTo(0, 300);
                    var navPage = new NavigationPage(new Blank1());
                    Application.Current.MainPage = navPage;
                    await Task.Delay(250);
                    await navPage.FadeTo(0, 400);
                    await navPage.FadeTo(1, 300);
                }

                PointSystem.GuessCounter(Firstword.Length);
                int[] J = new int[Firstword.Length];
                int[] I = new int[Firstword.Length];
                int InitializedRow = Creation.AnswerBankRow();
           
                int InitializedColoumn = Creation.AnswerBankColoumn();
                if (Flow.CurrentRound() < 5)
                {

                    if (GuesssCount >= PointSystem.CountOfGuesses() || IsCorrect)
                    {

                        await Application.Current.MainPage.FadeTo(0, 300);
                        var navPage = new NavigationPage(new Blank1());
                        Application.Current.MainPage = navPage;
                        await Task.Delay(250);
                        await navPage.FadeTo(0, 400);
                        await navPage.FadeTo(1, 300);

                    
                        
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
                        TimeDeduct += 5;
                    
                        if (!Flow.GameInCategory())
                        {
                            switch (difficulty)
                            {
                                case 1: time = 15 - TimeDeduct; break;
                                case 2: time = 20 - TimeDeduct; break;
                                case 3: time = 25 - TimeDeduct; break;
                                case 4: time = 30 - TimeDeduct; break;
                            }
                        }
                        else
                        {
                            if (Firstword.Length >= 10 && Firstword.Length <= 12)
                            {
                                time = 30 - TimeDeduct;
                            }
                            else if (Firstword.Length >= 7 && Firstword.Length <= 9)
                            {
                                time = 25 - TimeDeduct;
                            }
                            else if (Firstword.Length >= 5 && Firstword.Length <= 6)
                            {
                                time = 20 - TimeDeduct;
                            }
                            else
                            {
                                time = 15 - TimeDeduct;
                            }
                        }

                        TimerOn();
                        guess1.Text = "Guess:   " + GuesssCount;
                    }


                }
                else
                {
                    ResultPopUp.IsVisible = false;
                  
                    if(EnteredHere == false) {
                        Device.BeginInvokeOnMainThread(() => {
                            ResultPopUp.IsVisible = true;
                         
                            ResultImage.IsVisible = false;
                            Resulting.FontFamily = "Face Off M54.ttf#Face Off M54";
                            Resulting.TextColor = Color.Red;
                            Resulting.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 5;
                            Resulting.Text = "GAME"+Environment.NewLine+"OVER";
                            EnteredHere = true;
                            ResultScore.TextColor = Color.WhiteSmoke; 
                            ResultScore.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 2;
                            ResultScore.Text = "You Earned" +Environment.NewLine+ PointSystem.TotalScore()+" PTS";
                            ResultText.IsVisible =false;
                        });
                  
                    }
                    else {
                        BGMusics.InGame_BGM().Stop();
                        var agree = await DisplayAlert("Before you leave...", "Do you want to save this tok?", "Yes", "No");

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (agree)
                            {
                                SavingGame.IsVisible = true;
                            }
                            else {
                                await Application.Current.MainPage.FadeTo(0, 300);
                                var navPage = new NavigationPage(new MainPage());
                                Application.Current.MainPage = navPage;
                                await Task.Delay(250);
                                await navPage.FadeTo(0, 400);
                                await navPage.FadeTo(1, 300);
                            }
                        });

                    }
                }
            }catch (Exception ex) {
                await DisplayAlert("Error",ex.ToString(),"OK");
            }

        }
        public async void ExitTapped(object sender, EventArgs e)
        {

            BGMusics.InGame_BGM().Stop();
            await Application.Current.MainPage.FadeTo(0, 300);
            var navPage = new NavigationPage(new MainPage());
            Application.Current.MainPage = navPage;
            await Task.Delay(250);
            await navPage.FadeTo(0, 400);
            await navPage.FadeTo(1, 300);



        }
        public void CloseScreen(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
         
            ShopPopUp.IsVisible = false;
        }


        public  void ShopSetting(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
           ShopPopUp.IsVisible = true;
        }

        public void ShopNextClicked(object sender, EventArgs e)
        {
            BGMusics.Clicking_Sound().Play();
            if (ShopSmash.IsVisible)
            {
                ShopSmash.IsVisible = false;
                ShopOthers.IsVisible = true;
            }
            else
            {

                ShopSmash.IsVisible = true;
                ShopOthers.IsVisible = false;
            }
        }

        public async void Home_Clicked(object sender, EventArgs args) {
            var agree = await DisplayAlert("Leaving Already...","Do you want to quit?","Yes","No");
         
                Device.BeginInvokeOnMainThread(async () =>
                {
                     if (agree)
                    {
                        await Application.Current.MainPage.FadeTo(0, 300);
                        var navPage = new NavigationPage(new MainPage());
                        Application.Current.MainPage = navPage;
                        await Task.Delay(250);
                        await navPage.FadeTo(0, 400);
                        await navPage.FadeTo(1, 300);
                    }
                });
           
        }
        void LoadGames()
        {

            int slotsAvailbale = 3;
            Image[] SlotHolder = new Image[slotsAvailbale];
            Label[] SlotName = new Label[slotsAvailbale];
           string[] values = new string[slotsAvailbale];
            string[] slotNumber = new string[slotsAvailbale];
            bool[] agree = new bool[slotsAvailbale];
            
            TapGestureRecognizer[] recognizer = new TapGestureRecognizer[slotsAvailbale];
            for (int i = 0; i < slotsAvailbale; i++)
            {
                SaveSlots.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < slotsAvailbale; i++) {
                slotNumber[i] = "SD" + (i + 1); 
            }

            for (int i = 0; i < slotsAvailbale; i++)
            {
                SlotHolder[i] = new Image
                {
                    Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.BlackButton.png"),
                    BackgroundColor = Color.Transparent,
                    Aspect = Aspect.AspectFit
                };
                SlotName[i] = new Label { Text = "Data Slot " + (i + 1), FontFamily = "Face Off M54.ttf#Face Off M54", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), TextColor = Color.White, BackgroundColor = Color.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };

                int mark = i;
               
                recognizer[i] = new TapGestureRecognizer();
                agree[i] = false;
              
                SlotName[i].GestureRecognizers.Add(recognizer[i]);
                recognizer[i].Tapped  +=  (sender, args)  =>
                {

                    TheSlot.Text = "Save This Game in Slot " + (mark+1);
                    SavePopUp.IsVisible = true;

                };
            
                SaveSlots.Children.Add(SlotHolder[i], 0, i);
                SaveSlots.Children.Add(SlotName[i], 0, i);
            }
          

        }

        void SavingGames(int slot) {
            SaveOrLoad.SaveGameInSlot("SD" + slot,SaveName.Text,LocalSelection.DataValues().TrimEnd('-'));
        }
       
       
        void ShoppingSmash()
        {
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
        void SavingUI()
        {

            Image[] images = new Image[2];
            Label[] labels = new Label[2];
            string[] text = { "Save", "Cancel" };
            TapGestureRecognizer[] recognizers = new TapGestureRecognizer[2];
            for (int i = 0; i < 2; i++)
            {
                butons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < 2; i++)
            {

            }
            for (int i = 0; i < 2; i++)
            {
                images[i] = new Image
                {
                    Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Button.BlackButton.png"),
                    BackgroundColor = Color.Transparent,
                    Aspect = Aspect.AspectFit
                };
                labels[i] = new Label { Text = text[i], FontFamily = "Face Off M54.ttf#Face Off M54", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), TextColor = Color.White, BackgroundColor = Color.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
                recognizers[i] = new TapGestureRecognizer();
                int mark = i;
                recognizers[i].Tapped +=async (sender, args) =>
                {
                    BGMusics.Clicking_Sound().Play();
                    if (labels[mark].Text == "Save")
                    {
                        string[] texts = TheSlot.Text.Split(' ');
                        int slot = int.Parse(texts[5]);
                        SavingGames(slot);
                        await Application.Current.MainPage.FadeTo(0, 300);
                        var navPage = new NavigationPage(new MainPage());
                        Application.Current.MainPage = navPage;
                        await Task.Delay(250);
                        await navPage.FadeTo(0, 400);
                        await navPage.FadeTo(1, 300);

                    
                    }
                    else {

                        SavePopUp.IsVisible = false;
                    }
                };
                labels[i].GestureRecognizers.Add(recognizers[i]);
                butons.Children.Add(images[i],i,0);
                butons.Children.Add(labels[i], i, 0);
            }
        }

    }



}