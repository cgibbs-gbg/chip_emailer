using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChipEmailer.Queries
{
    public class QueryCache
    {
        private static readonly IDictionary<string, string> Cache = new Dictionary<string, string>();

        static QueryCache()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var thisNamespace = typeof(QueryCache).Namespace + ".";
            var resourceNames = assembly.GetManifestResourceNames()
                .Where(m => m.StartsWith(thisNamespace) && m.EndsWith(".sql"));
            foreach (var resourceName in resourceNames)
            {
                var cleanResourceName = resourceName.Replace(thisNamespace, "");
                var resourceStream = assembly.GetManifestResourceStream(resourceName);
                if (resourceStream == null)
                {
                    continue;
                }
                
                using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
                {
                    Cache[cleanResourceName] = reader.ReadToEnd();
                }
            }
        }

        public static string GetQuery(string key)
        {
            if (Cache.ContainsKey(key))
            {
                return Cache[key];
            }
                
            throw new InvalidOperationException($"Missing script: {key}");
        }
    }
}