using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SuckerBot
{
    class Utilities
    {
        private static Dictionary<string, string> alerts;

        /*Utilities Function is to provide support in terms of Alerts and helping the misc class*/
        /*this was cut short, maybe subjected to change if I have time in future*/

        static Utilities()
        {
            string json = File.ReadAllText("SystemLanguage/alert.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            alerts = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetAlert(string key)
        {
            if (alerts.ContainsKey(key))
            {
                return alerts[key];
            }
            else
            {
                return "Error 3: Wrong Key";
            }

        }

        public static string FormatString(string key, params object[] parameter)
        {
            if (alerts.ContainsKey(key))
            {
                return string.Format(alerts[key], parameter);
            }
            else
            {
                return "Error 3: Key Not Found";
            }

        }

    }
}
