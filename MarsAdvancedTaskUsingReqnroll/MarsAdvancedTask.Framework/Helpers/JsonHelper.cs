using Newtonsoft.Json;

namespace MarsAdvancedTask.Framework.Helpers
{
    public class JsonHelper
    {
        //Read Json from a file and deserializes it into a model object
        public static T ReadJson<T>(string jsonPath)
        {
            // base = bin/Debug/net8.0
            var basePath = AppContext.BaseDirectory;
            var fullPath = Path.Combine(basePath, jsonPath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"JSON file not found:{fullPath}");

            var jsonData = File.ReadAllText(fullPath);
            var obj = JsonConvert.DeserializeObject<T>(jsonData);

            if (obj == null)
                throw new InvalidOperationException($"Couldn't deserialize {fullPath} into {typeof(T).Name}");

            return obj;
        }
    }
}
