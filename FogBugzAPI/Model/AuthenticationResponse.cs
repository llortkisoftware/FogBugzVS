using System.Xml.Linq;
using System.Xml.XPath;
using FogBugzAPI.FogBugzClient;

namespace FogBugzAPI.Model {
    public class AuthenticationResponse : IFogBugzType
    {

        public string Token { get; private set; }

        protected AuthenticationResponse() { }
        public AuthenticationResponse(XDocument xmlDocument)
        {
            Token = xmlDocument.XPathSelectElement("/response/token")?.Value;
        }
    }
}