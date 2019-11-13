using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Config
    {
        private const string ConfigFolder = "Resources";
        private const string ConfigFile = "config.json";
        private const string ConfigPath = ConfigFolder + "/" + ConfigFile;
        public static BotConfig bot;

        static Config()
        {
            if (!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);

            
            if (!File.Exists(ConfigPath))
            {
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented); //Indented - each element in new line
                File.WriteAllText(ConfigPath, json);
            }
            else
            {
                string json = File.ReadAllText(ConfigPath);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }
    }

    public struct BotConfig
    {
        public string token;
        public string cmdPrefix;
    }
}
