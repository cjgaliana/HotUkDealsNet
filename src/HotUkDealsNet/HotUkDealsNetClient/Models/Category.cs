using Newtonsoft.Json;

namespace HotUkDealsNetClient.Models
{
    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url_name")]
        public string UrlName { get; set; }
    }
}