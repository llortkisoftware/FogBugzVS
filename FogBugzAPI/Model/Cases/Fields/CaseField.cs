using FogBugzAPI.Model.Fields;

namespace FogBugzAPI.Model.Cases.Fields
{
    public class CaseField : Field<CaseFieldName, CaseField> {
        internal CaseField(CaseFieldName caseFieldName, FieldType fieldType, string fogBugzName) : base(caseFieldName, fieldType, fogBugzName)
        {
        }

        public override CaseField CreateNew()
        {
            return new CaseField(FieldName, FieldType, FogBugzName);
        }

        public override CaseField CreateNew(string value)
        {
            CaseField newField = new CaseField(FieldName, FieldType, FogBugzName);
            newField.Value.FromString(value);
            return newField;
        }
    }
}