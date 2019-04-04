using System;
using System.Collections.Generic;
using System.Text;
using TokBlitzBeta.GamePlay;
using Xamarin.Forms;
using TokBlitzBeta.Model;
using Newtonsoft.Json;

namespace TokBlitzBeta.GamePlay
{
    public class CategoryLoad
    {
        static int TotalToks;
        static int ToksLeft;
        static int[] SelectedIDs;
        static int Answered;
        static string OldSaves;
        static string NewSaves;
        static List<QoutesAndSayings> SelectedQAS = new List<QoutesAndSayings>();
        public static void LoadCategoryBy(string category)
        { 
            SelectedIDs = new int[5];
            Random rand = new Random();
            string group = category;
            string[] splitter = { " ", "/", "...", "-", ";", ":" };
            int[] SelectedToksByID = new int[5];
            var result = JsonConvert.DeserializeObject<List<QoutesAndSayings>>(LocalSelection.GetQoutesAndSayings());

            List<QoutesAndSayings> list = new List<QoutesAndSayings>();


            for (int i = 0; i < result.Count; i++)
            {

                if (result[i].category.ToLower() == group.ToLower() && list.Count<120)
                { 
                    list.Add(new QoutesAndSayings
                    {
                        id = result[i].id,
                        tok_group = result[i].tok_group,
                        category = result[i].category,
                        primary_text = result[i].primary_text,
                        secondary_text = result[i].secondary_text,

                    });
                  }
            }
            TotalToks = list.Count;
            int randomed =0;
            string[] AnsweredToks = { };
            string DataValue = string.Empty;
            string Datas = string.Empty;
            if (Application.Current.Properties.ContainsKey(category.ToUpper()))
            {
                Datas = Application.Current.Properties[category.ToUpper()].ToString();
                AnsweredToks = Application.Current.Properties[category.ToUpper()].ToString().TrimEnd('-').Split('-');
                Answered = AnsweredToks.Length;
            }
            else {
                Answered = 0;
            }

            ToksLeft = TotalToks - AnsweredToks.Length;

           
            for (int i = 0; i < 5; i++)
            {
                bool clearedSaved = true;
                bool clearedSelect = true;
                randomed = rand.Next(0, TotalToks);
                if (Answered == 0)
                {
                    clearedSaved = false;
                }
                while (clearedSaved || clearedSelect)
                {
                    for (int j = 0; j < Answered; j++)
                    {
                        if (AnsweredToks[j] != list[randomed].id.ToString())
                        {


                            clearedSaved = false;
                        }
                        else
                        {
                            randomed = rand.Next(0, TotalToks);
                            clearedSaved = true;
                            break;
                        }

                    }

                    for (int j = 0; j < 5; j++)
                    {
                        if (SelectedToksByID[j] != list[randomed].id)
                        {

                            clearedSelect = false;
                        }
                        else
                        {
                            randomed = rand.Next(0, TotalToks);
                            clearedSelect = true;
                            break;
                        }

                    }
                }


                SelectedToksByID[i] = list[randomed].id;
                DataValue += list[randomed].id +"-";
            }


            GetSaves(Datas,DataValue);

            SelectedIDs = SelectedToksByID;
            SetTotalToks();
            SetToksLeft();
            SetWhatPart();
            GetAllQASList(list);
            LoadSelectedToks();
         
        }
        public static void GetSaves(string old, string New) {
            OldSaves = old;
            NewSaves = New;
            OldSaveGame();
        }

        public static string OldSaveGame() {
            return OldSaves;
        }

        public static string NewSaveGame()
        {
            return NewSaves;
        }

        public static void SaveThisNow(string category)
        {
            if (Application.Current.Properties.ContainsKey(category.ToUpper()))
            {
                Application.Current.Properties[category.ToUpper()] = OldSaveGame() + NewSaveGame();
            }
            else
            {
                Application.Current.Properties.Add(category.ToUpper(), NewSaveGame());
            }
            Application.Current.SavePropertiesAsync();
        }



        public static void LoadTheCategory(string category) {
            LocalSelection.ConvertToList();
            
            int NumberOfItems = 0;
            Random rand = new Random();
            string[] splitter = { " ", "/", "...", "-", ";", ":" };
            var list = LocalSelection.SetAllSelectedQuoteAndSayingsList();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].category.ToLower() == category.ToLower()) {
                    NumberOfItems++;
                }
            }
            TotalToks = NumberOfItems;
            int[] IDs = new int[NumberOfItems];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].category.ToLower() == category.ToLower() && IDs.Length<NumberOfItems)
                {
                    IDs[i] = list[i].id;
                }
            }
            int[] SelectedToksByID = new int[5];
            string[] AnsweredToks = { };
            string DataValue = string.Empty;
          
            if (Application.Current.Properties.ContainsKey(category))
            {
                DataValue += Application.Current.Properties[category].ToString();
                AnsweredToks = Application.Current.Properties[category].ToString().Split('-');
            }
            ToksLeft = TotalToks - AnsweredToks.Length;
            Answered = AnsweredToks.Length;
            int randomed = rand.Next(0, IDs.Length);
          
              for (int i = 0; i < 5; i++)
              {
                for (int j=0;j<AnsweredToks.Length;j++) {
                    if (AnsweredToks[j] == IDs[randomed].ToString()) {

                        randomed = rand.Next(0, IDs.Length);
                        j = 0;
                    }

                }
                 
                SelectedToksByID[i] = IDs[randomed];
                if (i <= 3) { DataValue += IDs[randomed] + "-"; } else { DataValue += IDs[randomed].ToString(); }
             
                randomed = rand.Next(0, IDs.Length);

            }

            if (Application.Current.Properties.ContainsKey(category))
            {
                Application.Current.Properties[category] = DataValue;
            }
            else
            {
                Application.Current.Properties.Add(category, DataValue);
            }
            Application.Current.SavePropertiesAsync();
            SelectedIDs = SelectedToksByID;
            SetTotalToks();
            SetToksLeft();
            SetWhatPart();
            LoadSelectedToks();
        }

        public static int SetTotalToks() {
            return TotalToks;
        }
        public static int SetToksLeft() {
            return ToksLeft;
        }
        public static string SetWhatPart() {
            string part = string.Empty;
            if (Answered >= 81 && Answered <= 120)
            {

                part = "PART 3";
            }
            else if (Answered >= 41 && Answered <= 80)
            {
                part = "PART 2";

            }
            else {
                part = "PART 1";
            }

            return part;
        }

        public static int[] LoadSelectedToks() {
            return SelectedIDs;
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
