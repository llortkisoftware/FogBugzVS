using FogBugzAPI.FogBugzClient;
using FogBugzAPI.Model;
using NUnit.Framework;

namespace FogBugzAPI.Tests
{
    [TestFixture]
    public class ApiVersionTests
    {
        [Test]
        public async void TestGetAPIVersion()
        {
            Configuration cfg = Configuration.Load();
            FogBugzClientAsync client = new FogBugzClientAsync(cfg.BaseUrlList[0]);

            ApiVersion ver = client.ApiVersion;

            Assert.AreEqual("api.asp?", ver.ApiLocation);
            Assert.AreEqual(8, ver.Version);
            Assert.AreEqual(1, ver.MinimumCompatibleApi);
        }
    }
}
