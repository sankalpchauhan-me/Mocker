using MockLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MockLogic
{
    public class MockerGeneric<T>: ILanguageRef where T : class
    {
        protected internal Mocker Mocking;
        protected internal BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.NonPublic | BindingFlags.Public;
        protected internal Func<Mocker, T> CustomActivator;
        protected internal readonly Dictionary<string, Func<Mocker, T, object>> Actions = new Dictionary<string, Func<Mocker, T, object>>();
        protected internal readonly Lazy<Dictionary<string, PropertyInfo>> TypeProperties;
        protected internal bool? UseStrictMode;
        protected internal bool? IsValid;
        protected internal Action<Mocker, T> FinalizeAction;

        public MockerGeneric(string language = Constants.DEFAULT_LANGUAGE)
        {
            this.Language = language;
            Mocking = new Mocker(language);
            TypeProperties = new Lazy<Dictionary<string, PropertyInfo>>(() =>
            {
                return typeof(T).GetProperties(BindingFlags)
                    .ToDictionary(pi => pi.Name);
            });
        }

        /// <summary>
        /// Set the binding flags visibility when getting properties
        /// </summary>
        public MockerGeneric<T> UseBindingFlags(BindingFlags flags)
        {
            this.BindingFlags = flags;
            return this;
        }


        /// <summary>
        /// Uses the factory method to generate new instances.
        /// </summary>
        public MockerGeneric<T> CustomInstantiator(Func<Mocker, T> factoryMethod)
        {
            this.CustomActivator = factoryMethod;
            return this;
        }

        /// <summary>
        /// Creates a condition for a property and return instance being generated.
        /// </summary>
        public MockerGeneric<T> Condition<TProperty>(Expression<Func<T, TProperty>> property, Func<Mocker, T, TProperty> setter)
        {
            var propName = PropertyName.For(property);

            Func<Mocker, T, object> invoker = (f, t) => setter(f, t);

            this.Actions.Add(propName, invoker);

            return this;
        }

        /// <summary>
        /// Creates a Condition for a property.
        /// </summary>
        public MockerGeneric<T> Condition<TProperty>(Expression<Func<T, TProperty>> property, Func<Mocker, TProperty> setter)
        {
            var propName = PropertyName.For(property);

            Func<Mocker, T, object> invoker = (f, t) => setter(f);

            this.Actions.Add(propName, invoker);

            return this;
        }

        public MockerGeneric<T> StrictMode(bool ensureRulesForAllProperties)
        {
            UseStrictMode = ensureRulesForAllProperties;
            return this;
        }

        /// <summary>
        /// Action is invoked after all the conditions are applied.
        /// </summary>
        public MockerGeneric<T> FinishWith(Action<Mocker, T> action)
        {
            this.FinalizeAction = action;
            return this;
        }

        /// <summary>
        /// Generates a mock object of T.
        public virtual T Mock()
        {
            var instance = CustomActivator == null ? Activator.CreateInstance<T>() : CustomActivator(this.Mocking);

            Populate(instance);

            return instance;
        }

        /// <summary>
        /// Generates multiple mock objects of T.
        /// </summary>
        public virtual IEnumerable<T> Mock(int count)
        {
            return Enumerable.Range(1, count)
                .Select(i => Mock());
        }

        /// <summary>
        /// Only populates an instance of T.
        /// </summary>
        public virtual void Populate(T instance)
        {
            var useStrictMode = UseStrictMode ?? Mocker.DefaultStrictMode;
            if (useStrictMode && !IsValid.HasValue)
            {
                //run validation
                this.IsValid = Validate();
            }
            if (useStrictMode && !IsValid.GetValueOrDefault())
            {
                throw new InvalidOperationException(string.Format("Cannot generate {0} because strict mode is enabled on this type and some properties have no rules.",
                    typeof(T)));
            }

            var typeProps = TypeProperties.Value;

            foreach (var kvp in Actions)
            {
                PropertyInfo prop;
                typeProps.TryGetValue(kvp.Key, out prop);
                var valueFactory = kvp.Value;
                prop.SetValue(instance, valueFactory(Mocking, instance), null);
            }

            if (FinalizeAction != null)
            {
                FinalizeAction(this.Mocking, instance);
            }

        }

        /// <summary>
        /// Checks if all properties have rules.
        /// </summary>
        /// <returns>True if validation pases, false otherwise.</returns>
        public virtual bool Validate()
        {
            return TypeProperties.Value.Count == Actions.Count;
        }

        /// <summary>
        /// The current langauge.
        /// </summary>
        public string Language { get; set; }
    }
}
