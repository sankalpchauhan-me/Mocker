using MockLogic.Data;
using MockLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockLogic
{
    public class Mocker
    {
        public static bool DefaultStrictMode = false;

        public Mocker(string language = Constants.DEFAULT_LANGUAGE)
        {
            this.Name = new Name(language);
            this.Random = new RandomGenerator();
        }

        public Name Name { get; set; }
        public RandomGenerator Random { get; set; }

        /// <summary>
        /// Helper method to pick a random element.
        /// </summary>
        public T PickRandom<T>(IEnumerable<T> items)
        {
            return this.Random.ArrayElement(items.ToArray());
        }

        /// <summary>
        /// Picks a random Enum of T. Applicable to only Enums.
        /// </summary>
        public T PickRandom<T>() where T : struct
        {
            var e = typeof(T);
            if (!e.IsEnum)
                throw new ArgumentException(" T must be an enum.");

            var val = this.Random.ArrayElement(Enum.GetNames(e));

            T picked;
            Enum.TryParse(val, out picked);
            return picked;
        }
    }
}
