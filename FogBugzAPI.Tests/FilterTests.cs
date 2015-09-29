using FogBugzAPI.FogBugzClient;
using FogBugzAPI.FogBugzClient.Command;
using NUnit.Framework;

namespace FogBugzAPI.Tests
{
    [TestFixture]
    public class FilterTests
    {
        [Test]
        public async void TestGetFilters()
        {
            Configuration cfg = Configuration.Load();
            FogBugzClientAsync fogBugzClient = new FogBugzClientAsync(cfg.BaseUrlList[0]);

            var auth = await fogBugzClient.LogonAsync(cfg.BaseUrlList[0]);

            var filters = await fogBugzClient.ExecuteAsync(new ListFiltersCommand());
            
            Assert.IsTrue(filters.Count > 0, "No filters!");

            var logoff = await fogBugzClient.LogoffAsync();
            Assert.IsFalse(logoff.IsError, "Error with logoff");
        }

        [Test]
        public async void TestSetFilterThrowsNoExceptions()
        {
            Configuration cfg = Configuration.Load();
            FogBugzClientAsync fogBugzClient = new FogBugzClientAsync(cfg.BaseUrlList[0]);

            var auth = await fogBugzClient.LogonAsync(cfg.BaseUrlList[0]);

            var filters = await fogBugzClient.ExecuteAsync(new ListFiltersCommand());
            
            var setresult = await fogBugzClient.ExecuteAsync(new SetFilterCommand(filters[0]));

            var logoff = await fogBugzClient.LogoffAsync();
            Assert.IsFalse(logoff.IsError, "Error with logoff");
        }
    }


}
