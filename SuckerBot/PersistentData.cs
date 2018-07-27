using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuckerBot
{
    /*A Data class that will deal with all Persistent Data for the Mailing system*/

    class PersistentData
    {
        /*Local Dictionary for the current system Guild*/
        private static Dictionary<string, string> Data = new Dictionary<string, string>();

        /*Constructor helps in reading the latest data from the JSON File*/
        static PersistentData()
        {
            if (!validate("tickets.json")) return;
            string json = File.ReadAllText("tickets.json");
            Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        /*A local function for storing data to preserve data hiding*/
        /*This Function dates an id and message and stores it in the Local Data as well as the JSON File*/
        public static void DataStore(string id, string message)
        {
            Data.Add(id, message);
            SaveData();
        }

        /*Removes Data*/
        public static void DeleteMail(string id)
        {
            Data.Remove(id);
            SaveData();
        }

        /*Removes All Data*/
        public static void DeleteAll()
        {
            Data.Clear();
            SaveData();
        }

        /*A bool function to check weather a mail exists in the file*/
        public static bool DoesExist(string id)
        {
            if (Data.ContainsKey(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*Grabs all data for mail checking and displaying*/
        public static Dictionary<string, string> GetAllData()
        {
            return Data;
        }

        /*Checks the amount of enteries in the mailing system*/
        public static int GetCount()
        {
            return Data.Count();
        }

        /*Saves the Data to both local data storage as well as the JSON File*/
        public static void SaveData()
        {
            string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
            File.WriteAllText("tickets.json", json);
        }

        /*Used to check the validity of the file before loading the file*/
        private static bool validate(string file)
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, "");
                SaveData();
                return false;
            }
            return true;
        }

    }
}
