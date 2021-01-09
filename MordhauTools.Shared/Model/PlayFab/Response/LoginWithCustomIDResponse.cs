using Newtonsoft.Json;

namespace MordhauTools.Shared.Model.PlayFab.Response
{
    public class LoginWithCustomIDResponse : PlayFabResponse
    {
        [JsonProperty("data")]
        public LoginResult Data { get; set; }
    }
}
