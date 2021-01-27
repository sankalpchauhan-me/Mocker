using MockLogic.data;
using MockLogic.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockLogic.Data
{
    public class Name : BaseData
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Name(string language = Constants.DEFAULT_LANGUAGE) : base(language)
        {
        }

        /// <summary>
        /// Gets a first name
        /// </summary>
        public string FirstName()
        {
            var get = (JArray)Get("first_name");

            return Random.ArrayElement(get);
        }

        /// <summary>
        /// Gets a last name
        /// </summary>
        public string LastName()
        {
            return GetRandomArrayItem("last_name");
        }

        /// <summary>
        /// Gets a random prefix for a name
        /// </summary>
        public string Prefix()
        {
            return GetRandomArrayItem("prefix");
        }

        /// <summary>
        /// Gets a random suffix for a name
        /// </summary>
        public string Suffix()
        {
            return GetRandomArrayItem("suffix");
        }

        /// <summary>
        /// Gets a full name
        /// </summary>
        public string FindName(string firstName = "", string lastName = "", bool? withPrefix = null, bool? withSuffix = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                firstName = FirstName();
            if (string.IsNullOrWhiteSpace(lastName))
                lastName = LastName();

            if (!withPrefix.HasValue && !withSuffix.HasValue)
            {
                withPrefix = Random.Bool();
                withSuffix = !withPrefix;
            }

            return string.Format("{0} {1} {2} {3}",
                withPrefix.GetValueOrDefault() ? Prefix() : "", firstName, lastName, withSuffix.GetValueOrDefault() ? Suffix() : "")
                .Trim();

        }

        /// <summary>
        /// Gets a random job title.
        /// </summary>
        public string JobTitle()
        {
            var descriptor = GetRandomArrayItem("title.descriptor");
            var level = GetRandomArrayItem("title.level");
            var job = GetRandomArrayItem("title.job");

            return string.Format("{0} {1} {2}", descriptor, level, job);
        }
    }
}
