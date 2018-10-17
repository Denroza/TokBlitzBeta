using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokBlitzBeta.MainViewModel;
using TokBlitzBeta.Model;
using TokBlitzBeta.GameLogic;

namespace TokBlitzBeta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SinglePlayerInGame : ContentPage
	{
        Label[,] labeled = new Label[2, 10];
       
        Label[,] BoardLabel = new Label[10,5];
        Frame[,] frames = new Frame[10,5];
        Frame[,] frameschoice = new Frame[2, 10];
         string[] answers;
        int getrow=0;
        int getcoloumn=0;
        TapGestureRecognizer[,] gridTap = new TapGestureRecognizer[2, 10];
        string[,] vars = new string[2, 10];
        public SinglePlayerInGame ()
		{
			InitializeComponent ();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bgSP.png");
            //   profile.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Sprites.profile.png");
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
    
        

           string[] wordwithblank = Execution.SetBlanks(word);
            wordcount = wordwithblank.Count();
            int sizefont = Execution.SetFontSize();
            for (int row =1;row<10;row++) {
                for (int coloumn = 1;coloumn<5;coloumn++) {
   
                     if (wordcount>currentcount) {
                        if (!wordwithblank[currentcount].Equals(""))
                        {
                            if (wordwithblank[currentcount].Count() < 10)
                            {
                                BoardLabel[row, coloumn] = new Label
                                {
                                    Text = wordwithblank[currentcount],
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.Black,
                                    FontSize = sizefont,
                                   HorizontalTextAlignment = TextAlignment.Center
                               }; 
                          }
                   else {

                                BoardLabel[row, coloumn] = new Label
                                {
                                    Text = wordwithblank[currentcount],
                                    VerticalTextAlignment = TextAlignment.Center,
                                    TextColor = Color.Black,
                                    FontSize = sizefont - 10 ,
                                    HorizontalTextAlignment = TextAlignment.Center
                                };


                         }


                            frames[row, coloumn] = new Frame
                            {

                                BackgroundColor = Color.Transparent,
                                BorderColor = Color.Black
                            };
                        }
                        else {
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
                 else{


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
                BoardLabel[9,4] = new Label
                {
                    Text = "",

                    VerticalTextAlignment = TextAlignment.End,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                    frames[9, 4] = new Frame
                    {
                        BackgroundColor = Color.Transparent,
                        BorderColor = Color.Transparent
                    };



                    GameBoard.Children.Add(frames[row,coloumn], coloumn, row);
               GameBoard.Children.Add(BoardLabel[row, coloumn], coloumn, row);


                }
            }
        }
        //===========================================//
        Label[] AnswerLabel;
        public void PopulateAnswer()
        {
          
             answers = Execution.SetToBoardLetters();
            AnswerGetter(answers);
          //  CounterCheck(answers.Count());
            if (answers.Count() == 1) {
                AnswerLabel = new Label[answers[0].ToCharArray().Count()];
                answer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                for (int NumberOfGrid = 0; NumberOfGrid < answers[0].ToCharArray().Count(); NumberOfGrid++)
                {
                    answer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    AnswerLabel[NumberOfGrid] = new Label
                    {
                        Text = "___",
                        VerticalTextAlignment = TextAlignment.End,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    answer.Children.Add(AnswerLabel[NumberOfGrid], NumberOfGrid, 0);
                }
            }
        }
        //===============================//
        char[] lettersdivide;
        string reserveletters;
        public  void choicesGrid()
        {
           
            string[] ans = AnswerSetter();
        //   CounterCheck(ans[0].ToString());
            Random rand = new Random();
            int here = 0;
          
            for (int row = 0; row < 2; row++)
            {
                for (int coloumn = 0; coloumn < 10; coloumn++)
                { vars[row, coloumn] = ""; }
            }
            if (ans.Count().Equals(1)) {
                char[] thisletters = new char[ans[0].Count()];
                reserveletters = "";
                   int randomrow = rand.Next(0, 2);
                   int randomcoloumn = rand.Next(0, 10);
           //     CounterCheck(randomrow.ToString());
                lettersdivide = answers[0].ToCharArray();
               
                    for (int i=0;i< lettersdivide.Count();i++) {

                      while (vars[randomrow, randomcoloumn].Equals(""))
                    {
                        vars[randomrow, randomcoloumn] = lettersdivide[i].ToString().ToLower();
                        
                    }
                    randomcoloumn = rand.Next(0, 10);
                    randomrow = rand.Next(0, 2);
                }
            }

          
                for (int row = 0; row < 2; row++)
                    {
                for (int coloumn = 0; coloumn < 10; coloumn++)
                {

                    if (vars[row, coloumn].Equals(""))
                    {
                        vars[row, coloumn] = Execution.RandomLetters().ToString();
                    }
                    labeled[row, coloumn] = new Label
                    {
                        Text = vars[row, coloumn],
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    gridTap[row, coloumn] = new TapGestureRecognizer();
                    var getJ = row;
                    var getI = coloumn;
                    gridTap[row, coloumn].Tapped += (sender, e) =>
                    {
                        var check = vars[getJ, getI];
                        if (labeled[getJ, getI].Text.Equals("") || labeled[getJ, getI].Text.Equals(null))
                        {
                            if ( AnswerLabel[here - 1].Equals(labeled[getJ, getI])) {
                                AnswerLabel[here - 1].Text = "___";
                                reserveletters = reserveletters.Remove(reserveletters.Length-1,1);
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
                            
                            if (here.Equals(ans[0].Count())) {
                                if (reserveletters.ToString().Equals(ans[0].ToLower()))
                                {
                                    var navPage = new NavigationPage(new CorrectPage());
                                    Application.Current.MainPage = navPage;
                                }
                                else {
                                    var navPage = new NavigationPage(new WrongPage());
                                    Application.Current.MainPage = navPage;
                                }
                               
                            }

                        }

                        //  DisplayAlert("Value", check, "OK");
                    };


             //       }
                    labeled[row, coloumn].GestureRecognizers.Add(gridTap[row, coloumn]);
               
                 choices.Children.Add(labeled[row, coloumn], coloumn, row);
                    
                }

            }

        }
        //===============================//
        static string checke;
        public static string CounterCheck(string check) {
            checke = check;
            Countered();
            return checke;

        }
        public static string Countered() {
            return checke;
        }
        static string[] getter;
        public static string[] AnswerGetter(string[] answer) {
            getter = answer;
            AnswerSetter();
            return getter;
        }
        public static string[] AnswerSetter() {

            return getter;
        }



        //=======================================///
        private async void Button_Clicked(object sender, EventArgs e)
        {

            for (int i=0; i< lettersdivide.Count();i++) {
                await DisplayAlert("Check", lettersdivide[i].ToString(), "OKS");
            }
           await DisplayAlert("Check", Countered().ToString(), "OKS");
            /*       var navPage = new NavigationPage(new SinglePlayerInGame());
            await Task.Delay(200);
            await navPage.FadeTo(0, 250);
            await Task.Delay(200);
            await navPage.FadeTo(1, 250);
            Application.Current.MainPage = navPage;*/

        }
        //=======================================///
        private async void Button_Clicked_1(object sender, EventArgs e)
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