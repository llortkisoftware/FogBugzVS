using System.Collections.Generic;
using FogBugzAPI.Model;

namespace FogBugzAPI.FogBugzClient.Command
{
    public class ValidateTokenCommand : IFogBugzCommand<AuthenticationResponse>, IFogBugzType
    {
        public ValidateTokenCommand(string token)
        {
            Parameters.Add(new KeyValuePair<string, string>("token", token));
        }
        public string Command => "logon";

        public List<KeyValuePair<string, string>> Parameters { get; } = new List<KeyValuePair<string, string>>();

        public AuthenticationResponse CreateResponse(FogBugzReturn fogBugzReturn)
        {
            return new AuthenticationResponse(fogBugzReturn.XmlDocument);
        }
    }


}
