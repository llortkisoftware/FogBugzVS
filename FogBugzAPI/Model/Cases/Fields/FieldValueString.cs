namespace FogBugzAPI.Model.Cases.Fields
{
    public class FieldValueString : FieldValue
    {

        public FieldValueString()
        {
            Value = "";
        }

        public override void FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Value = "";
            }

            Value = value;
        }

    }
}