using Newtonsoft.Json;
using System.Collections.Generic;

namespace MordhauTools.Shared.Model.PlayFab.Response
{
    public class GetPlayFabIDsFromSteamIDsResponse : PlayFabResponse
    {
        [JsonProperty("data")]
        public DataWrapperObject<List<SteamPlayFabIdPair>> SteamPlayFabPairs { get; set; }
    }
}
