using System.Collections.Generic;
using Newtonsoft.Json;

namespace HotUkDealsNetClient.Models
{
    public class Deals
    {
        [JsonProperty("items")]
        public List<HotDeal> DealsItems { get; set; }
    }
}