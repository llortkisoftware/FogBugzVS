using System;
using System.Globalization;

namespace FogBugzAPI.Model.Cases.Fields
{
    public class FieldValueDouble : FieldValue
    {

        public FieldValueDouble()
        {
            Value = 0d;
        }

        public override void FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Value = 0d;
                return;
            }

            Value = Convert.ToDouble(value);
        }

        public override string ToStringValue()
        {
            return ((double)Value).ToString(CultureInfo.InvariantCulture);
        }
    }
}