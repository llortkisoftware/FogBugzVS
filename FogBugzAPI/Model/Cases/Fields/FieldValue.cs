namespace FogBugzAPI.Model.Cases.Fields
{
    public class FieldValue
    {
        public FieldValue() { }

        public object Value { get; set; }

        public virtual void FromString(string value) { Value = value; }
        public virtual string ToStringValue()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Performs a direct cast to type T. Will throw a cast exception if the Value cannot be cast to the requested type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return (T)Value;
        }
    }
}