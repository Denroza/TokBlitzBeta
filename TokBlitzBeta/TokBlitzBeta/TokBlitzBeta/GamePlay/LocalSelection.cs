using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TokBlitzBeta.Model;
namespace TokBlitzBeta.GamePlay
{
    public class LocalSelection
    {

        static List<ConvertedQAS> UsedList = new List<ConvertedQAS>();
        static List<QoutesAndSayings> AllListed = new List<QoutesAndSayings>();
        static List<QoutesAndSayings> SelectedQAS = new List<QoutesAndSayings>();
        static string[] Phrases = new string[5];
        static int[] IDs = new int[5];
        static string[] Categories = new string[5];
        static string[] Sources = new string[5];
        public static string GetQoutesAndSayings() {

            var result = string.Empty;
            string fileName = "TokBlitzBeta.Data.quotesandsayings.json";
            result = GetResourceStringFromFile(fileName);
            return result;

        }
        private static string GetResourceStringFromFile(string fileName)
        {
            var result = string.Empty;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
       
            public static List<ConvertedQAS> ConvertToList() {
            var result = JsonConvert.DeserializeObject<List<QoutesAndSayings>>(GetQoutesAndSayings());
            List<ConvertedQAS> list = new List<ConvertedQAS>();
            string[] splitter = { " ", "/", "...", "-", ";", ":" };
            char[] trimmer = new char[] { ' ', '!', ',', '.', '/', '?', '"', ':', ';' };

            for (int i = 0; i<result.Count;i++) {
              
                    list.Add(new ConvertedQAS {
                        id = result[i].id,
                      
                        tok_group = result[i].tok_group,
                        
                        primary_text = result[i].primary_text,
                        secondary_text = result[i].secondary_text,
                       
                        wordcount = result[i].number_of_words,
                        maxchar = result[i].longest_word_length

                    });
               
            }
            GetAllQuoteAndSayingsList(result);
            return list;
        }

        public static void QuotesAndSayingsSelected() {
            int difficulty = Flow.DifficultyIdentifier();
            int max = 0;
            char[] trimmer = new char[] { ' ', '!', ',', '.', '/', '?', '"', ':', ';' };
            int lenMin = 0;
            int lenMax = 0;
            Random random = new Random();
            int[] ID = new int[20];
            
            var converted = ConvertToList();
            string[] splitter = { " ", "/", "...", "-", ";", ":" };
            List<ConvertedQAS> list = new List<ConvertedQAS>();
            List<ConvertedQAS> selected = new List<ConvertedQAS>();
            List<ConvertedQAS> Final = new List<ConvertedQAS>();
            switch (difficulty)
            {
                case 1: max = 15; break;
                case 2: max = 24; break;
                case 3: max = 28; break;
                case 4: max = 28; break;

            }
            switch (difficulty)
            {
                case 1: lenMin = 2; lenMax = 5; break;
                case 2: lenMin = 4; lenMax = 7; break;
                case 3: lenMin = 6; lenMax = 10; break;
                case 4: lenMin = 9; lenMax = 13; break;

            }



            for (int i = 0; i <converted.Count; i++)
            {
                if (converted[i].primary_text.Split(splitter, StringSplitOptions.RemoveEmptyEntries).Length <= max)
                {
                    list.Add(converted[i]);
                }
            }
            #region
            int randomizer = random.Next(0,list.Count);
            int thisID = list.Where(item => item.id.Equals(list[randomizer].id)).Select(item => item.id).FirstOrDefault();
            string[] word = list.Where(item => item.id.Equals(list[randomizer].id)).Select(item=>item.primary_text).FirstOrDefault().Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            int MaxCount = list.Where(item => item.id.Equals(list[randomizer].id)).Select(item => item.wordcount).FirstOrDefault();
            int tracker=0;
            while (selected.Count < 20) { 
          /*  for (int checker =0; checker<20;checker++) {
                    bool repeat = false;
                    

                    if (thisID == ID[checker] || word.Length<2 || MaxCount>max )
                    {
                        randomizer = random.Next(0, list.Count);
                        thisID = list.Where(item => item.id.Equals(list[randomizer].id)).Select(item => item.id).FirstOrDefault();
                        word = list.Where(item => item.id.Equals(list[randomizer].id)).Select(item => item.primary_text).FirstOrDefault().Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                        MaxCount = list.Where(item => item.id.Equals(list[randomizer].id)).Select(item => item.wordcount).FirstOrDefault();
                        repeat = true;

                    }
                    if (repeat) {
                        checker = 0;
                    }

                   
                }
                */
                ID[tracker] = thisID;
                selected.Add(new ConvertedQAS {
                    id = list[randomizer].id,
                    primary_text = list[randomizer].primary_text,
                    secondary_text = list[randomizer].secondary_text,
                    tok_group = list[randomizer].tok_group,
                
                    Category = list[randomizer].Category,
                  
                    wordcount = list[randomizer].wordcount
                });
                randomizer = random.Next(0, list.Count);
                tracker++;
            }

          
            #endregion
            GetUsedQuoteAndSayingsList(selected);
          

        }

        public static void GetUsedQuoteAndSayingsList(List<ConvertedQAS> list) {

            UsedList = list;
            SetSelectedQuoteAndSayingsList();
        }
        public static void GetAllQuoteAndSayingsList(List<QoutesAndSayings> list)
        {

            AllListed = list;
            SetAllSelectedQuoteAndSayingsList();
        }
        public static List<ConvertedQAS> SetSelectedQuoteAndSayingsList() {
            return UsedList;
        }
        public static List<QoutesAndSayings> SetAllSelectedQuoteAndSayingsList()
        {
            return AllListed;
        }
      static  string dataValues;
        public static List<QoutesAndSayings> ConvertQuotesAndSayings()
        {
            dataValues = string.Empty;
            Random rand = new Random();
            string group = "";
            string[] splitter = { " ", "/", "...", "-", ";", ":" };
            var result = JsonConvert.DeserializeObject<List<QoutesAndSayings>>(GetQoutesAndSayings());
           
            List<QoutesAndSayings> list = new List<QoutesAndSayings>();
                

            for (int i = 0; i < result.Count; i++)
            {

                if (result[i].is_mobile) { 
                list.Add(new QoutesAndSayings
                {
                    id = result[i].id,
                    primary_trimmed = result[i].primary_trimmed,
                        tok_group = result[i].tok_group,
                     category = result[i].category,
                        primary_text = result[i].primary_text,
                        secondary_text = result[i].secondary_text,
                     
                    });
                }

            }
            if (!SaveOrLoad.IsLoadedGame()) {
            int randomed = rand.Next(0,list.Count);
           string ChosenPhrase = list[randomed].primary_trimmed;
            for (int i=0; i<5;i++) { 
            while (!Selection.PhraseCountChecker(ChosenPhrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries).Count()) || !Selection.WordCounter(ChosenPhrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries)) ||
                Selection.DuplicatePhrase(list[randomed].id.ToString()) || Selection.HasEnoughWordsToBeAnswered(ChosenPhrase.Split(splitter, StringSplitOptions.RemoveEmptyEntries)))
            {
                randomed = rand.Next(0, list.Count);
              ChosenPhrase = list[randomed].primary_trimmed;
            }
                Phrases[i] = list[randomed].primary_text;
                IDs[i]= list[randomed].id;
                Categories[i] = list[randomed].category;
                Sources[i] = list[randomed].secondary_text;
                dataValues += list[randomed].id + "-";
            }
            SetPhrases();
            SetCategories();
            SetSources();
            DataValues();
            SetIDs();
            }
            GetAllQASList(list);
          
            return result;
        }

        public static string DataValues() {
            return dataValues;
        }
        public static string[] SetPhrases()
        {
            return Phrases;
        }
        public static string[] SetCategories()
        {
            return Categories;
        }
        public static string[] SetSources()
        {
            return Sources;
        }
        public static int[] SetIDs()
        {
            return IDs;
        }

        public static void GetAllQASList(List<QoutesAndSayings> list)
        {

            SelectedQAS = list;
            SetSelectedQASList();
        }
        public static List<QoutesAndSayings> SetSelectedQASList()
        {
            return SelectedQAS;
        }
    }
}
