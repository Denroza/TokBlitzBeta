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
	public partial class Sample : ContentPage
	{
        Label[,] labeled = new Label[2, 10];
       
        Label[,] BoardLabel = new Label[10, 5];
        Frame[,] frames = new Frame[10, 5];
        Frame[,] frameschoice = new Frame[2, 10];
        string[] answers;
        int getrow = 0;
        int getcoloumn = 0;
        
        TapGestureRecognizer[,] gridTap = new TapGestureRecognizer[2, 10];
        string[,] vars = new string[2, 10];
        public Sample ()
		{
			InitializeComponent ();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.InGame.png");
            // profile1.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.profile.png");
            //   profile2.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.profile.png");
            //    timer.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.timer.png");
            // board.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.screen.png");
            //      round.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.screen.png");
            //screen.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.screenblank.png");
          
                PopulateGameBoard();
              
           
                PopulateAnswer();
                choicesGrid();
            
        }

        //===============================//
        int currentcount = 0;
        int wordcount = 0;
      
        public void PopulateGameBoard()
        {
            Random rand = new Random();
            string[] word = Logic.WordGetter();


            int setsetsize;
            string[] wordwithblank = Execution.SetBlanks(word);
            string[] AllWords = Execution.GetAllWords();
            bool check1 = false;
            bool check2 = false;
            wordcount = wordwithblank.Count();
            int setfontsize = Execution.SetFontSize();
            for (int loop = 0; loop < wordcount; loop++)
            {
               
                if (AllWords[loop].Count() <=6)
                {
                    check1 = false;
                    

                }
                if (AllWords[loop].Count() >= 7 && AllWords[loop].Count() <= 11)
                {
                    check1 = true;
                }
                if (AllWords[loop].Count() >= 12) {
                    check1 = false;
                    check2 = true;
                    break;
                }
                
            }

            if (check2.Equals(true))
            {
                setsetsize = setfontsize - ((setfontsize / 3) - 1);
              

            }
            else if (check1.Equals(true) && check2.Equals(false))
            {
                setsetsize = setfontsize - (setfontsize / 2);
            }
            else 
            {
              setsetsize = setfontsize; ;
            }
            Execution.InitializeRowAndColoumn(wordcount);
            if (wordcount > 1 && wordcount < 33)
            {
             
                int setrow = Execution.SetRow();
                int setcoloumn = Execution.SetColoumn(); ;
            
            
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
                        else {

                            GameBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                        }

                    }
                    for (int coloumn = 0; coloumn < setcoloumn; coloumn++)
                    {
                        GameBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    }
                }

                else if (wordcount>3 &&wordcount<19) {
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
                            if (!wordwithblank[currentcount].Equals(""))
                            {

                           
                                BoardLabel[row, coloumn] = new Label
                                {
                                    Text = wordwithblank[currentcount],
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.Black,
                                    BackgroundColor = Color.FromRgb(138, 138, 138),
                                    FontSize = setsetsize,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                   
                                };
                                frames[row, coloumn] = new Frame
                                {
                                  
                                    BackgroundColor = Color.Transparent,
                                    
                                   
                                };
                               // BoardLabel[row, coloumn].FontFamily =  "Assets/Fonts/ProximaNovaCond-Thin.otf";
             
                            }
                            else
                            {
                                getrow = row;
                                getcoloumn = coloumn;
                                BoardLabel[row, coloumn] = new Label
                                {
                                    Text = "______",
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.Black,
                                    HorizontalTextAlignment = TextAlignment.Center
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


                            BoardLabel[row, coloumn] = new Label
                            {
                                Text = "",

                                VerticalTextAlignment = TextAlignment.End,
                                HorizontalTextAlignment = TextAlignment.Center
                            };
                         
                            frames[row, coloumn] = new Frame
                            {
                                BackgroundColor = Color.Transparent,
                                BorderColor = Color.Transparent
                            };

                        }

                  
                       GameBoard.Children.Add(frames[row, coloumn], coloumn, row);
                        GameBoard.Children.Add(BoardLabel[row, coloumn], coloumn, row);



                    }
                }
            }
            else {

             
                for (int row = 0; row < 9; row++)
                {
                    GameBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                }
                for (int coloumn = 0; coloumn < 4; coloumn++)
                {
                    GameBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }
                for (int row = 0; row < 9; row++)
                  {
                for (int coloumn = 0; coloumn < 4; coloumn++)
                {

                    if (wordcount > currentcount)
                    {
                        if (!wordwithblank[currentcount].Equals(""))
                        {


                                BoardLabel[row, coloumn] = new Label
                                {
                                    Text = wordwithblank[currentcount],
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.Black,
                                    BackgroundColor = Color.FromRgb(138, 138, 138),
                                    FontSize = setsetsize,
                                    HorizontalTextAlignment = TextAlignment.Center
                                };

                             


                            frames[row, coloumn] = new Frame
                            {

                                BackgroundColor = Color.Transparent,
                              
                              
                            };
                        }
                        else
                        {
                            getrow = row;
                            getcoloumn = coloumn;
                            BoardLabel[row, coloumn] = new Label
                            {
                                Text = "______",
                                VerticalTextAlignment = TextAlignment.Center,
                                TextColor = Color.Black,
                                HorizontalTextAlignment = TextAlignment.Center
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


                        BoardLabel[row, coloumn] = new Label
                        {
                            Text = "",

                            VerticalTextAlignment = TextAlignment.End,
                            HorizontalTextAlignment = TextAlignment.Center
                        };
                        frames[row, coloumn] = new Frame
                        {
                            BackgroundColor = Color.Transparent,
                            BorderColor = Color.Transparent
                            
                        };

                    }
                    BoardLabel[8, 3] = new Label
                    {
                        Text = "",

                        VerticalTextAlignment = TextAlignment.End,
                        HorizontalTextAlignment = TextAlignment.Center
                    };

                    frames[8, 3] = new Frame
                    {
                        BackgroundColor = Color.Transparent,
                        BorderColor = Color.Transparent
                    };



                    GameBoard.Children.Add(frames[row, coloumn], coloumn, row);
                    GameBoard.Children.Add(BoardLabel[row, coloumn], coloumn, row);


                }
            }
        }
        }
        //===========================================//
        Label[] AnswerLabel;
        int _thisanswer=0;
        public void PopulateAnswer()
        {

            answers = Execution.SetToBoardLetters();
            AnswerGetter(answers);
         
            //  CounterCheck(answers.Count());
            if (Logic.SetDifficulty().Equals("Easy"))
            {
                AnswerLabel = new Label[answers[_thisanswer].ToCharArray().Count()];
                answer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                for (int NumberOfGrid = 0; NumberOfGrid < answers[_thisanswer].ToCharArray().Count(); NumberOfGrid++)
                {
                    answer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    if (NumberOfGrid.Equals(0))
                    {
                        if (answers[_thisanswer].ToCharArray().Count().Equals(0))
                        {
                            AnswerLabel[NumberOfGrid] = new Label
                            {
                                Text = "___",
                                VerticalTextAlignment = TextAlignment.End,
                                HorizontalTextAlignment = TextAlignment.Center
                            };
                        }
                        else {
                            AnswerLabel[NumberOfGrid] = new Label
                            {
                                Text = answers[_thisanswer].ToCharArray()[_thisanswer].ToString(),
                                VerticalTextAlignment = TextAlignment.End,
                                HorizontalTextAlignment = TextAlignment.Center
                            };
                        }

                    }
                    else
                    {
                        AnswerLabel[NumberOfGrid] = new Label
                        {
                            Text = "___",
                            VerticalTextAlignment = TextAlignment.End,
                            HorizontalTextAlignment = TextAlignment.Center
                        };
                    }
                    answer.Children.Add(AnswerLabel[NumberOfGrid], NumberOfGrid, 0);
                }
            }
            if (Logic.SetDifficulty().Equals("Moderate")) {
                AnswerLabel = new Label[answers[Execution.QuestionCount()].ToCharArray().Count()];
                answer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                for (int NumberOfGrid = 0; NumberOfGrid < answers[Execution.QuestionCount()].ToCharArray().Count(); NumberOfGrid++)
                {
                    answer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    if (NumberOfGrid.Equals(Execution.QuestionCount()))
                    {
                        if (answers[Execution.QuestionCount()].ToCharArray().Count().Equals(0))
                        {
                            AnswerLabel[NumberOfGrid] = new Label
                            {
                                Text = "___",
                                VerticalTextAlignment = TextAlignment.End,
                                HorizontalTextAlignment = TextAlignment.Center
                            };
                        }
                        else
                        {
                            AnswerLabel[NumberOfGrid] = new Label
                            {
                                Text = answers[Execution.QuestionCount()].ToCharArray()[0].ToString(),
                                VerticalTextAlignment = TextAlignment.End,
                                HorizontalTextAlignment = TextAlignment.Center
                            };
                        }
                    }
                    else
                    {
                        AnswerLabel[NumberOfGrid] = new Label
                        {
                            Text = "___",
                            VerticalTextAlignment = TextAlignment.End,
                            HorizontalTextAlignment = TextAlignment.Center
                        };
                    }
                    answer.Children.Add(AnswerLabel[NumberOfGrid], NumberOfGrid, 0);
                }

            }
        }
        //===============================//
        char[] lettersdivide;
        string reserveletters;
        int here = 1;
        public void choicesGrid()
        {
           
            string[] ans = AnswerSetter();
            var children = answer.Children.ToList();
         Random rand = new Random();
            if (ans[Execution.QuestionCount()].Count().Equals(1)) {
                here = 0;
            }
         

            for (int row = 0; row < 2; row++)
            {
                for (int coloumn = 0; coloumn < 10; coloumn++)
                { vars[row, coloumn] = ""; }
            }
            if (Logic.SetDifficulty().Equals("Easy"))
            {
                
                char[] thisletters = new char[ans[Execution.QuestionCount()].Count()];
                reserveletters = ans[Execution.QuestionCount()].ToCharArray()[Execution.QuestionCount()].ToString().ToLower();
                int randomrow = rand.Next(0, 2);
                int randomcoloumn = rand.Next(0, 10);
              
                lettersdivide = answers[Execution.QuestionCount()].ToCharArray();
               
                for (int i = 0; i < lettersdivide.Count(); i++)
                {
                    bool check = true;
                    while (check)
                    {
                        if (vars[randomrow, randomcoloumn].Equals(""))
                        {
                            vars[randomrow, randomcoloumn] = lettersdivide[i].ToString().ToLower();
                            check = false;
                        }
                        else {
                            randomcoloumn = rand.Next(0, 10);
                            randomrow = rand.Next(0, 2);
                        }
                    }
                 
                    randomcoloumn = rand.Next(0, 10);
                    randomrow = rand.Next(0, 2);
                }
            }


            if (Logic.SetDifficulty().Equals("Moderate"))
            {

                char[] thisletters = new char[ans[Execution.QuestionCount()].Count()];
                reserveletters = ans[0].ToCharArray()[0].ToString().ToLower();
                int randomrow = rand.Next(0, 2);
                int randomcoloumn = rand.Next(0, 10);

                lettersdivide = answers[Execution.QuestionCount()].ToCharArray();

                for (int i = 0; i < lettersdivide.Count(); i++)
                {
                    bool check = true;
                    while (check)
                    {
                        if (vars[randomrow, randomcoloumn].Equals(""))
                        {
                            vars[randomrow, randomcoloumn] = lettersdivide[i].ToString().ToLower();
                            check = false;
                        }
                        else
                        {
                            randomcoloumn = rand.Next(0, 10);
                            randomrow = rand.Next(0, 2);
                        }
                    }

                    randomcoloumn = rand.Next(0, 10);
                    randomrow = rand.Next(0, 2);
                }
            }

            for (int row = 0; row < 2; row++)
            {
                for (int coloumn = 0; coloumn < 10; coloumn++)
                {

                 /*  if (vars[row, coloumn].Equals(""))
                   {
                        vars[row, coloumn] = Execution.RandomLetters().ToString();
                   }*/
                    labeled[row, coloumn] = new Label
                    {
                        Text = vars[row, coloumn],
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.FromRgb(42, 41, 46),
                        TextColor = Color.GhostWhite
                        
                    };
                    gridTap[row, coloumn] = new TapGestureRecognizer();
                    var getJ = row;
                    var getI = coloumn;
                    gridTap[row, coloumn].Tapped += (sender, e) =>
                    {
                        var check = vars[getJ, getI];
                        if (labeled[getJ, getI].Text.Equals("") || labeled[getJ, getI].Text.Equals(null))
                        {
                            if (AnswerLabel[here - 1].Equals(labeled[getJ, getI]))
                            {
                                AnswerLabel[here - 1].Text = "___";
                                reserveletters = reserveletters.Remove(reserveletters.Length - 1, 1);
                                labeled[getJ, getI].Text = check;
                                here--;

                            }

                        }
                        else
                        {
                            AnswerLabel[here].Text = check;
                            reserveletters += check;
                            labeled[getJ, getI].Text = "";
                            here++;

                            if (here.Equals(ans[Execution.QuestionCount()].Count()))
                            {
                                int round = Execution.CurrentRound();
                                if (!Logic.SetDifficulty().Equals("Easy")) {
                                    Execution.DoNextStage(false);
                                }
                                Execution.GetRound(round);
                                // Execution.GetRound(roundcount++);
                                if (reserveletters.ToString().Equals(ans[Execution.QuestionCount()].ToLower()))
                                {
                                    if (Execution.EnterNextStage().Equals(true))
                                    {

                                        Execution.QuestionCounter(_thisanswer);
                                        var navPage = new NavigationPage(new CorrectPage());
                                        Application.Current.MainPage = navPage;
                                    }
                                    else
                                    {
                                        
                                        if (Logic.SetDifficulty().Equals("Moderate")) {
                                            if (Execution.QuestionCount()>1) {
                                                Execution.DoNextStage(true);
                                            }
                                            if ( Execution.EnterNextStage().Equals(true)) {
                                                Execution.QuestionCounter(_thisanswer);
                                                var navPage = new NavigationPage(new CorrectPage());
                                                Application.Current.MainPage = navPage;
                                            }
                                        }
                                        answer.ColumnDefinitions.Clear();
                                        answer.RowDefinitions.Clear();
                                        foreach (var child in children)
                                        {
                                            answer.Children.Remove(child);
                                        }
                                        if (ans[Execution.QuestionCount()].Count().Equals(1))
                                        {
                                            here = 0;
                                        }
                                        else { here = 1; }
                                        choicesGrid();
                                        PopulateAnswer();
                                    }
                                }

                                else
                                {
                                    if (Execution.EnterNextStage().Equals(true))
                                    {
                                        Execution.QuestionCounter(_thisanswer);

                                        var navPage = new NavigationPage(new WrongPage());
                                        Application.Current.MainPage = navPage;

                                    }
                                    else
                                    {

                                    }
                                }
                                  
                              

                            }
                          

                        }

                    };


                    //       }
                    labeled[row, coloumn].GestureRecognizers.Add(gridTap[row, coloumn]);

                    choices.Children.Add(labeled[row, coloumn], coloumn, row);

                }

            }

        }
        //===============================//
        private void ResetAll() {

            
        }
       

        //==================///

        static string checke;
        public static string CounterCheck(string check)
        {
            checke = check;
            Countered();
            return checke;

        }
        public static string Countered()
        {
            return checke;
        }
        static string[] getter;
        public static string[] AnswerGetter(string[] answer)
        {
            getter = answer;
            AnswerSetter();
            return getter;
        }
        public static string[] AnswerSetter()
        {

            return getter;
        }



        //=======================================///
        private async void Button_Clicked(object sender, EventArgs e)
        {
            for (int i =0;i<AnswerSetter().Count();i++) {
                await DisplayAlert("Words",AnswerSetter()[i], "OK");
            }
                /*
              var navPage = new NavigationPage(new Sample());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;
            */
        }
        //=======================================///
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            /*
            var navPage = new NavigationPage(new GameType());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;
            */
            }

    }
}