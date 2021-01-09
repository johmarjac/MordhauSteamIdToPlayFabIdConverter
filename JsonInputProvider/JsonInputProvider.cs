using MordhauTools.Shared.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace JsonInputProvider
{
    public class JsonInputProvider : IInputConversionProvider
    {
        public string ProviderName => "Json Input Provider";

        public string Description => "This Provider read the Input File and parses a String Array in Json Format.";

        public async Task<string[]> ImportData(string inputFile)
        {
            if (!File.Exists(inputFile))
                return null;

            return JsonConvert.DeserializeObject<string[]>(await File.ReadAllTextAsync(inputFile));
        }
    }
}
