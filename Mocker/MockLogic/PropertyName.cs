using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MockLogic
{
    internal static class PropertyName
    {
        public static string For<T, TProp>(Expression<Func<T, TProp>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }
        public static string For<T>(Expression<Func<T, object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }
        public static string For(Expression<Func<object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }
        public static string GetMemberName(Expression expression)
        {
            MemberExpression memberExpression;

            var unary = expression as UnaryExpression;
            if (unary != null)
                memberExpression = unary.Operand as MemberExpression;
            else
                //when the property is of type object the body itself is the
                //correct expression
                memberExpression = expression as MemberExpression;

            if (memberExpression == null
                || !(memberExpression.Member is PropertyInfo))
                throw new ArgumentException(
                    "Expression was not of the form 'x =&gt; x.property'.");

            return memberExpression.Member.Name;
        }
    }
}
