using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogBugzAPI.Model;
using FogBugzAPI.XMLAPI;
using NUnit.Framework;

namespace FogBugzAPI.Tests.Model
{
    [TestFixture]
    public class FilterTests
    {
        [Test]
        public void TestGetFilters()
        {
            Configuration cfg = Configuration.Load();

            Task < Authentication > authTask = Authentication.Logon(cfg.BaseUrlList[0]);

            authTask.Wait();

            Authentication auth = authTask.Result;
            cfg.BaseUrlList[0].Token = auth.Token;
            Task<bool> validToken = Authentication.IsValidToken(cfg.BaseUrlList[0]);
            validToken.Wait();
            Assert.IsTrue(validToken.Result, "Token invalid after logon");
            Task<List<Filter>> filters = Filter.GetFiltersAsync(cfg.BaseUrlList[0]);
            filters.Wait();
            Assert.IsTrue(filters.Result.Count > 0, "No filters!");

            Task logoffTask = Authentication.LogOff(cfg.BaseUrlList[0]);
            logoffTask.Wait();
        }

        [Test]
        public void TestSetFilterThrowsNoExceptions()
        {
            Configuration cfg = Configuration.Load();

            Task<Authentication> authTask = Authentication.Logon(cfg.BaseUrlList[0]);

            authTask.Wait();

            Authentication auth = authTask.Result;
            cfg.BaseUrlList[0].Token = auth.Token;
            Task<bool> validToken = Authentication.IsValidToken(cfg.BaseUrlList[0]);
            validToken.Wait();
            Task<List<Filter>> filters = Filter.GetFiltersAsync(cfg.BaseUrlList[0]);
            filters.Wait();

            Filter.SetFilterAsync(cfg.BaseUrlList[0], filters.Result[0]).Wait();

            Task logoffTask = Authentication.LogOff(cfg.BaseUrlList[0]);
            logoffTask.Wait();
        }
    }


}
