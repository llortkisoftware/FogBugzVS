using FogBugzAPI.FogBugzClient;
using FogBugzAPI.Model;
using NUnit.Framework;

namespace FogBugzAPI.Tests
{
    [TestFixture]
    public class AuthenticationTests
    {

        [Test]
        public async void TestLogonGoodCredentialsBothUsernameAndPWStoredInFogBugzUrlThenLogOff()
        {
            /*
             * These credentials can either be stored in the configuration file, or hard coded in the tests.
             * Stored credentials is the safer option since it is easy to forget to removed hard coded credentials.
             */

            Configuration cfg = Configuration.Load();
            FogBugzClientAsync fogBugzClient = new FogBugzClientAsync(cfg.BaseUrlList[0]);

            AuthenticationResponse authenticationResponse = await fogBugzClient.LogonAsync(cfg.BaseUrlList[0]);
            var authenticationErrorResponse = authenticationResponse as AuthenticationErrorResponse;
            if (authenticationErrorResponse != null)
            {
                Assert.Fail("Error returned from good credentials: " + authenticationErrorResponse.ErrorResponse);
            }
            Assert.IsNotNullOrEmpty(authenticationResponse.Token);
            var logoffResponse = await fogBugzClient.LogoffAsync();
            Assert.IsFalse(logoffResponse.IsError, "Error returned from logoff");
        }

        [Test]
        public async void TestLogonGoodCredentialsUsernameOnlyInFogBugzUrlThenLogOff()
        {
            /*
             * These credentials can either be stored in the configuration file, or hard coded in the tests.
             * Stored credentials is the safer option since it is easy to forget to removed hard coded credentials.
             */

            Configuration cfg = Configuration.Load();
            FogBugzClientAsync fogBugzClient = new FogBugzClientAsync(cfg.BaseUrlList[0]);

            AuthenticationResponse authenticationResponse = await fogBugzClient.LogonAsync(cfg.BaseUrlList[0].DefaultUsername, cfg.BaseUrlList[0].DefaultPassword);
            var authenticationErrorResponse = authenticationResponse as AuthenticationErrorResponse;
            if (authenticationErrorResponse != null)
            {
                Assert.Fail("Error returned from good credentials: " + authenticationErrorResponse.ErrorResponse);
            }
            Assert.IsNotNullOrEmpty(authenticationResponse.Token);
            var logoffResponse = await fogBugzClient.LogoffAsync();
            Assert.IsFalse(logoffResponse.IsError, "Error returned from logoff");
        }

        [Test]
        public async void TestLogonBadCredentials()
        {
            /*
             * These credentials can either be stored in the configuration file, or hard coded in the tests.
             * Stored credentials is the safer option since it is easy to forget to removed hard coded credentials.
             */

            Configuration cfg = Configuration.Load();
            FogBugzClientAsync fogBugzClient = new FogBugzClientAsync(cfg.BaseUrlList[0]);

            AuthenticationResponse response = await fogBugzClient.LogonAsync("user123", "pass123");

            Assert.IsTrue(response is AuthenticationErrorResponse, "Error not returned from bad credentials.");
        }

        [Test]
        public async void TestIsValidToken()
        {
            Configuration cfg = Configuration.Load();
            FogBugzClientAsync fogBugzClient = new FogBugzClientAsync(cfg.BaseUrlList[0]);
            AuthenticationResponse response = await fogBugzClient.LogonAsync(cfg.BaseUrlList[0]);
            var authenticationErrorResponse = response as AuthenticationErrorResponse;
            if (authenticationErrorResponse != null)
            {
                Assert.Fail("Could not log in to test token: " + authenticationErrorResponse.ErrorResponse);
            }

            var tokenResponse = await fogBugzClient.ValidateTokenAsync(response.Token);
            Assert.AreEqual(response.Token, tokenResponse.Token, "Valid token returned false");
            await fogBugzClient.LogoffAsync();
            var validateInvalidResponse = await fogBugzClient.ValidateTokenAsync(response.Token);
            Assert.IsNullOrEmpty(validateInvalidResponse.Token, "Invalid token returned value");
        }
    }
}
