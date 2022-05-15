using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuirkyCoffeeMachine.Model
{
    public class BrewCoffeeModel
    {
        public string Message { get; set; }
        public DateTime CreatedDate => DateTime.Now;

        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public static List<BrewCoffeeModel> GetBrewCoffeeData()
        {
            string BrewCoffeeDataFile = Directory.GetCurrentDirectory()+"/Data/BrewCoffeeData.json";
            if (!File.Exists(BrewCoffeeDataFile))
                return new List<BrewCoffeeModel>();

            //I am going to assume the IO is always working, I'd usually do a try catch here
            string jsonString = File.ReadAllText(BrewCoffeeDataFile);
            List<BrewCoffeeModel> BrewCoffeeData = JsonSerializer.Deserialize<List<BrewCoffeeModel>>(jsonString);

            return BrewCoffeeData;
        }

        public static void SaveBrewCoffeeData(List<BrewCoffeeModel> brewCoffeeData)
        {
            string brewCoffeeDataFile = Directory.GetCurrentDirectory() + "/Data/BrewCoffeeData.json";            
            string jsonString = JsonSerializer.Serialize(brewCoffeeData);
            File.WriteAllText(brewCoffeeDataFile, jsonString);
        }
    }
}
