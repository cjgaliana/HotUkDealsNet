using System.Collections.Generic;
using Newtonsoft.Json;

namespace HotUkDealsNetClient.Models
{
    public class Tags
    {
        [JsonProperty("items")]
        public List<Tag> ItemTags { get; set; }
    }
}