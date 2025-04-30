using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System;
using System.Linq;

namespace Gnome.Application.Shared
{
    public class ModelBinderBuilder<T>
    {
        public readonly List<ModelPropertyConfiguration> ModelPropertyConfiguration;

        public ModelBinderBuilder(List<ModelPropertyConfiguration> list)
        {
            ModelPropertyConfiguration = list;
        }

        public ModelPropertyConfiguration ForMember(Expression<Func<T, object>> expr)
        {
            string propertyName = GetCorrectPropertyName(expr);
            ModelPropertyConfiguration modelPropertyConfiguration = ModelPropertyConfiguration.Where((ModelPropertyConfiguration x) => x.PropertyName.Equals(propertyName)).FirstOrDefault();
            if (modelPropertyConfiguration == null)
            {
                modelPropertyConfiguration = new ModelPropertyConfiguration(propertyName);
                ModelPropertyConfiguration.Add(modelPropertyConfiguration);
            }

            return modelPropertyConfiguration;
        }

        public void ForAllOtherPropertiesUseBody()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!ModelPropertyConfiguration.Any((ModelPropertyConfiguration x) => x.PropertyName.Equals(propertyInfo.Name)))
                {
                    ModelPropertyConfiguration item = new ModelPropertyConfiguration(propertyInfo.Name).FromBody();
                    ModelPropertyConfiguration.Add(item);
                }
            }
        }

        private static string GetCorrectPropertyName(Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            return ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;
        }
    }
}