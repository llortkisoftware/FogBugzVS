using System.Xml.Linq;
using FogBugzAPI.Model.Cases;
using FogBugzAPI.Model.Projects.Fields;

namespace FogBugzAPI.Model.Projects
{
    public class Project : FogBugzObject<ProjectFieldName, ProjectField, ProjectField>
    {
        public Project(XElement projectElement) : base(projectElement, ProjectField.GetInstance())
        {
        }
    }
}
