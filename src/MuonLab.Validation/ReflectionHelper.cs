using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MuonLab.Validation
{
	internal static class ReflectionHelper
    {
        public static string PropertyChainToString(Expression expression, char delimeter)
        {
            if (expression is LambdaExpression)
            {
                var memberExpression = expression as LambdaExpression;
                return DoPropertyChainToString(memberExpression.Body, delimeter).TrimEnd(delimeter);
            }

            throw new NotSupportedException("Probably a nullable type, need implementing! Debug: `" + expression + "`");
        }

		static string DoPropertyChainToString(Expression expression, char delimeter)
        {
            if (expression is MemberExpression)
            {
                var exp = expression as MemberExpression;
                return DoPropertyChainToString(exp.Expression, delimeter) + exp.Member.Name + delimeter;
            }

			if(expression is MethodCallExpression)
			{
				var exp = expression as MethodCallExpression;
				if(exp.Method.Name == "get_Item")
				{
					var index = ((exp.Arguments[0] as MemberExpression).Member as FieldInfo).GetValue(((exp.Arguments[0] as MemberExpression).Expression as ConstantExpression).Value);

					return DoPropertyChainToString((exp.Object as MemberExpression).Expression, delimeter) + (exp.Object as MemberExpression).Member.Name + "[" + index + "]" + delimeter;
				}
			}
            return string.Empty;
        }
    }
}