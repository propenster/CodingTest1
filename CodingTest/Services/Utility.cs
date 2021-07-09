using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Services
{
    public static class Utility
    {


        public static async Task<T> ParseJsonToObjectAsync<T>(string FIlePath) where T : class
        {
            T output = null;
            try
            {
                using (var sr = new StreamReader(FIlePath))
                {
                    var jsonText = sr.ReadToEnd();
                    JObject jObject = JObject.Parse(jsonText);
                    var fees = jObject.SelectToken("fees");
                    output = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(fees.ToString()));
                    Console.WriteLine("Welcomelasldkad");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"The File Parsing Operation Failed: {ex.Message}");
            }

            return output;
        }

        public static T ParseJsonToObject<T>(string FIlePath) where T : class
        {
            T output = null;
            try
            {
                using (var sr = new StreamReader(FIlePath))
                {
                    var jsonText = sr.ReadToEnd();
                    //output = JsonConvert.DeserializeObject<T>(jsonText);
                    JObject jObject = JObject.Parse(jsonText);
                    var fees = jObject.SelectToken("fees");
                    output = JsonConvert.DeserializeObject<T>(fees.ToString());
                    Console.WriteLine("Welcomelasldkad");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"The File Parsing Operation Failed: {ex.ToString()}");
            }

            return output;
        }
    }
}
