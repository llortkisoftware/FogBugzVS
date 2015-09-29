using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using FogBugzAPI.FogBugzClient;

namespace FogBugzAPI.Model
{
    public class ApiVersion : FogBugzReturn, IFogBugzType
    {
        public int Version { get; private set; }
        public int MinimumCompatibleApi { get; private set; }
        public string ApiLocation { get; private set; }

        public ApiVersion(XDocument xmlDocument) : base(xmlDocument)
        {
            Version = Convert.ToInt32(xmlDocument.XPathSelectElement("/response/version").Value);
            MinimumCompatibleApi = Convert.ToInt32(xmlDocument.XPathSelectElement("/response/minversion").Value);
            ApiLocation = xmlDocument.XPathSelectElement("/response/url").Value;
        }
    }
}
