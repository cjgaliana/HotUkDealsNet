using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotUkDealsNetClient.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void InitializeTest()
        { }

        [TestMethod]
        public async Task TestMethod1()
        {
            var client = new HotUkDealsClient("YOUR API KEY HERE");
            var data = await client.GetHottestDealsAsync();


            Assert.AreEqual(20, data.Count);
        }
    }
}
