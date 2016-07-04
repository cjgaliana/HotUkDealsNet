using Newtonsoft.Json;

namespace HotUkDealsNetClient.Models
{
    public class ApiResponse
    {
        [JsonProperty("deals")]
        public Deals Deals { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }
}