using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Lottie.Forms;

namespace TokBlitzBeta.GamePlay
{
    public class Game
    {
        static string[] ListOfWordWithBlank;
        static string[] ListOfWordsToBeAnswered;
        static string[] AllWords;
        static bool diditloop = false;
        static string FirstWord = "";
        // static string SecondWord = "";
        //  static string ThirdWord = "";
        static string[] ExludedWords = { "this", "that", "the", "then", "of", "i", "for", "and", "i'll" };
       static char[] trimmer = new char[] { ' ', '!', ',', '.', '/', '?', '"', ':', ';' };

        static string[] splitter = new string[] {" ","/" };
        static Random random = new Random();
        static Random randforletters = new Random();
        public  static string[] SetBlanks(string[]words)
        {

           // AllWords = words;
            int difficulty = Flow.DifficultyIdentifier();
            string[] wordlist = words;
            ListOfWordsToBeAnswered = new string[1];
            ListOfWordWithBlank = new string[wordlist.Length];
            int easy = random.Next(0, wordlist.Length-1);
            string[] trimedwords = new string[wordlist.Length];
            
            for (int triming = 0; triming < wordlist.Length; triming++)
            {
                trimedwords[triming] = wordlist[triming].Trim(trimmer);
            }
            int infichecker = 0;
            if (!Flow.SetPlayerMode() && !Flow.GameInCategory())
            {
                bool IsExcluded = true;
                switch (difficulty)
                {
                    case 1:
                        while (trimedwords[easy].Length >= 5 || trimedwords[easy].Length <= 2 || trimedwords[easy].Length.Equals(1) || IsExcluded)
                        {
                            infichecker++;
                            if (infichecker.Equals(50))
                            {
                               
                                diditloop = true;
                            }
                            else
                            {
                                diditloop = false;
                            }
                            for (int i =0; i<ExludedWords.Length;i++) {
                                if (trimedwords[easy].ToLower() == ExludedWords[i].ToLower())
                                {
                                    IsExcluded = true;
                                }
                                else {
                                    IsExcluded = false;
                                }
                              
                            }
                            easy = random.Next(0, wordlist.Length);
                        }
                        break;
                    case 2:
                        while (trimedwords[easy].Length <= 4 || trimedwords[easy].Length >= 7 || trimedwords[easy].Length.Equals(1) || IsExcluded)
                        {
                            infichecker++;
                            if (infichecker.Equals(50))
                            {
                                
                                diditloop = true;
                            }
                            else
                            {
                                diditloop = false;
                            }
                            for (int i = 0; i < ExludedWords.Length; i++)
                            {
                                if (trimedwords[easy].ToLower() == ExludedWords[i].ToLower())
                                {
                                    IsExcluded = true;
                                }
                                else
                                {
                                    IsExcluded = false;
                                }

                            }
                            easy = random.Next(0, wordlist.Length);

                        }
                        break;
                    case 3:
                        while (trimedwords[easy].Length <= 6 || trimedwords[easy].Length >= 10 || trimedwords[easy].Length.Equals(1) || IsExcluded)
                        {
                            infichecker++;
                            if (infichecker.Equals(50))
                            {
                            
                                diditloop = true;
                            }
                            else
                            {
                                diditloop = false;
                            }

                            for (int i = 0; i < ExludedWords.Length; i++)
                            {
                                if (trimedwords[easy].ToLower() == ExludedWords[i].ToLower())
                                {
                                    IsExcluded = true;
                                }
                                else
                                {
                                    IsExcluded = false;
                                }

                            }
                            easy = random.Next(0, wordlist.Length);

                        }
                        break;
                    case 4:
                        while (trimedwords[easy].Length <= 8 || trimedwords[easy].Length >= 13 || trimedwords[easy].Length.Equals(1) || IsExcluded)
                        {
                            infichecker++;
                            if (infichecker.Equals(50))
                            {
                                
                                diditloop = true;
                            }
                            else
                            {
                                diditloop = false;
                            }
                            for (int i = 0; i < ExludedWords.Length; i++)
                            {
                                if (trimedwords[easy].ToLower() == ExludedWords[i].ToLower())
                                {
                                    IsExcluded = true;
                                }
                                else
                                {
                                    IsExcluded = false;
                                }

                            }
                            easy = random.Next(0, wordlist.Length);

                        }
                        break;


                }

            }
            else
            {
                bool IsExcluded = true;
                while (trimedwords[easy].Length <= 2 || trimedwords[easy].Length >= 13 || trimedwords[easy].Length.Equals(1) || IsExcluded )
                {
                    infichecker++;
                    if (infichecker.Equals(50))
                    {
                        Console.WriteLine("Error found in selecting word: Infinite Loop");
                        break;
                    }
                   
                    for (int i = 0; i < ExludedWords.Length; i++)
                    {
                        if (trimedwords[easy].ToLower() == ExludedWords[i].ToLower())
                        {
                            IsExcluded = true;
                        }
                        else
                        {
                            IsExcluded = false;
                        }

                    }
                    easy = random.Next(0, wordlist.Length);


                }
            }



            for (int i = 0; i < wordlist.Length; i++)
            {
                //   int j = 0;

                if (i.Equals(easy))
                {
                    ListOfWordWithBlank[i] = "";
                    ListOfWordsToBeAnswered[0] = wordlist[i].Trim(trimmer);
                    FirstWord = ListOfWordsToBeAnswered[0];
                  
                }
                else
                {
                    ListOfWordWithBlank[i] = wordlist[i];

                }


            }

            for (int i = 0; i < wordlist.Length; i++)
            {


                if (ListOfWordsToBeAnswered[0].ToLower().Equals(wordlist[i].ToLower()))
                {
                    ListOfWordWithBlank[i] = "";

                }

            }
            EnteredInfiniteLoop();
            SetToBoardLetters();
            SetFirstWord();
            
            return ListOfWordWithBlank;

        }

        public static bool EnteredInfiniteLoop() {
            return diditloop;
        }
        public static string[] SetToBoardLetters()
        {
            return ListOfWordsToBeAnswered;
        }
        public  static string SetFirstWord()
        {
           
         
            return FirstWord;

        }
        public static char RandomLetters()
        {

            int randit = randforletters.Next(0, 26);
            char letter = (char)('a' + randit);

            return letter;
        }
        public static int GuessRemaing(int wordcount) {
            int guessp=0;
            switch (wordcount) {
                case 2: guessp = 2;break;
                case 3: guessp = 3;  break;
                case 4: guessp = 4; break;
                default: guessp = 5; break;
            }

            return guessp;

        }
        static int smash = 20;
        public static void DeductSmash(int quantity) {
            smash -= quantity;
            SmashCounter();
        }
        public static void AddSmash(int quantity)
        {
            smash += quantity;
            SmashCounter();
        }
        public static void DefaultSmash()
        {
            smash = 20;
            SmashCounter();
        }
        public static int SmashCounter() {
           
            return smash;
        }
        //==============//
       public static AnimationView LightningAnimation()
        {
            AnimationView view = new AnimationView();
            view.Animation = "lightning.json";
            return view;
        } 

    }
}
