namespace MordhauTools.Shared.Model.PlayFab.Request
{
    public sealed class LoginWithCustomIDRequest : PlayFabTitleRequest
    {
        public string CustomId { get; set; }

        public bool CreateAccount { get; set; }
    }
}
