using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace HttpServer
{
    public class ServerSettings
    {
        public int Port { get; set; } = 7700;
        public string Path { get; set; } = @"./site/";
        public ServerSettings(int port, string path)
        {
            Port = port;
            Path = path;
        }
        public ServerSettings() { }

        public void Serialize()
        {
            //json serialization

            var jsonSerializer = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, jsonSerializer);

            using (StreamWriter streamWriter = new StreamWriter(@"Settings.JSON", false))
            {
                streamWriter.WriteLine(jsonString);
            }
        }
        public static ServerSettings Deserialize()
        {
            //json deserialization
            using (var fs = new FileStream(@"Settings.JSON", FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<ServerSettings>(fs);
            }
        }
    }
}
