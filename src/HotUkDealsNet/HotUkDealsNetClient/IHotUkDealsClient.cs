using System.Collections.Generic;
using System.Threading.Tasks;
using HotUkDealsNetClient.Models;

namespace HotUkDealsNetClient
{
    public interface IHotUkDealsClient
    {
        Task<List<HotDeal>> GetLatestDealsAsync(int page = 1);

        Task<List<HotDeal>> GetHottestDealsAsync(int page = 1);

        Task<List<HotDeal>> SearchtDealsAsync(string keyword, int page = 1);
    }
}