using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DealerConceptsApp
{
    public static class DictionaryExt
    {
        private static JsonSerializerSettings _byValueSettings;
        private static JsonSerializerSettings _byNameSettings;

        static DictionaryExt()
        {
            _byValueSettings = new JsonSerializerSettings();
            _byValueSettings.Converters.Add(new KeyValuePairConverter());
            _byValueSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            _byValueSettings.Formatting = Formatting.Indented;

            _byNameSettings = new JsonSerializerSettings();
            _byNameSettings.Converters.Add(new KeyValuePairConverter());
            _byNameSettings.Formatting = Formatting.Indented;
        }

        public static Dictionary<string, string> ToDictionary(this Enum @enum)
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().ToDictionary(e => e.ToString(), e => Enum.GetName(type, e));
        }


        public static Dictionary<string, string> ToDictionary<T>(this T @enum) where T : struct
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().ToDictionary(e => e.ToString(), e => Enum.GetName(type, e));
        }

        public static KeyValuePair<int, string>[] ToJsonDictionary<T>(this T @enum) where T : struct
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().Select(e => new
                KeyValuePair<int, string>(Convert.ToInt32(e.ToString()), Enum.GetName(type, e))).ToArray();
        }

        public static Dictionary<string, int> ToDictionary<T>(this T @enum, int minValue) where T : struct
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().Where(i => i >= minValue).ToDictionary(e => Enum.GetName(type, e), e => e);
        }

        public static Dictionary<int, string> ToDictionaryByValue<T>(this T @enum, int minValue = 0) where T : struct
        {
            Type type = @enum.GetType();

            Dictionary<int, string> dic = new Dictionary<int, string>();

            foreach (var item in Enum.GetValues(@enum.GetType()))

            {
                int val = (int)item;
                if (val >= minValue)
                {
                    dic.Add(val, item.ToString());

                }

            }

            return dic;
        }

        public static MvcHtmlString EnumToJson(this Type type, int minValue = 0)
        {
            if (!type.IsEnum) { throw new ArgumentException("Must be an Enum type", "type"); }

            Dictionary<string, Dictionary<string, object>> dict = Enum.GetValues(type).Cast<int>().Where(v => v >= minValue).ToDictionary(v => Enum.GetName(type, v), v =>
            {
                string name = Enum.GetName(type, v);
                string display = type.GetMember(name)
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    ?.GetName();
                return new Dictionary<string, object>
                {
                    { "value", v },
                    { "displayName", display ?? name }
                };
            });

            string json = JsonConvert.SerializeObject(dict, _byNameSettings);

            return new MvcHtmlString(json);
        }


        public static MvcHtmlString EnumToJsonByValue(this Type type, int minValue = 0)
        {
            if (!type.IsEnum) { throw new ArgumentException("Must be an Enum type", "type"); }

            Dictionary<int, Dictionary<string, string>> dict = new Dictionary<int, Dictionary<string, string>>();

            foreach (var item in Enum.GetValues(type))

            {
                int val = (int)item;
                if (val >= minValue)
                {
                    string enumerator = item.ToString();
                    string display = type.GetMember(enumerator)
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        ?.GetName();

                    dict.Add(val, new Dictionary<string, string>
                    {
                        { "Name", enumerator },
                        { "DisplayName", display ?? enumerator }
                    });

                }
            }

            string json = JsonConvert.SerializeObject(dict, _byValueSettings);

            return new MvcHtmlString(json);
        }
    }
}