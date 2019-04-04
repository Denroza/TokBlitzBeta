using System;
using System.Collections.Generic;
using System.Text;

namespace TokBlitzBeta.GamePlay
{
    public class PointSystem
    {
        static int score = 0;
        //static int mistakescore;
        static int roundscore = 0;
        static int guess;
        public static void GuessCounter(int wordcount)
        {
            if (wordcount.Equals(2))
            {
                guess = 1;
            }
            else if (wordcount.Equals(3))
            {
                guess = 2;
            }
            else if (wordcount.Equals(4))
            {
                guess = 3;
            }
            else
            {

                guess = 4;
            }
            CountOfGuesses();

        }
        public static void PointCount(int gotscore, int mistake, int wordcount)
        {

            if (wordcount.Equals(2))
            {
                switch (mistake) {
                    case 0: gotscore = 100; break;
                    case 1: gotscore = 0;break;
                    default: gotscore = 0;break;
                }
            }
            else if (wordcount.Equals(3))
            {
                switch (mistake)
                {
                    case 0: gotscore = 100; break;
                    case 1: gotscore = 75; break;
                    case 2: gotscore = 50; break;
                    default: gotscore = 0; break;
                }
            }
            else if (wordcount.Equals(4))
            {
                switch (mistake)
                {
                    case 0: gotscore = 100; break;
                    case 1: gotscore = 50; break;
                    case 2: gotscore = 25; break;
                    default: gotscore = 0; break;
                }
            }
            else
            {
                switch (mistake)
                {
                    case 0: gotscore = 100; break;
                    case 1: gotscore = 75; break;
                    case 2: gotscore = 50; break;
                    case 3: gotscore = 25; break;
                    default: gotscore = 0; break;
                }
            }

            if (gotscore < 0)
            {
                gotscore = 0;
            }
            roundscore = gotscore;
            score += gotscore;
            ShowRoundScore();
            TotalScore();
        }
        public static int ShowRoundScore()
        {
            return roundscore;
        }
        public static int TotalScore()
        {
            return score;
        }
        public static int CountOfGuesses()
        {
            return guess;
        }
        public static int ResetScore()
        {
            score = 0;

            return score;
        }
    }
}
