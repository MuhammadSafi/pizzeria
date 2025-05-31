using System.Text.Json;

namespace pizzeria.Business
{
    public static class JsonLoader
    {
        public static List<T> LoadFromJson<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        public static string ReadJson(string filePath)
        {
            return File.ReadAllText(filePath);

        }
    }
}
