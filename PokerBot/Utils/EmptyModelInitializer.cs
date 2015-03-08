using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public class EmptyModelInitializer
    {
        public static T EmptyModel<T>(T model)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                // will initialize to the default value of property type
                property.SetValue(model, null);
            }
            return model;
        }
    }
}
