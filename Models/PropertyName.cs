using System.Linq.Expressions;

namespace AzureSpellCheckDemo.Models
{
    public class PropertyName
    {

        public static string For(Expression<Func<object>> expression)
        {
            Expression body = expression.Body;
            string propertyName = GetMemberName(body);
            return propertyName;
        }

        public static string For<TModel>(Expression<Func<TModel, object>> expression)
        {
            Expression body = expression.Body;
            string propertyName = GetMemberName(body);
            return propertyName;
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)(expression);
                if (expression.NodeType == ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression) + "." + memberExpression.Member.Name;
                }
                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format(
                        "Cannot interpret member from {0}",
                        expression));

                return GetMemberName(unaryExpression.Operand);
            }

            throw new Exception(string.Format(
                "Could not determine member from {0}",
                expression));
        }
    }
}
