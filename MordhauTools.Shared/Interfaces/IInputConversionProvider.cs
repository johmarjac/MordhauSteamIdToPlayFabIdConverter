using System.Threading.Tasks;

namespace MordhauTools.Shared.Interfaces
{
    public interface IInputConversionProvider
    {
        string ProviderName { get; }

        Task<string[]> ImportData(string inputFile);
    }
}
