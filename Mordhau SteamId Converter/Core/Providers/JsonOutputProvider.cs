﻿using MordhauTools.Shared.Interfaces;
using MordhauTools.Shared.Model.PlayFab;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MordhauTools.Core.Providers
{
    public class JsonOutputProvider : IOutputConversionProvider
    {
        public string ProviderName => "Json Output Provider";
        
        public string Description => "This Provider outputs the conversion originally how the PlayFabApi returns it.";

        public async Task ExportData(string filename, IEnumerable<SteamPlayFabIdPair> conversionResult)
        {
            var encodedJson = JsonConvert.SerializeObject(conversionResult, Formatting.Indented);
            await File.WriteAllTextAsync(filename, encodedJson);
        }
    }
}
