using System.Xml.Linq;
using System.Xml.XPath;

namespace FogBugzAPI.Model {
    public class AuthenticationErrorResponse : AuthenticationResponse
    {

        public string ErrorResponse { get; private set; }

        public AuthenticationErrorResponse(XDocument xmlDocument)
        {
            ErrorResponse = xmlDocument.XPathSelectElement("/response/error").Value;
        }

    }
}