using MordhauTools.Shared.Model.PlayFab.Request;
using MordhauTools.Shared.Model.PlayFab.Response;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MordhauTools.Core
{
    public static class PlayFabApiHelper
    {

        public static async Task<PlayFabResponse> LoginWithCustomID(LoginWithCustomIDRequest request)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.Default, "application/json");

                var response = await client.PostAsync($"https://{request.TitleId}.playfabapi.com/Client/LoginWithCustomID", content);
                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return JsonConvert.DeserializeObject<LoginWithCustomIDResponse>(responseStr);
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    return JsonConvert.DeserializeObject<ApiErrorWrapper>(responseStr);
                else
                    throw new Exception("Unhandled StatusCode occured.");
            }
        }

        public static async Task<PlayFabResponse> GetPlayFabIDsFromSteamIDs(string sessionTicket, string titleId, GetPlayFabIDsFromSteamIDsRequest request)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Authorization", sessionTicket);

                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.Default, "application/json");

                var response = await client.PostAsync($"https://{titleId}.playfabapi.com/Client/GetPlayFabIDsFromSteamIDs", content);
                var responseStr = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return JsonConvert.DeserializeObject<GetPlayFabIDsFromSteamIDsResponse>(responseStr);
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    return JsonConvert.DeserializeObject<ApiErrorWrapper>(responseStr);
                else
                    throw new Exception("Unhandled StatusCode occured.");
            }
        }

    }
}
