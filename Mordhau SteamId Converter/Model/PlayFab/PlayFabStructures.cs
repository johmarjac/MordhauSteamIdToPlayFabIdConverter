using Newtonsoft.Json;

namespace MordhauTools.Model.PlayFab
{
    public class LoginResult
    {
        public string PlayFabId { get; set; }

        public string SessionTicket { get; set; }
    }

    public class SteamPlayFabIdPair
    {
        public string PlayFabId { get; set; }

        public string SteamStringId { get; set; }
    }

    public class DataWrapperObject<TObject>
        where TObject : new()
    {
        [JsonProperty("data")]
        public TObject Data { get; set; }
    }

}
