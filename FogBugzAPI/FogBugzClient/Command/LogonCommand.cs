using System.Collections.Generic;
using FogBugzAPI.Model;

namespace FogBugzAPI.FogBugzClient.Command {
    public class LogonCommand : IFogBugzCommand<AuthenticationResponse>, IFogBugzType
    {

        internal LogonCommand(FogBugzUrl defaultUrl) : this(defaultUrl.DefaultUsername, defaultUrl.DefaultPassword)
        { }

        internal LogonCommand(string username, string password)
        {
            Parameters.Add(new KeyValuePair<string, string>("email", username));
            Parameters.Add(new KeyValuePair<string, string>("password", password));
        }

        public string Command => "logon";

        public List<KeyValuePair<string, string>> Parameters { get; } = new List<KeyValuePair<string, string>>();

        public AuthenticationResponse CreateResponse(FogBugzReturn fogBugzReturn)
        {
            if (fogBugzReturn.IsError)
            {
                return new AuthenticationErrorResponse(fogBugzReturn.XmlDocument);
            }
            else
            {
                return new AuthenticationResponse(fogBugzReturn.XmlDocument);
            }
        }
    }
}