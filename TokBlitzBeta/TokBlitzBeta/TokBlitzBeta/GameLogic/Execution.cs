using System;
using System.Collections.Generic;
using System.Text;
using TokBlitzBeta.GameLogic;
using TokBlitzBeta.Model;
using System.Threading.Tasks;
namespace TokBlitzBeta.GameLogic
{
    public class Execution
    {
        static string[] ListOfWordWithBlank;
        static string[] ListOfWordsToBeAnswered;
        static string[] AllWords;
        static Random random = new Random();
        static Random randforletters = new Random();
        public static string[] SetBlanks(string[] word)
        {

            AllWords = word;
            string[] wordlist = word;
            //     string[] bannedwords = {"are","a","was","the","is","am","do","does","i","for" };
            if (Logic.SetDifficulty() == "Easy")
            {

                ListOfWordsToBeAnswered = new string[1];
                ListOfWordWithBlank = new string[wordlist.Length];
                int easy = random.Next(0, wordlist.Length);
             
                for (int i = 0; i < wordlist.Length; i++)
                {
                    //   int j = 0;

                    if (i.Equals(easy))
                    {
                        ListOfWordWithBlank[i] = "";
                        ListOfWordsToBeAnswered[0] = wordlist[i];
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
            }
            else if (Logic.SetDifficulty() == "Moderate")
            {

                ListOfWordsToBeAnswered = new string[2];
                ListOfWordWithBlank = new string[wordlist.Length];
                int b1 = random.Next(0, wordlist.Length);
                int b2 = random.Next(0, wordlist.Length);
                while (b1.Equals(b2) || b2.Equals(b1 - 1) || b2.Equals(b1 + 1))
                {
                    b2 = random.Next(0, wordlist.Length);
                }
                for (int i = 0; i < wordlist.Length; i++)
                {



                    if (i.Equals(b1))
                    {
                        ListOfWordWithBlank[i] = "";
                        ListOfWordsToBeAnswered[0] = wordlist[i];

                    }
                    else if (i.Equals(b2))
                    {

                        ListOfWordWithBlank[i] = "";
                        ListOfWordsToBeAnswered[1] = wordlist[i];
                    }
                    else
                    {
                        ListOfWordWithBlank[i] = wordlist[i];

                    }


                }
            }
            else
            {

                ListOfWordsToBeAnswered = new string[3];
                ListOfWordWithBlank = new string[wordlist.Length];
                int b1 = random.Next(0, wordlist.Length);
                int b2 = random.Next(0, wordlist.Length);
                int b3 = random.Next(0, wordlist.Length);
                while (b2.Equals(b1) || b2.Equals(b1 - 1) || b2.Equals(b1 + 1) || b2.Equals(b3) || b2.Equals(b3 - 1) || b2.Equals(b3 + 1))
                {
                    b2 = random.Next(0, wordlist.Length);
                }
                while (b3.Equals(b1) || b3.Equals(b1 - 1) || b3.Equals(b1 + 1) || b3.Equals(b2) || b3.Equals(b2 - 1) || b3.Equals(b2 + 1))
                {
                    b3 = random.Next(0, wordlist.Length);
                }

                for (int i = 0; i < wordlist.Length; i++)
                {


                    if (i.Equals(b1))
                    {
                        ListOfWordWithBlank[i] = "";
                        ListOfWordsToBeAnswered[0] = wordlist[i];

                    }
                    else if (i.Equals(b2))
                    {

                        ListOfWordWithBlank[i] = "";
                        ListOfWordsToBeAnswered[1] = wordlist[i];
                    }
                    else if (i.Equals(b3))
                    {

                        ListOfWordWithBlank[i] = "";
                        ListOfWordsToBeAnswered[2] = wordlist[i];
                    }
                    else
                    {
                        ListOfWordWithBlank[i] = wordlist[i];

                    }


                }



            }

            SetToBoardLetters();
            GetAllWords();
            return ListOfWordWithBlank;

        }

        public static string[] SetToBoardLetters()
        {

            return ListOfWordsToBeAnswered;
        }

        public static char RandomLetters()
        {

            int randit = randforletters.Next(0, 26);
            char letter = (char)('a' + randit);

            return letter;
        }





        static int setrow = 0;
        static int setcoloumn = 0;

        static int fontsize = 30;
        public static void InitializeRowAndColoumn(int wordcount)
        {
            fontsize = 14;
            if (Logic.SetDifficulty().Equals("Easy"))
            {
                if (wordcount <= 3)
                {
                  
                    setrow = 1;
                    setcoloumn = 3;
                    fontsize = 33;
                 

                }
                if (wordcount >= 4 && wordcount <= 6)
                {
                    setrow = 2;
                    setcoloumn = 3;
                    fontsize = 30;
                 
                }
                if (wordcount >= 7 && wordcount <= 9)
                {
                    setrow = 3;
                    setcoloumn = 3;
                    fontsize = 30;
                 
                }
            }
            if (Logic.SetDifficulty().Equals("Moderate"))
            {
                if (wordcount >= 10 && wordcount <= 12)
                {
                    setrow = 4;
                    setcoloumn = 3;
                    fontsize = 21;
                 
                }
                if (wordcount >= 13 && wordcount <= 15)
                {
                    setrow = 5;
                    setcoloumn = 3;
                    fontsize = 21;
                   
                }
                if (wordcount >= 16 && wordcount <= 18)
                {
                    setrow = 6;
                    setcoloumn = 3;
                    fontsize = 21;
                }
                if (wordcount >= 19 && wordcount <= 21)
                {
                    setrow = 7;
                    setcoloumn = 3;
                    fontsize = 21;
                }
            }
            else
            {
                if (wordcount >= 22 && wordcount <= 24)
                {
                    setrow = 8;
                    setcoloumn = 3;
                    fontsize = 18;
                }
                if (wordcount >= 25 && wordcount <= 28)
                {
                    setrow = 7;
                    setcoloumn = 4;
                    fontsize = 18;
                }
                if (wordcount >= 29 && wordcount <= 32)
                {
                    setrow = 8;
                    setcoloumn = 4;
                    fontsize = 18;
                }
            }
            /*if (wordcount > 32 && wordcount < 36)
            {
                setrow = 9;
                setcoloumn = 4;

            }
            */
            SetRow();
            SetColoumn();
            SetFontSize();
          
        }

        public static int SetRow()
        {
            return setrow;
        }
        public static int SetColoumn()
        {
            return setcoloumn;
        }
        public static int SetFontSize()
        {

            return fontsize;
        }
  
        public static string[] GetAllWords() {

            return AllWords;
        }

        public static int TotalRound() {
            int cur = 0;
            if (Logic.SetDifficulty().Equals("Easy"))
            {
                cur = 10;
            }
            else {
                cur = 5;
            }

            return cur;
        }
        static int val;
        public static void GetRound(int round) {
            val = round + 1;
           
            CurrentRound();

        }
        public static int CurrentRound() {
            return val;
        }
        public static int ResetRound() {
            return val = 1;
        }

        static int questionCount ;
        public static int QuestionCounter(int question) {
            questionCount = question + 1;
          
            return questionCount;
        }

        public static int QuestionCount() {

            return questionCount;
        
        }
        public static int QuestionCountReset() {
            questionCount = 0;
            return questionCount;
        }

        static bool _continue =true;
        public static bool DoNextStage(bool _do) {
            _continue = _do;
            EnterNextStage();
            return _continue;

        }
        public static bool EnterNextStage() {
            return _continue;
        }
       static bool recall = true;
        public static bool BoardRecaller(bool call) {
            RecallBoard();
            return recall = call;

        }
        public static bool RecallBoard() {
            return recall;
        }
         




    }
}
