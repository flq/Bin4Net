using System;

namespace Bin4Net.Publish
{
    public class DependencyDefinition
    {
        public readonly string ProductName;
        public readonly Version MinimumVersion;
        public readonly Version MaximumVersion;
        public readonly bool? UseDepsCompatibilitySettings;

        private DependencyDefinition(string productName, string version, bool useCompatibilitySettings)
        {
            ProductName = productName;
            UseDepsCompatibilitySettings = useCompatibilitySettings;
            MinimumVersion = new Version(version);
        }

        public static DependencyDefinition From(string productName, string version, bool useCompatibilitySettings)
        {
            return new DependencyDefinition(productName, version, useCompatibilitySettings);
        }
    }
}