using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TokBlitzBeta.GamePlay
{
   public static class SignalRService
    {
        private const string baseUrl = "https://tokgamesapi.azurewebsites.net/api/v1";
        private const string codePrefix = "?code=";
        private const string apiKey = "3zAAVfyq47XbeMEvUmn855SXjTMZf/Z7nt0Cj5UrQ3w/UCCuHheGjQ==";

        static HttpClient client = new HttpClient();
        public static async Task<SignalRConnectionInfo> GetSignalRConnectionInfo()
        {
            var result = await client.PostAsync($"{baseUrl}/multiplayerconnect{codePrefix}{apiKey}", null);
            return JsonConvert.DeserializeObject<SignalRConnectionInfo>(await result.Content.ReadAsStringAsync());
        }

        public static async Task<SignalRConnectionInfo> MultiplayerSetup(GamePlayer player)
        {
            var result = await client.PostAsync($"{baseUrl}/multiplayersetup{codePrefix}{apiKey}", new StringContent(JsonConvert.SerializeObject(player)));
            return JsonConvert.DeserializeObject<SignalRConnectionInfo>(await result.Content.ReadAsStringAsync());
        }

        public static async Task<SignalRConnectionInfo> MultiplayerStopWaiting(GamePlayer player)
        {
            var result = await client.PostAsync($"{baseUrl}/multiplayerstopwaiting{codePrefix}{apiKey}", new StringContent(JsonConvert.SerializeObject(player)));
            return JsonConvert.DeserializeObject<SignalRConnectionInfo>(await result.Content.ReadAsStringAsync());
        }

        public static async Task<SignalRConnectionInfo> SendMessage(GameMessage message)
        {
            var result = await client.PostAsync($"{baseUrl}/sendgamemessage{codePrefix}{apiKey}", new StringContent(JsonConvert.SerializeObject(message)));
            return JsonConvert.DeserializeObject<SignalRConnectionInfo>(await result.Content.ReadAsStringAsync());
        }
    }

    public class SignalRConnectionInfo
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }

    //Message types:
    //p1_round, p2_round, p1_guess, p2_guess, p1_points, p2_points, time
    public class GameMessage
    {
        [JsonProperty("player")]
        public GamePlayer Player { get; set; } = new GamePlayer();

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("room_id")]
        public string RoomId { get; set; }

        [JsonProperty("content")]
        public dynamic Content { get; set; }
    }

    public class GameInfo
    {
        [JsonProperty("players")]
        public List<GamePlayer> Players { get; set; }

        [JsonProperty("room_id")]
        public string RoomId { get; set; }

        [JsonProperty("toks")]
        public List<GameTok> Toks { get; set; }

        [JsonProperty("content")]
        public dynamic Content { get; set; }
    }

    public class GamePlayer
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("user_photo")]
        public string UserPhoto { get; set; }

        [JsonProperty("player_number", NullValueHandling = NullValueHandling.Ignore)]
        public int? PlayerNumber { get; set; } = null;
    }
    
    public class GameToks
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
        //Unix time format                                                                          
        [JsonProperty(PropertyName = "_ts")]
        public long _Timestamp { get; set; }

        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; } = "tok";

        [JsonProperty(PropertyName = "activity_id")]
        public string ActivityId { get; set; } = "";

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

        [JsonProperty(PropertyName = "is_replicated")]
        public bool IsReplicated { get; set; }

        [JsonProperty(PropertyName = "is_global")]
        public bool IsGlobal { get; set; } = true;

        [JsonProperty(PropertyName = "verified")]
        public bool IsVerified { get; set; } = true;

        [JsonProperty(PropertyName = "nsfw")]
        public bool NSFW { get; set; } = false;

        #region Statistics
        //Statistics

        [JsonProperty(PropertyName = "users_reacted", NullValueHandling = NullValueHandling.Ignore)]
        public long? UsersReacted { get; set; } = null;

        [JsonProperty(PropertyName = "likes", NullValueHandling = NullValueHandling.Ignore)]
        public long? Likes { get; set; } = null;

        [JsonProperty(PropertyName = "dislikes", NullValueHandling = NullValueHandling.Ignore)]
        public long? Dislikes { get; set; } = null;

        [JsonProperty(PropertyName = "accurates", NullValueHandling = NullValueHandling.Ignore)]
        public long? Accurates { get; set; } = null;

        [JsonProperty(PropertyName = "inaccurates", NullValueHandling = NullValueHandling.Ignore)]
        public long? Inaccurates { get; set; } = null;

        [JsonProperty(PropertyName = "comments", NullValueHandling = NullValueHandling.Ignore)]
        public long? Comments { get; set; } = null;

        [JsonProperty(PropertyName = "reports", NullValueHandling = NullValueHandling.Ignore)]
        public long? Reports { get; set; } = null;

        [JsonProperty(PropertyName = "shares", NullValueHandling = NullValueHandling.Ignore)]
        public long? Shares { get; set; } = null;

        [JsonProperty(PropertyName = "views", NullValueHandling = NullValueHandling.Ignore)]
        public long? Views { get; set; } = null;

        #endregion

        [JsonProperty(PropertyName = "num_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumberId { get; set; } = null;

        [JsonProperty(PropertyName = "user_number_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? UserNumberId { get; set; } = null;

        [JsonProperty(PropertyName = "category_number_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? CategoryNumberId { get; set; } = null;

        [JsonProperty(PropertyName = "toktype_number_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ToktypeNumberId { get; set; } = null;


    }
}
