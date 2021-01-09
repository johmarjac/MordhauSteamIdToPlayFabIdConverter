using MordhauTools.Shared.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace MordhauTools.Core.Providers
{
    public class LineInputProvider : IInputConversionProvider
    {
        public string ProviderName => "Line Input Provider";

        public string Description => "This Provider reads the Input File line by line, each containing a SteamId.";

        public Task<string[]> ImportData(string inputFile)
        {
            return File.ReadAllLinesAsync(inputFile);
        }
    }
}
