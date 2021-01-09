using System.Threading.Tasks;

namespace MordhauTools.Shared.Interfaces
{
    public interface IInputConversionProvider
    {
        /// <summary>
        /// The Display Name of this <see cref="IInputConversionProvider"/>.
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Describes the functionality of this <see cref="IInputConversionProvider"/>.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The core function to import the SteamIds.
        /// </summary>
        /// <param name="inputFile">Optional: Input file parameter.</param>
        /// <returns>An array of SteamIds.</returns>
        Task<string[]> ImportData(string inputFile);
    }
}
