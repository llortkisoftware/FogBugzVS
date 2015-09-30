using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogBugzAPI.Model.Cases.Fields;
using FogBugzAPI.Model.Fields;

namespace FogBugzAPI.Model.Projects.Fields
{
    public class ProjectField : Field<ProjectFieldName, ProjectField> {
        internal ProjectField(ProjectFieldName caseFieldName, FieldType fieldType, string fogBugzName) : base(caseFieldName, fieldType, fogBugzName)
        { }

        public override ProjectField CreateNew()
        {
            return new ProjectField(FieldName, FieldType, FogBugzName);
        }

        public override ProjectField CreateNew(string value)
        {
            ProjectField field = new ProjectField(FieldName, FieldType, FogBugzName);
            field.Value.FromString(value);
            return field;
        }
    }
}
