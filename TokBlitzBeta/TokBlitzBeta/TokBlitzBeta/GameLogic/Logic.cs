using System;
using System.Collections.Generic;
using System.Text;
using TokBlitzBeta.MainViewModel;
using System.Linq;
using TokBlitzBeta.Model;
using System.Threading.Tasks;

namespace TokBlitzBeta.GameLogic
{
    class Logic
    {
       
   
       static bool Multiplayer = true;
        static string SelectedGameType;
        static string SelectTokGroup;
        static string SelectCategory;
        static string Difficulty;
        public static void isMultiplayer(bool MorS) {
            Multiplayer = MorS;
            
        }
        public static bool setPlayers() {
            return Multiplayer;
        }

        public static void GetGameType(string type) {
            SelectedGameType = type;
        }

        public static string SelectedGametype() {

            return SelectedGameType;
        }

        public static void GetTokGroup(string group) {

            SelectTokGroup = group;
        }
        public static string SelectedTokGroup() {

            return SelectTokGroup;
        }


        public static void GetCategory(string category)
        {

            SelectCategory = category;
        }
        public static string SelectedCategory()
        {

            return SelectCategory;
        }

       
        public static void GetDifficulty(string difficulty) {
            Difficulty = difficulty;
        }
        public static string SetDifficulty() {

            return Difficulty;

        }
       static Random random;
        public static int PhraseRandomizer() {
             random = new Random();
             int easy = random.Next(2, 10);
             int moderate = random.Next(10, 22);
             int challenging = random.Next(22, 36);

            
            if (SetDifficulty().Equals("Easy"))
            {

                return easy;
            }
            else if (SetDifficulty().Equals("Moderate"))
            {

                return moderate;

            }
            else
                return challenging;

        }

   
     static  string[] listofphrase;
      public static int NumberOfPhraseInTheSelectedDifficulty() {
            
          MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();
          int numberofword = PhraseRandomizer();
            var phrase = main.Qoutes.Where(qoute => qoute.WordCount.Equals(numberofword)).ToList();
            while (phrase.Count.Equals(0)) {
                  numberofword =  PhraseRandomizer();
                phrase = main.Qoutes.Where(qoute => qoute.WordCount.Equals(numberofword)).ToList();
            }
         
            listofphrase = new string[phrase.Count];
            int i = 0;
            foreach (var val in phrase) {
                listofphrase[i] = val.TheQoute;
                i++;
            }
          
            return listofphrase.Length;
      }
        public static int NumberOfPhraseCounted() {

            return NumberOfPhraseInTheSelectedDifficulty();
        }

         static string gotphrase;
       static string[] listofwords;

        public static string[] Listofwords { get => listofwords; set => listofwords = value; }

        public static string PhraseGetter() {
            MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();
            int gotcount = NumberOfPhraseCounted();
     int ramdomized = random.Next(0, gotcount);
            gotphrase = listofphrase[ramdomized];

            return gotphrase ;
        }
 
       public static string[] WordGetter() {
            MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();
            string word = PhraseGetter();
            string[] ok = word.Split(' ');

            return ok;
       }

        /*
    public static void PhraseSetGame() {



    }*/


    }


}
