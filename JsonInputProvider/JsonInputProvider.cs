using MordhauTools.Shared.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace JsonInputProvider
{
    public class JsonInputProvider : IInputConversionProvider
    {
        public string ProviderName => "Json Input Provider";

        public async Task<string[]> ImportData(string inputFile)
        {
            return JsonConvert.DeserializeObject<string[]>(await File.ReadAllTextAsync(inputFile));
        }
    }
}
