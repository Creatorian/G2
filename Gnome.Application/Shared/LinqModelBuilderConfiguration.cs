using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public class LinqModelBuilderConfiguration
    {
        public Dictionary<Type, List<ModelPropertyConfiguration>> TypeConfiguration = new Dictionary<Type, List<ModelPropertyConfiguration>>();
        public ModelBinderBuilder<T> ForModel<T>()
        {
            if (TypeConfiguration.ContainsKey(typeof(T)))
            {
                return new ModelBinderBuilder<T>(TypeConfiguration[typeof(T)]);
            }

            List<ModelPropertyConfiguration> list = new List<ModelPropertyConfiguration>();
            TypeConfiguration.Add(typeof(T), list);
            return new ModelBinderBuilder<T>(list);
        }


        public void ApplyConfiguration<T>(ILinqModelBinderConfiguration<T> model)
        {
            ModelBinderBuilder<T> builder = ForModel<T>();
            model.Configure(builder);
        }
    }
}
