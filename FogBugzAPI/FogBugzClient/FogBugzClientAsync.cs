using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
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
        /// <exception cref="FogBugzException">Throws FogBugzException if API vesion is less than the supported api version</exception>
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

        //Convenience methods for logon and logoff so the token is automatically set.
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

        public async Task<FogBugzReturn> LogoffAsync()
        {
            return await ExecuteAsync(new LogoffCommand());
        }

        public async Task<AuthenticationResponse> ValidateTokenAsync(string token)
        {
            ValidateTokenCommand command = new ValidateTokenCommand(token);
            return await ExecuteAsync(command);
        }
    }

    public class FogBugzReturn : IFogBugzType
    {

        public Boolean IsError { get; private set; }
        public XDocument XmlDocument { get; private set; }

        public FogBugzReturn(XDocument xmlDocument)
        {
            XmlDocument = xmlDocument;
            if (xmlDocument.XPathSelectElement("/response/error") != null)
            {
                IsError = true;
            }
        }
    }
}
