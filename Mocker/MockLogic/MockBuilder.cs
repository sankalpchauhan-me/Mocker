using MockLogic.Temp;
using MockLogic.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MockLogic
{
    public class MockBuilder
    {
        string[] PropertyNames;
        string[] TypesString;
        string className;
        ClassMaker _builder;
        public MockBuilder(string[] PropertyNames, string[] TypesString, string className)
        {
            this.PropertyNames = PropertyNames;
            this.TypesString = TypesString;
            this.className = className;
            _builder = new ClassMaker(className);
        }

        public object GetMocks()
        {
            Mocker mock = new Mocker();
            List<object> list = new List<object>();
            var generatedObject = _builder.CreateDynamicClass(PropertyNames, TypesString);
            Type TP = generatedObject.GetType();
            foreach (PropertyInfo PI in TP.GetProperties())
            {
                var businessObjectPropValue = PI.GetValue(generatedObject, null);
                PI.SetValue(generatedObject, mock.Name.FirstName(), null);
                //PI.SetValue("FirstName", mock.Name.FirstName());
                list.Add(PI.Name);
            }
            //var testEmployee = new MockerGeneric<TempEmployee>()
            //    .Condition(o => o.FirstName, m => m.Name.FirstName())
            //    .Condition(o => o.LastName, m => m.Name.LastName())
            //    .Condition(o => o.id, m => m.Random.Number(1, 100));
            //var user = testEmployee.Mock(1000);
            return (generatedObject);
        }
    }
}
