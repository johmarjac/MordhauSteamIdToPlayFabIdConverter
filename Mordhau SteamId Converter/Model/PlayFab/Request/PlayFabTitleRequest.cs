﻿namespace MordhauTools.Model.PlayFab.Request
{
    public abstract class PlayFabTitleRequest : PlayFabRequest
    {
        public string TitleId { get; set; }
    }
}