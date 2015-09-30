using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using FogBugzAPI.FogBugzClient;

namespace FogBugzAPI.Model.Projects {
    public class ProjectList : List<Project>
    {
        public ProjectList(FogBugzReturn fogBugzReturn)
        {
            AddRange(fogBugzReturn.XmlDocument.XPathSelectElements("response/projects/project").Select(project => new Project(project)));
        }
    }
}