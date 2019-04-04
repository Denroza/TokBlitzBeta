using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using TokBlitzBeta.GamePlay;
using TokBlitzBeta.Model;
namespace TokBlitzBeta.GamePlay
{
    public class Flow
    {

        //Game Type Initializer//
        private static int type;
        private static int selected;
        private static int star;
        private static bool Started = false;
        private static bool IsMultiPlayer = false;
        private static string MulMode;
        private static bool DidGameStarted = false;
        private static bool IsCategory = false;
        public static bool IsTesting = false;
        public static void IsItInTestMode(bool IsIt)
        {
            IsTesting = IsIt;
            InTestMode();
        }
        public static bool InTestMode()
        {
            return IsTesting;
        }

        public static bool IsTest = false;
        public static void IsItInTestMax(bool IsIt)
        {
            IsTest = IsIt;
            InTestMax();
        }
        public static bool InTestMax()
        {
            return IsTest;
        }


        public static void IsGameInCategory(bool mode)
        {
            IsCategory = mode;
            GameInCategory();
        }
        public static bool GameInCategory()
        {
            return IsCategory;
        }
        public static void DidTheGameStart(bool DidIt)
        {
            DidGameStarted = DidIt;
        }
        public static bool YesItStartedAlready()
        {
            return DidGameStarted;
        }
        public static void GetPlayerMode(bool mode)
        {
            IsMultiPlayer = mode;
            SetPlayerMode();
        }
        public static bool SetPlayerMode()
        {
            return IsMultiPlayer;
        }
        public static void GetMultiplayerMode(string mode)
        {
            MulMode = mode;
            SetMultiplayerMode();
        }
        public static string SetMultiplayerMode()
        {
            return MulMode;
        }
        public static void TypeSetter(int gametype)
        {
            type = gametype;
            SetGameType();
        }
        public static int SetGameType()
        {
            return type;
        }
        public static void GameSelection(int gameselect)
        {

            selected = gameselect;
            GameSelected();
        }
        public static int GameSelected()
        {
            return selected;
        }
        //==========================//
        public static int GameTypeSetter()
        {
            //    int TheType = SetGameType();
            int TheSelected = GameSelected();
            int selected = 0;
            switch (TheSelected)
            {
                case 1:
                    selected = 1;
                    break;
                case 2:
                    selected = 2;
                    break;
                default:

                    break;

            }
            return selected;
        }
        //Difficulty Setter
        public static void StarSetter(int level)
        {
            star = level;
            SetStar();
        }
        public static int SetStar()
        {
            return star;
        }
        /* public static int DifficultyIdentifier() {
             int staring = SetStar();
             int difficulty = 0;
             switch (staring) {
                 case 1:
                     difficulty = 1;
                     break;
                 case 2:
                     difficulty = 2;
                     break;
                 case 3:
                     difficulty = 3;
                     break;
                 case 4:
                     difficulty = 4;
                     break;

             }
             return difficulty;
         }*/
        public static int DifficultyIdentifier()
        {
            Random random = new Random();

            int staring = SetStar();
            int difficulty = 0;
            if (!GameInCategory())
            {
                switch (staring)
                {
                    case 1:
                        difficulty = 1;
                        break;
                    case 2:
                        difficulty = 2;
                        break;
                    case 3:
                        difficulty = 3;
                        break;
                    case 4:
                        difficulty = 4;
                        break;

                }
            }
            else
            {
                difficulty = random.Next(1, 5);
            }
            return difficulty;
        }
        static int val = 0;
        public static void GetRound(int round)
        {
            if (Flow.SetPlayerMode()) { val = round + 1; } else { val = round + 1; }


            CurrentRound();

        }
        public static int CurrentRound()
        {

            return val;
        }

        static int val1 = 0;
        public static void GetRound1(int round)
        {
            if (Flow.SetPlayerMode()) { val1 = round + 1; } else { val1 = round + 1; }


            CurrentRound1();

        }
        public static int CurrentRound1()
        {

            return val1;
        }

        public static int ResetRound()
        {
            return val = 0;
        }

        static int questionCount = 0;
        public static int QuestionCounter(int question)
        {
            questionCount = question + 1;
            QuestionCount();
            return questionCount;
        }

        public static int QuestionCount()
        {

            return questionCount;

        }
        public static int QuestionCountReset()
        {
            questionCount = 0;
            return questionCount;
        }

        public static void GameStarting(bool start)
        {
            Started = start;
            GameStarted();
        }
        public static bool GameStarted()
        {
            return Started;
        }
        static MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();

        public static async void ReadyToksForPlay(string group)
        {
            TokGamesApiClient apiClient = new TokGamesApiClient();
            try
            {

                await apiClient.GetGameToksAsync(new GameToksQuery() { tok_group = group, count = -1 });

            }
            catch (NullReferenceException e)
            {
                await apiClient.GetGameToksAsync(new GameToksQuery() { tok_group = group, count = -1 });
            }

            for (int i = 0; i < TokGamesApiClient.SetGameToks().Count; i++)
            {

                main.OnlineQoutes.Add(new OnlineQoutes()
                {
                    QouteID = TokGamesApiClient.SetGameToks()[i].Id,
                    QoutePhrase = TokGamesApiClient.SetGameToks()[i].PrimaryFieldText,
                    QoutesSource = TokGamesApiClient.SetGameToks()[i].SecondaryFieldText,
                    Category = TokGamesApiClient.SetGameToks()[i].Category
                });
            }
            Selection.GetOnlineList(main.OnlineQoutes);

        }

    }
}
