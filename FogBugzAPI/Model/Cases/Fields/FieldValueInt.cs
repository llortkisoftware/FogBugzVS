using System;

namespace FogBugzAPI.Model.Cases.Fields
{
    public class FieldValueInt : FieldValue
    {

        public FieldValueInt()
        {
            Value = 0;
        }

        public override void FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Value = 0;
                return;
            }

            Value = Convert.ToInt32(value);
        }

    }
}