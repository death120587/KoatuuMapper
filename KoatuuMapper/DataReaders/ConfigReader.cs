using Common.Model;
using Newtonsoft.Json;
using System.IO;

namespace KoatuuMapper.DataReaders
{
    public class ConfigReader
    {
        private readonly string _path;
        public ConfigReader(string path)
        {
            _path = path;
        }
        public MapperConfig GetConfig()
        {
            MapperConfig result;
            using (var reader = new StreamReader($"{_path}\\appsettings.json"))
            {
                result = JsonConvert.DeserializeObject<MapperConfig>(reader.ReadToEnd());
                reader.Close();
            }
            return result;
        }
    }
}
