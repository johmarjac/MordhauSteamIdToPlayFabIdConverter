using Newtonsoft.Json;

namespace MordhauTools.Model.PlayFab.Response
{
    public class LoginWithCustomIDResponse : PlayFabResponse
    {
        [JsonProperty("data")]
        public LoginResult Data { get; set; }
    }
}
