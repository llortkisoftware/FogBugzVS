using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using FogBugzAPI.Exceptions;
using FogBugzAPI.Model;
using FogBugzAPI.FogBugzClient;
using FogBugzAPI.FogBugzClient.Command;

namespace FogBugzAPI.FogBugzClient
{
    public class FogBugzClientAsync
    {

        private const int MyApiVersion = 8;

        private readonly Uri _baseUrl;
        public ApiVersion ApiVersion { get; private set; }
        private readonly string _apiPath;
        public string Token { get; set; }

        /// <summary>
        /// Creates a new instance of FogBugzClientAsync with the passed in URL.
        /// Synchronously Checks the API version on construction.
        /// </summary>
        /// <param name="fogBugzUrl">FogBugz remote Url</param>
        /// <exception cref="FogBugzException">Throws FogBugzException if API version is less than the supported api version</exception>
        public FogBugzClientAsync(FogBugzUrl fogBugzUrl)
        {
            _baseUrl = new Uri(fogBugzUrl.BaseUrl);
            Token = fogBugzUrl.Token;

            VerifyApiVersion();
            if (ApiVersion.Version < 8)
            {
                throw new FogBugzException("Remote API version must be at least 8, encountered " + ApiVersion.Version);
            }
            _apiPath = "/" + ApiVersion.ApiLocation;
        }

        private void VerifyApiVersion()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = _baseUrl;
            var response = client.GetAsync("/api.xml");
            response.Wait();
            var result = response.Result.Content.ReadAsStringAsync();
            result.Wait();
            XDocument document = XDocument.Parse(result.Result);

            ApiVersion = new ApiVersion(document);
        }

        /// <summary>
        /// Executes a FogBugz API command
        /// </summary>
        /// <typeparam name="T">The return type the command will create</typeparam>
        /// <param name="command">The FogBugz API command to run</param>
        /// <returns>FogBugz object created from the response.</returns>
        /// <exception cref="FogBugzException">Throws FogBugzException if a non logon command is received and no token has ben set.</exception>
        public async Task<T> ExecuteAsync<T>(IFogBugzCommand<T> command) where T : IFogBugzType
        {
            if (Token == null && !((command is LogonCommand) || (command is ValidateTokenCommand)))
            {
                throw new FogBugzException("Cannot run api commands when not logged in.");
            }

            HttpClient client = new HttpClient();

            client.BaseAddress = _baseUrl;
            var response = await client.GetAsync(_apiPath + GetCommand(command));
            var result = response.Content.ReadAsStringAsync();
            XDocument document = XDocument.Parse(await result);
            return command.CreateResponse(new FogBugzReturn(document));
        }

        private string GetCommand<T>(IFogBugzCommand<T> command) where T : IFogBugzType
        {
            StringBuilder commandString = new StringBuilder();
            commandString.Append("cmd=").Append(command.Command);
            foreach (var parameter in command.Parameters)
            {
                commandString.Append("&").Append(parameter.Key).Append("=").Append(WebUtility.UrlEncode(parameter.Value));
            }
            if (!(command is ValidateTokenCommand))
            {
                if (Token != null)
                {
                    commandString.Append("&").Append("token=").Append(WebUtility.UrlEncode(Token));
                }
            }
            return commandString.ToString();
        }

        /// <summary>
        /// Convenience method to execute a logon command
        /// </summary>
        /// <param name="defaults">Configuration object with a saved default username and password.</param>
        /// <returns>An authentication response. This may be of type <see cref="AuthenticationErrorResponse"/> if an authntication was returned</returns>
        public async Task<AuthenticationResponse> LogonAsync(FogBugzUrl defaults)
        {
            LogonCommand command = new LogonCommand(defaults);
            var response = await ExecuteAsync(command);
            if (response.Token != null)
            {
                Token = response.Token;
            }

            return response;
        }

        /// <summary>
        /// Convenience method to execute a logon command
        /// </summary>
        /// <param name="username">Full name or email address</param>
        /// <param name="password">Password</param>
        /// <returns>An authentication response. This may be of type <see cref="AuthenticationErrorResponse"/> if an authntication was returned</returns>
        public async Task<AuthenticationResponse> LogonAsync(string username, string password)
        {
            LogonCommand command = new LogonCommand(username, password);
            var response = await ExecuteAsync(command);
            if (response.Token != null)
            {
                Token = response.Token;
            }

            return response;
        }

        /// <summary>
        /// Convenience method to execute a logoff command
        /// </summary>
        /// <returns>FogBugzReturn response received</returns>
        public async Task<FogBugzReturn> LogoffAsync()
        {
            return await ExecuteAsync(new LogoffCommand());
        }

        /// <summary>
        /// Convenience method to execute a validate token command
        /// </summary>
        /// <param name="token">Token to validate</param>
        /// <returns>An authentication response. This may be of type <see cref="AuthenticationErrorResponse"/> if an authntication was returned</returns>
        public async Task<AuthenticationResponse> ValidateTokenAsync(string token)
        {
            ValidateTokenCommand command = new ValidateTokenCommand(token);
            return await ExecuteAsync(command);
        }
    }
}
