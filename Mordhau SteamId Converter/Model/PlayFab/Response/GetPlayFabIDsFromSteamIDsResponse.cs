using Newtonsoft.Json;
using System.Collections.Generic;

namespace MordhauTools.Model.PlayFab.Response
{
    public class GetPlayFabIDsFromSteamIDsResponse : PlayFabResponse
    {
        [JsonProperty("data")]
        public DataWrapperObject<List<SteamPlayFabIdPair>> SteamPlayFabPairs { get; set; }
    }
}
