using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class DataStorage
    {
        private static Dictionary<string, string> pairs = new Dictionary<string, string>(); //internal - only this application has acces to it
        private const string FilePath = "DataStorage.json";

        public static void AddPairToStorage(string key, string value)
        {
            pairs.Add(key, value);
            SaveData();
        }

        public static int GetPairsCount() { return pairs.Count; }

        //Static contructor runs olny once - the first run
        static DataStorage() //Load data
        {
            if (!ValidateStorageFile(FilePath)) return;
            string json = File.ReadAllText(FilePath);
            pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public static void SaveData()
        {
            string json = JsonConvert.SerializeObject(pairs, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        private static bool ValidateStorageFile(string file)
        {
            if(!File.Exists(file))
            {
                File.WriteAllText(file, ""); //create the file
                SaveData();
                return false;
            }
            return true;
        }
    }
}
