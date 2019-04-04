using System;
using System.Collections.Generic;
using System.Text;
using TokBlitzBeta.GamePlay;
using System.Linq;
using TokBlitzBeta.MainViewModel;
using Plugin.Connectivity;
using System.Threading.Tasks;
using TokBlitzBeta.Model;
namespace TokBlitzBeta.GamePlay
{
    public class Selection
    {
        static Random random;
        static string type;
        static string[] AllWords;
        static string[] Listed;
        static int phraselect = 0;

        static int[] IdBank = new int[6];
        static bool diditloop = false;
        static string OnlineID;
        static string[] splitted;
        static List<OnlineQoutes> onlineQoutes = new List<OnlineQoutes>();
        static string TheFirstWord;

        public static void GetListedPhrase(string[] listed)
        {
            Listed = listed;
            SetListPhrase();
        }
        public static string[] SetListPhrase()
        {
            return Listed;
        }
        public static void GetOnlineList(List<OnlineQoutes> toks)
        {
            onlineQoutes = toks;
            SetOnlineQoutes();
        }
        public static void GetOnlineID(string id)
        {
            OnlineID = id;
            SetOnlineID();
        }
        public static string SetOnlineID()
        {
            return OnlineID;
        }
        public static List<OnlineQoutes> SetOnlineQoutes()
        {
            return onlineQoutes;
        }
        public static string[] OnlineSplittedWords(string words)
        {

            splitted = words.Split(' ');

            GetOnlineSplittedWords();
            return splitted;
        }
        public static string[] GetOnlineSplittedWords()
        {

            return splitted;
        }
        public static void GetOnlineFirstWord(string FirstWord)
        {

            TheFirstWord = FirstWord;
            SetOnlineFirstWord();
        }
        public static string SetOnlineFirstWord()
        {

            TheFirstWord = Game.SetFirstWord();
            return TheFirstWord;
        }
        public static void OnlineSelectionStart1()
        {



            var data = SetListPhrase();
            random = new Random();

            int randomizer = random.Next(0, data.Count());
            var phraseselected = data[randomizer];
            string[] SplittedPhrase = phraseselected.Split(' ');
            GetThisPhrase(phraseselected);
            bool IsChecking = true;
            while (IsChecking)
            {
                IsChecking = false;
                while (!PhraseCountChecker(phraseselected.Count()))
                {
                    randomizer = random.Next(0, data.Count());
                    phraseselected = data[randomizer];
                    SplittedPhrase = phraseselected.Split(' ');
                    IsChecking = true;
                }
                while (!WordCounter(SplittedPhrase))
                {
                    randomizer = random.Next(0, data.Count());
                    phraseselected = data[randomizer];
                    SplittedPhrase = phraseselected.Split(' ');
                    IsChecking = true;
                }
            }
            OnlineSplittedWords(phraseselected);

        }
        static string[] UsedID = new string[5];
        public static bool DuplicatePhrase(string id)
        {
            bool IsDuplicate = false;

            for (int i = 0; i < 5; i++)
            {
                if (id == UsedID[i])
                {
                    IsDuplicate = true;
                    break;
                }
            }
            if (IsDuplicate == false)
            {
                UsedID[Flow.CurrentRound()] = id;
            }
            return IsDuplicate;
        }
        public static bool PhraseCountChecker(int wordcount)
        {
            int max = 12;
            bool IsCorrectCount = false;
            int difficulty = Flow.DifficultyIdentifier();
            switch (difficulty)
            {
                case 1: max = 15; break;
                case 2: max = 24; break;
                case 3: max = 28; break;
                case 4: max = 28; break;

            }
            if (wordcount >= 3 && wordcount <= max)
            {
                IsCorrectCount = true;
            }

            return IsCorrectCount;
        }
        public static bool HasEnoughWordsToBeAnswered(string[] words)
        {
            bool Yes = false;
            int difficulty = Flow.DifficultyIdentifier();
            int WordsAvailableForUse = 0;
            string[] ExludedWords = { "this", "that", "the", "then", "of", "i", "for", "and", "i'll" };
            switch (difficulty)
            {
                case 1:
                    for (int i = 0; i < words.Count(); i++)
                    {
                        if (words[i].Count() >= 3 && words[i].Count() <= 4)
                        {
                            WordsAvailableForUse++;
                        }
                    }
                    for (int i = 0; i < words.Count(); i++)
                    {
                        for (int j = 0; j < ExludedWords.Count(); j++)
                        {
                            if (words[i].ToLower() == ExludedWords[j].ToLower())
                            {
                                WordsAvailableForUse--;
                            }
                        }
                    }
                    if (WordsAvailableForUse <= 0)
                    {
                        Yes = true;
                    }
                    ; break;
                case 2:
                    for (int i = 0; i < words.Count(); i++)
                    {
                        if (words[i].Count() >= 5 && words[i].Count() <= 6)
                        {
                            WordsAvailableForUse++;
                        }
                    }
                    for (int i = 0; i < words.Count(); i++)
                    {
                        for (int j = 0; j < ExludedWords.Count(); j++)
                        {
                            if (words[i].ToLower() == ExludedWords[j].ToLower())
                            {
                                WordsAvailableForUse--;
                            }
                        }
                    }
                    if (WordsAvailableForUse <= 0)
                    {
                        Yes = true;
                    }
                    ; break;
                case 3:
                    for (int i = 0; i < words.Count(); i++)
                    {
                        if (words[i].Count() >= 7 && words[i].Count() <= 9)
                        {
                            WordsAvailableForUse++;
                        }
                    }
                    for (int i = 0; i < words.Count(); i++)
                    {
                        for (int j = 0; j < ExludedWords.Count(); j++)
                        {
                            if (words[i].ToLower() == ExludedWords[j].ToLower())
                            {
                                WordsAvailableForUse--;
                            }
                        }
                    }
                    if (WordsAvailableForUse <= 0)
                    {
                        Yes = true;
                    }
                    ; break;
                case 4:
                    for (int i = 0; i < words.Count(); i++)
                    {
                        if (words[i].Count() >= 10 && words[i].Count() <= 12)
                        {
                            WordsAvailableForUse++;
                        }
                    }
                    for (int i = 0; i < words.Count(); i++)
                    {
                        for (int j = 0; j < ExludedWords.Count(); j++)
                        {
                            if (words[i].ToLower() == ExludedWords[j].ToLower())
                            {
                                WordsAvailableForUse--;
                            }
                        }
                    }
                    if (WordsAvailableForUse <= 0)
                    {
                        Yes = true;
                    }
                    ; break;
            }
            return Yes;
        }

        public static int MaxCharIdentifyer(string[] words)
        {
            int maxchar = words[0].Count();
            for (int i = 1; i < words.Length; i++)
            {
                if (words[i].Count() > words[i - 1].Count())
                {
                    maxchar = words[i].Count();
                }
            }
            return maxchar;
        }
        public static bool WordCounter(string[] words)
        {
            char[] trimmer = new char[] { ' ', '!', ',', '.', ';' };
            bool IsCorrectCount = false;
            int difficulty = Flow.DifficultyIdentifier();
            switch (difficulty)
            {
                case 1:
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i].Trim(trimmer).Length >= 3 && words[i].Trim(trimmer).Length <= 4)
                        {
                            IsCorrectCount = true;
                            break;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i].Trim(trimmer).Length >= 5 && words[i].Trim(trimmer).Length <= 6)
                        {
                            IsCorrectCount = true;
                            break;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i].Trim(trimmer).Length >= 7 && words[i].Trim(trimmer).Length <= 9)
                        {
                            IsCorrectCount = true;
                            break;
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i].Trim(trimmer).Length >= 10 && words[i].Trim(trimmer).Length <= 12)
                        {
                            IsCorrectCount = true;
                            break;
                        }
                    }
                    break;
            }

            return IsCorrectCount;
        }
        public static void GetSelectedType(string t)
        {
            type = t;
            SetSelectedType();
        }

        public static string SetSelectedType()
        {
            return type;
        }
        public static string OnlineSelectionStart()
        {
            random = new Random();
            string phrase = "";
            int max = 12;
            int difficulty = Flow.DifficultyIdentifier();

            switch (difficulty)
            {
                case 1: max = 15; break;
                case 2: max = 21; break;
                case 3: max = 28; break;
                case 4: max = 28; break;

            }
            if (Flow.GameTypeSetter().Equals(1))//Selection By Qoute
            {
                int randoming;

                randoming = random.Next(0, SetOnlineQoutes().Count - 1);
                var QouteList = SetOnlineQoutes()[randoming].QoutePhrase;

                while (QouteList.Equals(null) || QouteList.Equals(" ") || QouteList.Equals("") || QouteList.Length > max)
                {

                    randoming = random.Next(0, SetOnlineQoutes().Count - 1);
                    QouteList = SetOnlineQoutes()[randoming].QoutePhrase;
                }
                phrase = QouteList;
            }
            if (Flow.GameTypeSetter().Equals(2))//Selection By Sayings
            {

            }
            return phrase;

        }

        static string onlineshowphrase;
        public static void OnlineGetThisPhrase(string Words)
        {
            onlineshowphrase = Words;
            OnlineThisIsThePhrase();
        }

        public static string OnlineThisIsThePhrase()
        {
            return onlineshowphrase;
        }



        //================= Toks=============================//
        public static int SelectionStart()
        {
            random = new Random();
            MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();
            int max = 15;
            int difficulty = Flow.DifficultyIdentifier();
            int gettingID = 0;
            int ID = 0;
            string[] splitter = { " ", "/", "...", "-", ";", ":" };
            switch (difficulty)
            {
                case 1: max = 15; break;
                case 2: max = 24; break;
                case 3: max = 28; break;
                case 4: max = 28; break;

            }
            if (Flow.GameTypeSetter().Equals(1))//Selection By Qoute
            {

                var QouteList = LocalSelection.SetSelectedQASList();
                gettingID = random.Next(0, QouteList.Count + 1);

                var phrase = QouteList.Where(qoute => qoute.id.Equals(QouteList[gettingID].id)).Select(id => id.id.Equals(QouteList[gettingID].id)).FirstOrDefault();
                var numword = QouteList.Where(qoute => qoute.id.Equals(QouteList[gettingID].id)).Select(id => id.primary_text).FirstOrDefault().Split(splitter, StringSplitOptions.RemoveEmptyEntries).Length;
                ID = QouteList[gettingID].id;
                while (phrase.Equals(null) || phrase.Equals(" ") || phrase.Equals("") || numword > max || numword <= 1)
                {

                    gettingID = random.Next(0, QouteList.Count + 1);
                    phrase = QouteList.Where(qoute => qoute.id.Equals(QouteList[gettingID].id)).Select(id => id.id.Equals(QouteList[gettingID].id)).FirstOrDefault();
                    numword = QouteList.Where(qoute => qoute.id.Equals(QouteList[gettingID].id)).Select(id => id.primary_text).FirstOrDefault().Split(splitter, StringSplitOptions.RemoveEmptyEntries).Length;
                    ID = QouteList[gettingID].id;
                }

            }
            if (Flow.GameTypeSetter().Equals(2))//Selection By Sayings
            {
                var SayingList = LocalSelection.SetSelectedQASList();
                gettingID = random.Next(1, SayingList.Count + 1);
                var phrase = SayingList.Where(qoute => qoute.id.Equals(gettingID)).Select(id => id.id.Equals(gettingID)).FirstOrDefault();
                var numword = SayingList.Where(qoute => qoute.id.Equals(SayingList[gettingID].id)).Select(id => id.primary_text).FirstOrDefault().Split(splitter, StringSplitOptions.RemoveEmptyEntries).Length;
                while (phrase.Equals(null) || phrase.Equals(" ") || phrase.Equals("") || numword > max || numword <= 1)
                {

                    gettingID = random.Next(1, SayingList.Count + 1);

                    phrase = SayingList.Where(qoute => qoute.id.Equals(gettingID)).Select(id => id.id.Equals(gettingID)).FirstOrDefault();
                    numword = SayingList.Where(qoute => qoute.id.Equals(SayingList[gettingID].id)).Select(id => id.primary_text).FirstOrDefault().Split(splitter, StringSplitOptions.RemoveEmptyEntries).Length;
                }

            }
            return gettingID;

        }

        public static void PhraseSelectionInitiate()
        {
            char[] trimmer = new char[] { ' ', '!', ',', '.', '/', '?', '"', ':', ';','(',')','"' };
            string[] splitter = { " ", "/", "...", "-", ";", ":","  " };
            int selected = SelectionStart();
            random = new Random();
            MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();

            string[] disect = { " " };
            var selectedphrase = "";
            if (!Flow.GameInCategory())
            {
                var Listing = LocalSelection.SetSelectedQASList();
                phraselect = LocalSelection.SetIDs()[Flow.CurrentRound()];
                selectedphrase = Listing.Where(qoute => qoute.id.Equals(phraselect)).Select(phrase => phrase.primary_trimmed).FirstOrDefault();
                disect = selectedphrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                AllWords = disect;
            }
            else {
                var Listing =CategoryLoad.SetSelectedQASList();
                phraselect = CategoryLoad.LoadSelectedToks()[Flow.CurrentRound()];
                selectedphrase = Listing.Where(qoute => qoute.id.Equals(phraselect)).Select(phrase => phrase.primary_trimmed).FirstOrDefault();
                disect = selectedphrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                AllWords = disect;
            }

        
            /*
            bool flagcheck = true;
            int infichecker = 0;
           
                int difficutly = Flow.DifficultyIdentifier();
                switch (difficutly)
                {
                    case 1:
                        while (flagcheck)
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

                            for (int i = 0; i < disect.Length; i++)
                            {
                                if (disect[i].Trim(trimmer).Length < 5)
                                {
                                    flagcheck = false;
                                }

                            }
                            for (int idc = 0; idc < IdBank.Length; idc++)
                            {
                                if (IdBank[idc].Equals(phraselect))
                                {
                                    flagcheck = true;
                                    break;
                                }
                            }

                            if (flagcheck)
                            {

                                selected = SelectionStart();
                                //phraselect = SelectionStart();
                                phraselect = Listing[selected].id;
                                IDGetter();
                                selectedphrase = Listing.Where(qoute => qoute.id.Equals(phraselect)).Select(phrase => phrase.primary_text).FirstOrDefault();
                                disect = selectedphrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                                AllWords = disect;
                            }
                            AllWords = disect;

                        }
                        break;
                    case 2:
                        while (flagcheck)
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

                            for (int i = 0; i < disect.Length; i++)
                            {
                                if (disect[i].Trim(trimmer).Length >= 4 && disect[i].Trim(trimmer).Length <= 6)
                                {
                                    flagcheck = false;
                                }

                            }
                            for (int idc = 0; idc < IdBank.Length; idc++)
                            {
                                if (IdBank[idc].Equals(phraselect))
                                {
                                    flagcheck = true;
                                    break;
                                }
                            }
                            if (flagcheck)
                            {
                                selected = SelectionStart();
                                phraselect = Listing[selected].id;
                                IDGetter();
                                selectedphrase = Listing.Where(qoute => qoute.id.Equals(phraselect)).Select(phrase => phrase.primary_text).FirstOrDefault();
                                disect = selectedphrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                                AllWords = disect;
                            }
                            AllWords = disect;

                        }

                        break;
                    case 3:
                        while (flagcheck)
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

                            for (int i = 0; i < disect.Length; i++)
                            {
                                if (disect[i].Trim(trimmer).Length >= 6 && disect[i].Trim(trimmer).Length <= 8)
                                {
                                    flagcheck = false;
                                }

                            }
                            for (int idc = 0; idc < IdBank.Length; idc++)
                            {
                                if (IdBank[idc].Equals(phraselect))
                                {
                                    flagcheck = true;
                                    break;
                                }
                            }
                            if (flagcheck)
                            {
                                selected = SelectionStart();
                                phraselect = Listing[selected].id;
                                IDGetter();
                                selectedphrase = Listing.Where(qoute => qoute.id.Equals(phraselect)).Select(phrase => phrase.primary_text).FirstOrDefault();
                                disect = selectedphrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                                AllWords = disect;
                            }
                            AllWords = disect;

                        }

                        break;
                    case 4:
                        while (flagcheck)
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

                            for (int i = 0; i < disect.Length; i++)
                            {
                                if (disect[i].Trim(trimmer).Length >= 9 && disect[i].Trim(trimmer).Length <= 12)
                                {
                                    flagcheck = false;
                                }

                            }
                            for (int idc = 0; idc < IdBank.Length; idc++)
                            {
                                if (IdBank[idc].Equals(phraselect))
                                {
                                    flagcheck = true;
                                    break;
                                }
                            }
                            if (flagcheck)
                            {
                                selected = SelectionStart();
                                phraselect = Listing[selected].id; ;
                                IDGetter();
                                selectedphrase = Listing.Where(qoute => qoute.id.Equals(phraselect)).Select(phrase => phrase.primary_text).FirstOrDefault();
                                disect = selectedphrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                                AllWords = disect;
                            }
                            AllWords = disect;

                        }

                        break;
                }*/

            GetThisPhrase(selectedphrase);
            IdBank[Flow.CurrentRound()] = phraselect;
            SelectedPhrase();
            IDGetter();

        }

        public static bool EnteredInfiniteLoop()
        {
            return diditloop;
        }


        public static string[] SelectedPhrase()
        {
          
            return AllWords;
        }

        static string showphrase;
        public static void GetThisPhrase(string Words)
        {

            showphrase = Words;



            ThisIsThePhrase();
        }

        public static string ThisIsThePhrase()
        {
            /*   if (CrossConnectivity.Current.IsConnected) {

                       showphrase = SetOnlineQoutes().Where(id => id.QouteID.Equals(SetOnlineID())).Select(sources => sources.QoutePhrase).FirstOrDefault();

               }*/



            return showphrase;
        }
        public static void SetLocalID(int localid)
        {
            phraselect = localid;
            IDGetter();
        }
        public static int GetLocalID()
        {
            return phraselect;
        }
        public static int IDGetter()
        {
           

            return phraselect;
        }
        public static string ShowCategory()
        {
            string showcategory = "";
            if (!Flow.GameInCategory())
            {
                var Listing = LocalSelection.SetSelectedQASList();

                showcategory = Listing.Where(id => id.id.Equals(LocalSelection.SetIDs()[Flow.CurrentRound()])).Select(category => category.category).FirstOrDefault();



            }
            else
            {
                var Listing = CategoryLoad.SetSelectedQASList();

                showcategory = Listing.Where(id => id.id.Equals(CategoryLoad.LoadSelectedToks()[Flow.CurrentRound()])).Select(category => category.category).FirstOrDefault();



            }


            /*   if (!CrossConnectivity.Current.IsConnected)
               {
                   MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();
                   if (Flow.GameTypeSetter().Equals(1))//Selection By Qoute
                   {
                       showcategory = main.Qoutes.Where(id => id.id.Equals(IDGetter())).Select(category => category.Category).FirstOrDefault();
                   }
                   if (Flow.GameTypeSetter().Equals(2))//Selection By Sayings
                   {
                       showcategory = main.Sayings.Where(id => id.id.Equals(IDGetter())).Select(category => category.Category).FirstOrDefault();
                   }
               }
               else {

                   showcategory = SetOnlineQoutes().Where(id => id.QouteID.Equals(SetOnlineID())).Select(sources => sources.Category).FirstOrDefault() ;


               }
               */
            return showcategory;
        }
        public static string ShowSource()
        {
           
            string showsource = "";
            if (!Flow.GameInCategory())
            {
                var Listing = LocalSelection.SetSelectedQASList();

                showsource = Listing.Where(id => id.id.Equals(LocalSelection.SetIDs()[Flow.CurrentRound()])).Select(s => s.secondary_text).FirstOrDefault();



            }
            else
            {
                var Listing = CategoryLoad.SetSelectedQASList();

                showsource = Listing.Where(id => id.id.Equals(CategoryLoad.LoadSelectedToks()[Flow.CurrentRound()])).Select(s => s.secondary_text).FirstOrDefault();


            }

            /*   if (!CrossConnectivity.Current.IsConnected)
               {
                   MainViewModel.MainViewModel main = new MainViewModel.MainViewModel();

                   if (Flow.GameTypeSetter().Equals(1))//Selection By Qoute
                   {
                       showsource = main.Qoutes.Where(id => id.id.Equals(IDGetter())).Select(s => s.QouteSource).FirstOrDefault();
                   }
                   if (Flow.GameTypeSetter().Equals(2))//Selection By Sayings
                   {
                       showsource = main.Sayings.Where(id => id.id.Equals(IDGetter())).Select(s => s.SayingSource).FirstOrDefault();
                   }

               }
               else {
                   if (Flow.GameTypeSetter().Equals(1))//Selection By Qoute
                   {
                       showsource = SetOnlineQoutes().Where(id => id.QouteID.Equals(SetOnlineID())).Select(sources => sources.QoutesSource).FirstOrDefault();
                   }
                   else {
                       showsource = "";
                       }
               }*/
            return showsource;
        }
        public static int PhraseMaximumCharacter()
        {
            int getMaxChar = 0;
            int difficulty = Flow.DifficultyIdentifier();
            switch (difficulty)
            {
                case 1: getMaxChar = 4; break;
                case 2: getMaxChar = 6; break;
                case 3: getMaxChar = 8; break;
                case 4: getMaxChar = 12; break;
            }
            return getMaxChar;
        }

     

    }
}