using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Firebase.Auth;
using System.Net.Http;
using TokBlitzBeta.Model;
using Xamarin.Forms;
using System.IO;

namespace TokBlitzBeta.GamePlay
{
    public class TokGamesApiClient
    {
        private bool FeaturedToksEnabled = true;
        //===========GameTok Codes=======================///
        private const string baseUrl = "https://tokgamesapi.azurewebsites.net/api/v1";
        private const string codePrefix = "?code=";
        private const string apiKey = "3zAAVfyq47XbeMEvUmn855SXjTMZf/Z7nt0Cj5UrQ3w/UCCuHheGjQ==";
        //==================================================//
        //================User Acount==============//
        private const string userbaseUrl = "https://tokkepediaapidev.azurewebsites.net/api/v1";
        private const string usercodePrefix = "?code=";
        private const string userapiKey = "PXQY2m1jxGcSfXvP1RUbapHDG4VEayAvFSRKaA12dZ2v1rMJwutcPA==";
        //===================================================//
        public HttpClient client = new HttpClient();

        public FirebaseIdentityUser User { get; set; }
        void InitializeApiClientUser(bool userRequired = false)
        {
            if (userRequired)
            {
                if (User == null)
                    throw new UnauthorizedAccessException();
            }

            string userid = User?.Id, token = User?.IdToken;
            if (userid == null) userid = "";
            if (token == null) token = "";

            client.DefaultRequestHeaders.Add("userid", userid);
            client.DefaultRequestHeaders.Add("token", token);
        }

        //region User
        public async Task<FirebaseIdentityUser> GetUserAsync(string id)
        {
            client.BaseAddress = new Uri($"{userbaseUrl}/user/{id}{usercodePrefix}{userapiKey}");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            client = new HttpClient();

            var item = await response.Content.ReadAsAsync<FirebaseIdentityUser>();
            return JsonConvert.DeserializeObject<FirebaseIdentityUser>(JsonConvert.SerializeObject(item));
        }

        public async Task<FirebaseAuthLink> SignUpAsync(string email, string password, string displayName, string country, DateTime date, string userPhoto)
        {
            TokketUser user = new TokketUser() { Email = email, PasswordHash = password, DisplayName = displayName, Country = country, Birthday = date, UserPhoto = userPhoto };
            user.BirthDate = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month)} {date.Day}";
            user.BirthYear = date.Year;
            user.BirthMonth = date.Month;
            user.BirthDay = date.Day;

            client.BaseAddress = new Uri($"{userbaseUrl}/signup{usercodePrefix}{userapiKey}");
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, user);
            client = new HttpClient();
            var res = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FirebaseAuthLink>(res);

        }

        public async Task<FirebaseAuthLink> LoginEmailPasswordAsync(string email, string password)
        {
            TokketUser user = new TokketUser() { Email = email, PasswordHash = password };
            client.BaseAddress = new Uri($"{userbaseUrl}/login{usercodePrefix}{userapiKey}");
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, user);
            client = new HttpClient();

            return JsonConvert.DeserializeObject<FirebaseAuthLink>(await response.Content.ReadAsStringAsync());
        }
        //endregion
        public async Task<List<GameTok>> GetGameToksAsync(GameToksQuery values = null)
        {
            if (values == null)
                values = new GameToksQuery() { count = -1 };

            client.BaseAddress = new Uri($"{baseUrl}/gametoks{codePrefix}{apiKey}");
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, values);
            client = new HttpClient();
            try
            {
                var data = await response.Content.ReadAsAsync<List<GameTok>>();

                GetGameToks(data);

                return data;
            }
            catch
            {
                return null;
            }
        }
        static List<GameTok> toks = new List<GameTok>();


        public static void GetGameToks(List<GameTok> gameToks) {
            toks = gameToks;
            SetGameToks();
        }
        public static List<GameTok> SetGameToks() {
            return toks;
        }
        public static string SetProfileURL(string id) {
            return $"{userbaseUrl}/user/{id}{usercodePrefix}{userapiKey}";
        }

        #region Set
        public async Task<Set> GetSetAsync(string id)
        {
            client.BaseAddress = new Uri($"{userbaseUrl}/set/{id}{usercodePrefix}{userapiKey}");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            client = new HttpClient();
            return await response.Content.ReadAsAsync<Set>();
        }
        public async Task<ResultData<Set>> GetSetsAsync(SetQueryValues values = null)
        {
            if (values == null)
                values = new SetQueryValues();

            client.DefaultRequestHeaders.Add("order", values?.order);
            client.DefaultRequestHeaders.Add("userid", values?.userid);
            client.DefaultRequestHeaders.Add("text", values?.text);
            client.DefaultRequestHeaders.Add("loadmore", values?.loadmore);
            client.DefaultRequestHeaders.Add("token", values?.token);
            client.BaseAddress = new Uri($"{userbaseUrl}/sets{usercodePrefix}{userapiKey}");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            client = new HttpClient();
            return await response.Content.ReadAsAsync<ResultData<Set>>();
        }
        public async Task<ResultData<GameTok>> GetToksByIdsAsync(string[] ids)
        {
            if (ids == null)
                return null;

            if (ids.Length > 100)
                return null;

            client.BaseAddress = new Uri($"{userbaseUrl}/toks{usercodePrefix}{userapiKey}");
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, ids);
            client = new HttpClient();

            try
            {
                var data = await response.Content.ReadAsAsync<ResultData<GameTok>>();

                for (int i = 0; i < data.Results.Count; ++i)
                {
                    if (data.Results[i].UserId == "tokket")
                    {
                        data.Results[i].UserPhoto = "/images/tokket.png";
                    }
                }

                return data;
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
    public class GameToksQuery
    {
        public string tok_group;
        public string tok_type;
        public string category;
        public int count = 10;
        public int group_number;
    }
    public class GameTok
    {
        [JsonProperty(PropertyName = "certified")]
        public bool IsCertified { get; set; } = true;

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; } = "";

        [JsonProperty(PropertyName = "group_number")]
        public int GroupNumber { get; set; } = 0;

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = "";

        [JsonProperty(PropertyName = "created_time")]
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        [JsonIgnore]
        private DateTime timestamp = DateTime.Now;

        //DataTime format
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp
        {
            get { return timestamp; }
            set
            {
                timestamp = DateTime.Now;
                //_Timestamp = DateTime.Now;
            }
        }

        //Unix time format
        [JsonProperty(PropertyName = "_ts")]
        public long _Timestamp { get; set; }

        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; } = "tok";

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; } = "user";

        //Retrieve separately in the client in case changes are made
        [JsonProperty(PropertyName = "user_display_name")]
        public string UserDisplayName { get; set; } = "User Name";

        [JsonProperty(PropertyName = "user_photo", NullValueHandling = NullValueHandling.Ignore)]
        public string UserPhoto { get; set; }

        [JsonProperty(PropertyName = "user_country", NullValueHandling = NullValueHandling.Ignore)]
        public string UserCountry { get; set; } = null;

        [JsonProperty(PropertyName = "tok_group")]
        public string TokGroup { get; set; }

        [JsonProperty(PropertyName = "tok_type")]
        public string TokType { get; set; }

        [JsonProperty(PropertyName = "tok_type_id")]
        public string TokTypeId { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "category_id")]
        public string CategoryId { get; set; }

        [JsonProperty(PropertyName = "primary_name")]
        public string PrimaryFieldName { get; set; }

        [JsonProperty(PropertyName = "primary_text")]
        public string PrimaryFieldText { get; set; }

        [JsonProperty(PropertyName = "secondary_name")]
        public string SecondaryFieldName { get; set; }

        [JsonProperty(PropertyName = "secondary_text", NullValueHandling = NullValueHandling.Ignore)]
        public string SecondaryFieldText { get; set; }

        [JsonProperty(PropertyName = "details", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Details { get; set; } = null;

        [JsonProperty("language")]
        public string Language { get; set; } = "english";

        [JsonProperty(PropertyName = "english_primary_text", NullValueHandling = NullValueHandling.Ignore)]
        public string EnglishPrimaryFieldText { get; set; }

        [JsonProperty(PropertyName = "english_secondary_text", NullValueHandling = NullValueHandling.Ignore)]
        public string EnglishSecondaryFieldText { get; set; }

        [JsonProperty(PropertyName = "english_details", NullValueHandling = NullValueHandling.Ignore)]
        public string[] EnglishDetails { get; set; } = null;

        [JsonProperty(PropertyName = "required_field_values", NullValueHandling = NullValueHandling.Ignore)]
        public string[] RequiredFieldValues { get; set; } = null;

        [JsonProperty(PropertyName = "optional_field_values", NullValueHandling = NullValueHandling.Ignore)]
        public string[] OptionalFieldValues { get; set; } = null;

        [JsonProperty(PropertyName = "notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; } = null;

        [JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; } = null;

        [JsonProperty(PropertyName = "is_detail_based")]
        public bool IsDetailBased { get; set; }

        [JsonProperty(PropertyName = "is_english")]
        public bool IsEnglish { get; set; } = true;
    }


    public class FirebaseIdentityUser
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonIgnore]
        public string IdToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        //Type of item, in this class it's a user
        [JsonRequired]
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; } = "user";

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "user_photo")]
        public string UserPhoto { get; set; }

        

        [JsonProperty(PropertyName = "birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty(PropertyName = "birthdate")]
        public string BirthDate { get; set; }

        [JsonProperty(PropertyName = "birth_year")]
        public long BirthYear { get; set; }

        [JsonProperty(PropertyName = "birth_month")]
        public long BirthMonth { get; set; }

        [JsonProperty(PropertyName = "birth_day")]
        public long BirthDay { get; set; }

        [JsonProperty(PropertyName = "gold_coins")]
        public long GoldCoins { get; set; } = 0;

        [JsonProperty(PropertyName = "platinum_coins")]
        public long PlatinumCoins { get; set; } = 0;
    }
    public class TokketUser
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonRequired]
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; } = "user";

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

     //   [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

   //     [JsonProperty(PropertyName = "password_hash")]
        public string PasswordHash { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "user_photo")]
        public string UserPhoto { get; set; }

        [JsonProperty(PropertyName = "birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty(PropertyName = "birthdate")]
        public string BirthDate { get; set; }

        [JsonProperty(PropertyName = "birth_year")]
        public int BirthYear { get; set; }

        [JsonProperty(PropertyName = "birth_month")]
        public int BirthMonth { get; set; }

        [JsonProperty(PropertyName = "birth_day")]
        public int BirthDay { get; set; }

        [JsonProperty(PropertyName = "joined")]
        public DateTime Joined { get; set; } = DateTime.Now;

        [JsonProperty("points")]
        public long Points { get; set; }

        [JsonProperty("coins")]
        public long Coins { get; set; }

        [JsonProperty(PropertyName = "created_time")]
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        //DataTime format
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        //Unix time format
        [JsonProperty(PropertyName = "_ts")]
        public int _Timestamp { get; set; }
    }
    public class SetQueryValues
    {
        public string order { get; set; }
        public string text { get; set; }
        public string userid { get; set; }
        public string loadmore { get; set; }
        public string token { get; set; }
        public string offset { get; set; } = "25"; //MaxItemCount, default is 25

        //public string category { get; set; }
        //public string tokgroup { get; set; }
        //public string toktype { get; set; }
    }

    public class Set
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString("n");

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; } = "set";

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; } = "user";

        //Retrieve separately in the client in case changes are made
        [JsonProperty(PropertyName = "user_display_name")]
        public string UserDisplayName { get; set; } = "User Name";

        [JsonProperty(PropertyName = "user_photo")]
        public string UserPhoto { get; set; }

        [JsonProperty(PropertyName = "user_country")]
        public string UserCountry { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "tok_group")]
        public string TokGroup { get; set; }

        [JsonProperty(PropertyName = "tok_type")]
        public string TokType { get; set; }

        [JsonProperty(PropertyName = "tok_type_id")]
        public string TokTypeId { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "ids")]
        public List<string> TokIds { get; set; }

        [JsonProperty(PropertyName = "toks")]
        public int Toks { get; set; } = 0;

        [JsonProperty(PropertyName = "views")]
        public int Views { get; set; } = 1;

        [JsonProperty(PropertyName = "likes")]
        public int Likes { get; set; }

        [JsonProperty(PropertyName = "shares")]
        public int Shares { get; set; }

        [JsonProperty(PropertyName = "created_time")]
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        //DataTime format
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        //Unix time format
        [JsonProperty(PropertyName = "_ts")]
        public int _Timestamp { get; set; }
    }

    public class ResultData<T>
    {
        public string Token { get; set; }
        public List<T> Results { get; set; }
    }
}
