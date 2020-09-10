using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace GridChase {
    class JsonConverter {
        public JArray toDictionary(string fileName) {
            string jsonFile = File.ReadAllText(fileName);
            JArray result = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(fileName);

            return result;
        }
    }
}
