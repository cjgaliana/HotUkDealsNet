using Newtonsoft.Json;

namespace HotUkDealsNetClient.Models
{
    public class Tag
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}