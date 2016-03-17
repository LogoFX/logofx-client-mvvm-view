using System.Collections.Generic;
using System.Reflection;

namespace LogoFX.Client.Mvvm.View.Localization
{
    public sealed class LocalAssemblyCollection : Dictionary<AssemblyName, ResourceSetCollection>
    {
        public string Path { get; set; }
    }
}
