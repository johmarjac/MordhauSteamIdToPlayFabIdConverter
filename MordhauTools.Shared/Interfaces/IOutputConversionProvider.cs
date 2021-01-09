using MordhauTools.Shared.Model.PlayFab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MordhauTools.Shared.Interfaces
{
    public interface IOutputConversionProvider
    {
        /// <summary>
        /// The Display Name of this <see cref="IOutputConversionProvider"/>.
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Describes the functionality of this <see cref="IOutputConversionProvider"/>.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The core function to export the Conversion.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="conversionResult"></param>
        /// <returns></returns>
        Task ExportData(string filename, IEnumerable<SteamPlayFabIdPair> conversionResult);
    }
}
