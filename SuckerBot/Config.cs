using Newtonsoft.Json;
using System.IO;

namespace SuckerBot
{
    class Config
    {

        private const string configFolder = "Resources";
        private const string configFile = "config.json";

        public static BotConfig bot;

        /*A configuration File based on Json file*/
        /*You will need to replace the token number in there to use this code for your own Bot*/
        /*Location for the json file is /ProjectName/bin/Debug/Resources/config.json*/

        static Config()
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            if (!File.Exists(configFolder + "/" + configFile))
            {
                BotConfig bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }
    }

    /*a data structure to hold the required configuration data*/

    struct BotConfig
    {
        public string token;
        public string cmdPrefix;
    }
}
