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
        public CaseList(FogBugzReturn fogBugzReturm)
        {
            AddRange(fogBugzReturm.XmlDocument.XPathSelectElements("response/cases/case").Select(c => new Case(c)));
        }
    }
}