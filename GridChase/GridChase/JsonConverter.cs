using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GridChase {
    class JsonConverter {
        public Dictionary<string, string> toDictionary(string fileName) {
            string jsonFile = File.ReadAllText(fileName);
            Dictionary<string, string> result = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonFile);

            return result;
        }
    }
}
