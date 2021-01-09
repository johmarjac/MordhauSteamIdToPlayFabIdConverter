using Newtonsoft.Json;

namespace MordhauTools.Shared.Model.PlayFab.Response
{
    public abstract class PlayFabResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
