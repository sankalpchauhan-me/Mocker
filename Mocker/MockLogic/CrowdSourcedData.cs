using MockLogic.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace MockLogic
{
    /// <summary>
    /// Reads the crowdsourced data
    /// </summary>
    public static class CrowdSourcedData
    {
        public static Lazy<JObject> Data = new Lazy<JObject>(Initialize, isThreadSafe: true);

        private static JObject Initialize()
        {
            var asm = typeof(CrowdSourcedData).Assembly;
            var root = new JObject();
            string s1 = System.AppDomain.CurrentDomain.BaseDirectory+"..\\" + Assembly.GetCallingAssembly().GetName().Name +"\\CrowdSource";

            foreach (var file in Directory.EnumerateFiles(s1, "*"+ Constants.ENDING_MATCHER))
            {
                using (var sr = new StreamReader(file))
                {
                    var serializer = new JsonSerializer();
                    var language = serializer.Deserialize<JObject>(new JsonTextReader(sr));
                    var props = language.Properties();
                    foreach (var prop in props)
                        root.Add(prop.Name, prop.First);
                }
            }
            return root;
        }

        /// <summary>
        /// Returns the JToken of key. If the key does not exist, then the fallback language is used.
        /// </summary>
        public static JToken Get(string category, string key, string language = Constants.DEFAULT_LANGUAGE, string languageFallback = Constants.DEFAULT_LANGUAGE)
        {
            var path = string.Format("{0}.{1}.{2}", language, category, key);
            var jtoken = Data.Value.SelectToken(path);

            if (jtoken != null)
            {
                return jtoken;
            }

            //fallback path
            var fallbackPath = string.Format("{0}.{1}.{2}", languageFallback, category, key);

            return Data.Value.SelectToken(fallbackPath, errorWhenNoMatch: true);
        }
    }
}
