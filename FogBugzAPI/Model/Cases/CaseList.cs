using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using FogBugzAPI.FogBugzClient;
using FogBugzAPI.Model.Cases;

namespace FogBugzAPI.Model
{
    public class CaseList : List<Case>, IFogBugzType
    {
        public string FilterDescription { get; private set; }

        public CaseList(FogBugzReturn fogBugzReturm)
        {
            XElement descriptionElement = fogBugzReturm.XmlDocument.XPathSelectElement("response/description");
            if (descriptionElement != null)
            {
                FilterDescription = descriptionElement.Value;
            }
            else
            {
                FilterDescription = "Search results";
            }
            AddRange(fogBugzReturm.XmlDocument.XPathSelectElements("response/cases/case").Select(c => new Case(c)));
        }
    }
}