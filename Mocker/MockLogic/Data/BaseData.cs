using MockLogic.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockLogic.data
{
    public class BaseData : ILanguageRef
    {
        public BaseData(string language = Constants.DEFAULT_LANGUAGE)
        {
            this.Language = language;
            this.Category = this.GetType().Name.ToLower();
            this.Random = new RandomGenerator();
        }

        public RandomGenerator Random { get; set; }

        /// <summary>
        /// The category name of inside the language
        /// </summary>
        protected string Category { get; set; }

        /// <summary>
        /// Current language of the data set.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Accesses the JSON path of a language dataset and returns the JToken.
        /// </summary>
        public JToken Get(string keyOrSubPath)
        {
            return CrowdSourcedData.Get(this.Category, keyOrSubPath, Language);
        }

        /// <summary>
        /// Access key of a locale data set and returns it as a JArray.
        /// </summary>
        public JArray GetArray(string keyOrSubPath)
        {
            return (JArray)Get(keyOrSubPath);
        }

        /// <summary>
        /// Access key of a locale data set and returns it as a JObject.
        /// </summary>
        public JObject GetObject(string key)
        {
            return (JObject)Get(key);
        }

        /// <summary>
        /// Access key of a locale data set and returns a random element.
        /// </summary>
        public string GetRandomArrayItem(string keyOrSubPath)
        {
            return Random.ArrayElement(GetArray(keyOrSubPath));
        }
    }
}
