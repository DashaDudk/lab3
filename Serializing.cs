using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static lab3_Dudka.Serializing;
using System.Windows.Forms;

namespace lab3_Dudka
{
    internal class Serializing
    {
        public class Researcher
        {
            public string id { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string degree { get; set; }
            public string dateofreceiving { get; set; }
        }

        public class Transform
        {
            public void SerializeResearcherToJsonFile(List<Researcher> researchers, string filePath)
            {
                try
                {
                    string jsonData = System.Text.Json.JsonSerializer.Serialize(researchers);
                 
                    File.WriteAllText(filePath, jsonData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка серіалізації: {ex.Message}");
                }
            }

           public List<Researcher> DeserializeResearcherFromJsonFile(string filePath)
            {
                try
                {
                    string jsonData = File.ReadAllText(filePath);
                    List<Researcher> researchers = System.Text.Json.JsonSerializer.Deserialize<List<Researcher>>(jsonData);

                    return researchers;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка десеріалізації: {ex.Message}");
                    return null;
                }
            }
        }
    }
}