using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using HotUkDealsNetClient.Models;
using Newtonsoft.Json;

namespace HotUkDealsNetClient
{
    public class HotUkDealsClient
    {
        #region API Constants

        private const string API_BASE_URL = "http://api.hotukdeals.com/rest_api";
        private const string API_VERSION = "v2";

        private readonly string API_KEY;

        private const string API_RESPONSE_FORMAT = "json";

        #endregion API Constants

        #region API parameters

        /// <summary>
        /// This parameter is used to determine what type of output you would like to receive in the response. Omitting this parameter will default to xml output. Currently the possible options are: json, xml
        /// </summary>
        private const string OUTPUT_TAG = "&output";

        /// <summary>
        /// This parameter is used to determine what forum you would like to see deals from. It uses the special "url name" for the forums. Omitting this parameter will default to all deals. The possible options are: all, deals, vouchers, freebies, competitions, deal-requests, for-sale-trade, misc, and feedback
        /// </summary>
        private const string FORUM_TAG = "&forum";

        /// <summary>
        /// This parameter is used to determine what category you would like to see deals from. It uses the special "url name" for the different categories. Omitting this parameter will default to all deals. The possible options are: computers, audiovisual, entertainment, fashion, home, mobiles, travel, groceries, kids, other-deals, gaming, restaurant
        /// </summary>
        private const string CATEGORY_TAG = "&category";

        /// <summary>
        /// This parameter is used to determine what merchant you would like to see deals from, it uses the special url name for merchants. To determine what the url name for a particular merchant is, replace all non alphanumeric characters with a hypen ( - max one between two words). For example the merchant "bob & charlie" would become "bob-charlie".
        /// </summary>
        private const string MERCHANT_TAG = "&merchant";

        /// <summary>
        /// This parameter allows you to retrieve deals with particular tags, it also takes special url names, which can be made by replacing all non alphanumeric characters with hypens (max one between two words).
        /// </summary>
        private const string TAGS_TAG = "&tag";

        /// <summary>
        /// This parameter allows you to retrieve deals from particular users, valid values for this parameter are valid HUKD user names.
        /// </summary>
        private const string USERNAME_TAG = "&username";

        /// <summary>
        /// This parameter allows you to filter between online and offline deals. Omitting this parameter will default to all deals. The possible values are: online, offline. Note: If used in combination with the search parameter, only online is available.
        /// </summary>
        private const string ONLINE_OFFLINE_TAG = "&online_offline";

        /// <summary>
        /// This parameter determines the order the deals are returned in. There are 3 possible options:
        ///  new: this will order the deals by submission date in descending order
        ///  discussed: this will order the deals by the last posted in date in descending order
        ///  hot: this will order the deals by the date they were voted hot by the users, in descending order
        /// </summary>
        private const string ORDER_TAG = "&order";

        /// <summary>
        /// This parameter, along with the results_per_page parameter allows you to paginate your results. You pass it the page number of the page you would like to view, so for example, "2" would give you the second set of results for a given filter configuration. (Note: Only the first 1000 results of any filter will be accessible).
        /// </summary>
        private const string PAGE_TAG = "&page";

        /// <summary>
        /// This parameter allows you to specify how many deals per request you would like returned. This value is limited to a maximum of 30 deals per request. The default is 20.
        /// </summary>
        private const string RESULTS_PER_PAGE_TAG = "&results_per_page";

        /// <summary>
        /// This parameter allows you to specify the minimum temperature rating of deals that are returned.
        /// </summary>
        private const string MIN_TEMPERATURE_TAG = "&min_temp";

        /// <summary>
        /// This parameter allows you to prevent expired deals from being returned.Set to true to exclude expired deals.
        /// </summary>
        private const string EXCLUDE_EXPIRED_TAG = "&exclude_expired";

        /// <summary>
        /// This parameter allows you to perform a search for a keyword or phrase and takes precedence over other parameters.It can be used in combination with the following parameters: output, forum, category, online_offline (see note in parameter description), page, results_per_page, exclude_expired. Any other parameters will be ignored.
        /// </summary>
        private const string SEARCH_TAG = "&search";

        #endregion API parameters

        #region Fields

        private readonly HttpClient _client;

        #endregion Fields

        #region Constructor

        public HotUkDealsClient(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key), "You need an API key to use the client");
            }

            this.API_KEY = key;
            this._client = new HttpClient();
            this.MapForums();
            this.MapOrderingTypes();
        }

        #endregion Constructor

        #region Public methods

        public async Task<List<HotDeal>> GetLatestDealsAsync(int page = 1)
        {
            string url = string.Format("{0}{1}{2}",
                this.GenerateBaseEndpoint(),
                this.GenerateApiParameter(PAGE_TAG, page.ToString()),
                this.GenerateApiParameter(ORDER_TAG, "new"));

            Debug.WriteLine("Calling HUKD API: " + url);
            var json = await this._client.GetStringAsync(new Uri(url));
            var response = JsonConvert.DeserializeObject<ApiResponse>(json);

            return response.Deals.DealsItems;
        }

        public async Task<List<HotDeal>> GetHottestDealsAsync(int page = 1)
        {
            string url = string.Format("{0}{1}{2}",
                this.GenerateBaseEndpoint(),
                this.GenerateApiParameter(PAGE_TAG, page.ToString()),
                this.GenerateApiParameter(ORDER_TAG, "hot"));

            Debug.WriteLine("Calling HUKD API: " + url);
            var json = await this._client.GetStringAsync(new Uri(url));
            var response = JsonConvert.DeserializeObject<ApiResponse>(json);

            return response.Deals.DealsItems;
        }

        public async Task<List<HotDeal>> SearchtDealsAsync(string keyword, int page = 1)
        {
            string url = string.Format("{0}{1}{2}",
                this.GenerateBaseEndpoint(),
                this.GenerateApiParameter(PAGE_TAG, page.ToString()),
                this.GenerateApiParameter(SEARCH_TAG, keyword));

            Debug.WriteLine("Calling HUKD API: " + url);
            var json = await this._client.GetStringAsync(new Uri(url));
            var response = JsonConvert.DeserializeObject<ApiResponse>(json);

            return response.Deals.DealsItems;
        }

        #endregion Public methods

        #region Private methods

        private string GenerateBaseEndpoint()
        {
            string finalURL = string.Format("{0}/{1}/?key={2}{3}",
                API_BASE_URL,
                API_VERSION,
                API_KEY,
                this.GenerateApiParameter(OUTPUT_TAG, API_RESPONSE_FORMAT)
                );
            return finalURL;
        }

        private string GenerateApiParameter(string tag, string value)
        {
            if (tag.StartsWith("&"))
            {
                tag = tag.Remove(0, 1);
            }
            string parameter = string.Format("&{0}={1}", tag, value);
            return parameter;
        }

        #endregion Private methods

        private void MapForums()
        {
            this.Forums = new Dictionary<ForumType, string>
            {
                {ForumType.All, "all"},
                {ForumType.Competitions, "competitions"},
                {ForumType.DealRequests, "deal-requests"},
                {ForumType.Deals, "deals"},
                {ForumType.Feedback, "feedback"},
                {ForumType.ForSaleTrade, "for-sale-trade"},
                {ForumType.Freebies, "freebies"},
                {ForumType.Misc, "misc"},
                {ForumType.Vouchers, "vouchers"}
            };
        }

        private void MapOrderingTypes()
        {
            this.OrderTypes = new Dictionary<OrderType, string>
            {
                { OrderType.Discussed, "discussed" },
                { OrderType.Hot, "hot" },
                { OrderType.New, "new" },
            };
        }

        public Dictionary<ForumType, string> Forums { get; set; }
        public Dictionary<OrderType, string> OrderTypes { get; set; }
    }
}
