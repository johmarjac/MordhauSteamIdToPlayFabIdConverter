using System.Reflection;
using System.Runtime.Loader;

namespace MordhauTools.Core
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}
