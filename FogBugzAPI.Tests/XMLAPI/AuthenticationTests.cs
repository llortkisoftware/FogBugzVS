using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogBugzAPI.Model;
using FogBugzAPI.XMLAPI;
using NUnit.Framework;

namespace FogBugzAPI.Tests.XMLAPI
{
    [TestFixture]
    public class AuthenticationTests
    {
        
        [Test]
        public async void TestLogonGoodCredentialsBothUsernameAndPWStoredInFogBugzUrl()
        {
            /*
             * These credentials can either be stored in the configuration file, or hard coded in the tests.
             * Stored credentials is the safer option since it is easy to forget to removed hard coded credentials.
             */

            Configuration cfg = Configuration.Load();
            if (string.IsNullOrEmpty(cfg.BaseUrlList[0].ApiLocation))
            {
                ApiVersion version = await ApiVersion.GetApiVersion(cfg.BaseUrlList[0]);
                cfg.BaseUrlList[0].ApiLocation = version.ApiLocation;
                cfg.Save();
            }
            Authentication response = await Authentication.Logon(cfg.BaseUrlList[0]);
            Assert.IsFalse(response.IsError, "Error returned from good credentials.");
            Assert.IsNotNullOrEmpty(response.Token);
            cfg.BaseUrlList[0].Token = response.Token;
            cfg.Save();

        }

        [Test]
        public async void TestLogonGoodCredentialsUsernameOnlyInFogBugzUrl()
        {
            /*
             * These credentials can either be stored in the configuration file, or hard coded in the tests.
             * Stored credentials is the safer option since it is easy to forget to removed hard coded credentials.
             */

            Configuration cfg = Configuration.Load();
            if (string.IsNullOrEmpty(cfg.BaseUrlList[0].ApiLocation))
            {
                ApiVersion version = await ApiVersion.GetApiVersion(cfg.BaseUrlList[0]);
                cfg.BaseUrlList[0].ApiLocation = version.ApiLocation;
                cfg.Save();
            }
            Authentication response = await Authentication.Logon(cfg.BaseUrlList[0], cfg.BaseUrlList[0].DefaultPassword);
            Assert.IsFalse(response.IsError, "Error returned from good credentials.");
            Assert.IsNotNullOrEmpty(response.Token);
            cfg.BaseUrlList[0].Token = response.Token;
            cfg.Save();

        }
        [Test]
        public async void TestLogonBadCredentials()
        {
            /*
             * These credentials can either be stored in the configuration file, or hard coded in the tests.
             * Stored credentials is the safer option since it is easy to forget to removed hard coded credentials.
             */

            Configuration cfg = Configuration.Load();
            if (string.IsNullOrEmpty(cfg.BaseUrlList[0].ApiLocation))
            {
                ApiVersion version = await ApiVersion.GetApiVersion(cfg.BaseUrlList[0]);
                cfg.BaseUrlList[0].ApiLocation = version.ApiLocation;
                cfg.Save();
            }
            Authentication response = await Authentication.Logon(cfg.BaseUrlList[0], "user123", "pass123");
            Assert.IsTrue(response.IsError, "Error not returned from bad credentials.");
            cfg.BaseUrlList[0].Token = "";
            cfg.Save();

        }

        [Test]
        public async void TestLogoff()
        {
            Configuration cfg = Configuration.Load();
            Authentication response;
            if (string.IsNullOrEmpty(cfg.BaseUrlList[0].Token))
            {
                response = await Authentication.Logon(cfg.BaseUrlList[0]);
                cfg.BaseUrlList[0].Token = response.Token;

                if (response.IsError)
                {
                    Assert.Fail("Could not login to test logoff. Error: "+ response.ErrorMessage);
                }

            }
            cfg.Save();
            await Authentication.LogOff(cfg.BaseUrlList[0]);
            Assert.IsFalse(await Authentication.IsValidToken(cfg.BaseUrlList[0]), "Token still valid after logoff!");
            cfg.BaseUrlList[0].Token = "";
            cfg.Save();
        }

        [Test]
        public async void TestIsValidToken()
        {
            Configuration cfg = Configuration.Load();
            Authentication response;
            if (string.IsNullOrEmpty(cfg.BaseUrlList[0].Token))
            {
                response = await Authentication.Logon(cfg.BaseUrlList[0]);
                cfg.BaseUrlList[0].Token = response.Token;

                if (response.IsError)
                {
                    Assert.Fail("Could not login to test token. Error: " + response.ErrorMessage);
                }

            }

            Assert.IsTrue(await Authentication.IsValidToken(cfg.BaseUrlList[0]), "Valid token returned false");
            await Authentication.LogOff(cfg.BaseUrlList[0]);
            Assert.IsFalse(await Authentication.IsValidToken(cfg.BaseUrlList[0]), "Invalid token returned true");
            cfg.BaseUrlList[0].Token = "";
            cfg.Save();
        }
    }
}
