using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MundiPag.MessageTranslator.Api.Exchange
{
    public static class FactoryReflectionGenericExchange
    {
        public static T Create<T>(Dictionary<string, object> dictionary) where T : class, new()
        {

            T generic = new T();

            PropertyInfo[] properties = generic.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {

                Object @object = dictionary.
                    FirstOrDefault(v => v.Key.ToLower().Equals(prop.Name.ToLower())).Value;

                if (!(@object is null))
                {
                    Type type = @object.GetType();

                    if (@object.GetType() == typeof(string[]))
                    {
                        var toInsert = (string[])@object;
                        prop.SetValue(generic, string.Join(",", toInsert.ToList()).Split(','));
                    }
                    else if (@object.GetType() == typeof(JArray))
                    {
                        var toInsert = (JArray)@object;
                        prop.SetValue(generic, toInsert.ToObject(prop.PropertyType));
                    }
                    else
                    {
                        prop.SetValue(generic, Convert.ChangeType(@object, prop.PropertyType));
                    }
                }
            }

            return generic;
        }
    }
}
