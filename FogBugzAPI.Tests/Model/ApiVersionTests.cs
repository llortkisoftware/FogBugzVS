using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogBugzAPI.Model;
using NUnit.Framework;

namespace FogBugzAPI.Tests.Model
{
    [TestFixture]
    public class ApiVersionTests
    {
        [Test]
        public async void TestGetAPIVersion()
        {
            Configuration cfg = Configuration.Load();
            Assert.AreEqual(2, cfg.BaseUrlList.Count);
            ApiVersion ver = await ApiVersion.GetApiVersion(cfg.BaseUrlList[0]);

            Assert.AreEqual("api.asp?", ver.ApiLocation);
            Assert.AreEqual(8, ver.Version);
            Assert.AreEqual(1, ver.MinimumCompatibleApi);
        }
    }
}
