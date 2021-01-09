using MordhauTools.Shared.Model.PlayFab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MordhauTools.Shared.Interfaces
{
    public interface IOutputConversionProvider
    {
        string ProviderName { get; }

        Task ExportData(string filename, IEnumerable<SteamPlayFabIdPair> conversionResult);
    }
}
