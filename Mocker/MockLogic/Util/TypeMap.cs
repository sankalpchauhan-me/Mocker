using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockLogic.Util
{
    public static class TypeMap
    {

        public static Type GetType(string s) {
            Dictionary<string, Type> types = new Dictionary<string, Type>();
            types.Add("byte", typeof(byte));
            types.Add("short", typeof(short));
            types.Add("int", typeof(int));
            types.Add("long", typeof(long));
            types.Add("float", typeof(float));
            types.Add("double", typeof(double));
            types.Add("char", typeof(char));
            types.Add("string", typeof(string));
            types.Add("decimal", typeof(decimal));
            types.Add("bool", typeof(bool));
            types.Add("DateTime", typeof(DateTime));
            return types[s];
        }
    }
}
