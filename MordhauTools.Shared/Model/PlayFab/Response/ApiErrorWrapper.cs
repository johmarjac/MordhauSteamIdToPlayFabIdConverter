using Newtonsoft.Json;

namespace MordhauTools.Shared.Model.PlayFab.Response
{
    public class ApiErrorWrapper : PlayFabResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
