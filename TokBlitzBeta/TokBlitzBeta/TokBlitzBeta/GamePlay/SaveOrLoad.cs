using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TokBlitzBeta.GamePlay;
using Xamarin.Forms;

namespace TokBlitzBeta.GamePlay
{
    public static class SaveOrLoad
    {


        static bool TriggeredLoad = false;
        static string[] Phrases;
        public static void ALoadedGame(bool loaded)
        {
            TriggeredLoad = loaded;
            IsLoadedGame();
        }
        public static bool IsLoadedGame()
        {
            return TriggeredLoad;
        }


        public static void SaveGameInSlot(string slot, string savename, string datavalues, string category = null)
        {

            if (!Flow.GameInCategory())
            {
                if (Application.Current.Properties.ContainsKey(slot.ToUpper()))
                {
                    Application.Current.Properties[slot.ToUpper()] = "Variety-" + Flow.DifficultyIdentifier() + "-" + savename + "-" + datavalues.TrimEnd('-');
                }
                else
                {
                    Application.Current.Properties.Add(slot.ToUpper(), "Variety-" + Flow.DifficultyIdentifier() + "-" + savename + "-" + datavalues.TrimEnd('-'));

                }

            }
            else
            {
                if (Application.Current.Properties.ContainsKey(slot.ToUpper()))
                {
                    Application.Current.Properties[slot.ToUpper()] = "Category-" + category + "-" + savename + "-" + datavalues.TrimEnd('-');
                }
                else
                {
                    Application.Current.Properties.Add(slot.ToUpper(), "Category-" + category + "-" + savename + "-" + datavalues.TrimEnd('-'));

                }
            }

            Application.Current.SavePropertiesAsync();


        }
        static string Type, Settings, DataValues, SaveName;
        public static void LoadGameInSelectedSlot(string slot)
        {
            if (Application.Current.Properties.ContainsKey(slot.ToUpper()))
            {
                string LoadSlot = Application.Current.Properties[slot.ToUpper()].ToString();
                string[] SplittedDatas = { };

                if (!Flow.GameInCategory())
                {
                    SplittedDatas = LoadSlot.Split('-');
                    Type = SplittedDatas[0];
                    Settings = SplittedDatas[1];
                    SaveName = SplittedDatas[2];
                    DataValues = SplittedDatas[3] + "-" + SplittedDatas[4] + "-" + SplittedDatas[5] + "-" + SplittedDatas[6] + "-" + SplittedDatas[7];
                }
                else
                {
                    SplittedDatas = LoadSlot.Split('-');
                    Type = SplittedDatas[0];
                    Settings = SplittedDatas[1];
                    SaveName = SplittedDatas[2];
                    DataValues = SplittedDatas[3] + "-" + SplittedDatas[4] + "-" + SplittedDatas[5] + "-" + SplittedDatas[6] + "-" + SplittedDatas[7];
                }

            }
            else
            {
                Type = null;
                Settings = null;
                DataValues = null;
                SaveName = null;
            }
            SetLoadedGameType();
            SetLoadedSettings();
            SetLoadedDataValues();
            SetSaveName();
        }

        public static string SetLoadedGameType()
        {
            return Type;
        }
        public static string SetLoadedSettings()
        {
            return Settings;
        }
        public static string SetLoadedDataValues()
        {
            return DataValues;
        }

        public static string SetSaveName()
        {
            return SaveName;
        }

        public static void GetPhrases(string[] phrase)
        {
            Phrases = phrase;
            SetPhrases();
        }

        public static string[] SetPhrases()
        {
            return Phrases;
        }


        /// <summary>
        /// A class used for both category progress and slot saves.
        /// </summary>


        /// <summary>
        /// Returns the input string in lowercase alphanumeric format. All spaces are replaced with underscores.
        /// </summary>
        public static string ToIdFormat(this string item)
        {
            item = item?.Trim().ToLower().Replace("/", "").Replace(" ", "_").Replace("&", "and").Replace("é", "e");
            item = Regex.Replace(item, "[^0-9A-Za-z]", "");
            return item;
        }


    }

    public class SaveFunctions
    {
        //===========GameTok Codes=======================///
        private const string baseUrl = "https://tokgamesapi.azurewebsites.net/api/v1";
        private const string codePrefix = "?code=";
        private const string apiKey = "3zAAVfyq47XbeMEvUmn855SXjTMZf/Z7nt0Cj5UrQ3w/UCCuHheGjQ==";
        //==================================================//
        public HttpClient client = new HttpClient();
        #region GameSave
        public async Task<bool> CreateGameSaveAsync(GameSave item)
        {
            client.BaseAddress = new Uri($"{baseUrl}/gamesave{codePrefix}{apiKey}");
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, item);
            client = new HttpClient();
            return response.IsSuccessStatusCode;
        }

        public async Task<GameSave> GetGameSaveAsync(string id)
        {

            client.BaseAddress = new Uri($"{baseUrl}/gamesave/{id}{codePrefix}{apiKey}");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            client = new HttpClient();
            GameSave getter = new GameSave();

            return await response.Content.ReadAsAsync<GameSave>();
        }

        public async Task<bool> UpdateGameSaveAsync(GameSave item)
        {
            client.BaseAddress = new Uri($"{baseUrl}/gamesave/{item.id}{codePrefix}{apiKey}");
            HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress, item);
            client = new HttpClient();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGameSaveAsync(string id)
        {
            client.BaseAddress = new Uri($"{baseUrl}/gamesave/{id}{codePrefix}{apiKey}");
            HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress);
            client = new HttpClient();
            return response.IsSuccessStatusCode;
        }
        #endregion


    }

    public class GameSave
    {
        public GameSave(string gameName = "Tok Blitz")
        {
            game_name = gameName;
            label = $"gamesave-{game_name.ToIdFormat()}";
        }

        /// <summary>
        /// Category progress: $"{user_id}-{gameName}-{category}"
        /// Slot save: $"{user_id}-{gameName}-slot1", $"{user_id}-{gameName}-slot2", $"{user_id}-{gameName}-slot3"
        /// </summary>
        public string id;

        /// <summary>
        /// This partition key (pk) field is used in the database to group data into partition
        /// Category progress: $"{user_id}-{gameName}"
        /// Slot save: $"{user_id}-{gameName}"
        /// </summary>
        public string pk;

        public string game_name;

        public string label;

        /// <summary>
        /// Stored all tok ids here in this string array like this: toks.Add("4")
        /// </summary>
        public List<string> toks;

        public bool is_category;
        public bool is_saved;
    }
}
