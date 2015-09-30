using System;

namespace FogBugzAPI.Model.Cases.Fields
{
    public class FieldValueBool : FieldValue
    {
        public FieldValueBool()
        {
            Value = false;
        }

        public override void FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Value = false;
                return;
            }

            Value = Convert.ToBoolean(value);
        }

        public override string ToStringValue()
        {
            return Value.ToString().ToLower();
        }
    }
}