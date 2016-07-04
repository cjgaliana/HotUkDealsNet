using Newtonsoft.Json;

namespace HotUkDealsNetClient.Models
{
    public class HotDeal
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("deal_link")]
        public string DesktopDealLink { get; set; }

        [JsonProperty("mobile_deal_link")]
        public string MobileDealLink { get; set; }

        [JsonProperty("deal_image")]
        public string ImageSmall { get; set; }

        [JsonProperty("deal_image_highres")]
        public string ImageFull { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("submit_time")]
        public string SubmitTime { get; set; }

        [JsonProperty("hot_time")]
        public string HotTime { get; set; }

        [JsonProperty("poster_name")]
        public string PosterName { get; set; }

        [JsonProperty("temperature")]
        public float Temperature { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("expired")]
        public bool HasExpired { get; set; }

        [JsonProperty("forum")]
        public Forum Forum { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; }

        [JsonProperty("tags")]
        public Tags Tags { get; set; }
    }
}