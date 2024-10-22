﻿using System.Collections.Generic;

namespace MordhauTools.Shared.Model.PlayFab.Request
{
    public class GetPlayFabIDsFromSteamIDsRequest : PlayFabRequest
    {
        public List<string> SteamStringIDs { get; set; }

        public GetPlayFabIDsFromSteamIDsRequest()
        {
            SteamStringIDs = new List<string>();
        }
    }
}
