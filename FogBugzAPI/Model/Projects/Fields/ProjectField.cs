using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogBugzAPI.Model.Cases.Fields;
using FogBugzAPI.Model.Fields;

namespace FogBugzAPI.Model.Projects.Fields
{
    public class ProjectField : FieldBase<ProjectFieldName, ProjectField>
    {
        public ProjectField() { }

        private ProjectField(ProjectFieldName caseFieldName, FieldType fieldType, string fogBugzName) : base(caseFieldName, fieldType, fogBugzName)
        {
            AddToLookupTable(new ProjectField(ProjectFieldName.ProjectId, FieldType.Integer, "ixProject"));

            AddToLookupTable(new ProjectField(ProjectFieldName.ProjectName, FieldType.String, "sProject"));
            AddToLookupTable(new ProjectField(ProjectFieldName.PersonOwnerId, FieldType.Integer, "ixPersonOwner"));
            AddToLookupTable(new ProjectField(ProjectFieldName.OwnerName, FieldType.String, "sPersonOwner"));
            AddToLookupTable(new ProjectField(ProjectFieldName.OwnerEmail, FieldType.String, "sEmail"));
            AddToLookupTable(new ProjectField(ProjectFieldName.OwnerPhone, FieldType.String, "sPhone"));
            AddToLookupTable(new ProjectField(ProjectFieldName.IsInbox, FieldType.Boolean, "fInbox"));
            AddToLookupTable(new ProjectField(ProjectFieldName.ProjectGroupTypeId, FieldType.Integer, "iType"));
            AddToLookupTable(new ProjectField(ProjectFieldName.ProjectGroupName, FieldType.String, "sGroup"));

            AddToLookupTable(new ProjectField(ProjectFieldName.ProjectDeleted, FieldType.Boolean, "fDeleted"));
            AddToLookupTable(new ProjectField(ProjectFieldName.PublicSubmissionEmail, FieldType.String, "sPublicSubmitEmail"));
        }

        public override ProjectField CreateNew()
        {
            return new ProjectField(FieldName, FieldType, FogBugzName);
        }

        public override ProjectField CreateNew(string value)
        {
            return new ProjectField(FieldName, FieldType, FogBugzName);
        }
    }
}
