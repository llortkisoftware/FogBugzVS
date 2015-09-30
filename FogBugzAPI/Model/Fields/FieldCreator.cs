using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace FogBugzAPI.Model.Fields {
    public abstract class FieldCreator<TEnum, TCreatable> : IFieldCreator<TEnum, TCreatable> 
        where TEnum : struct, IConvertible, IComparable, IFormattable 
        where TCreatable : Field<TEnum, TCreatable>  {

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")] private static readonly Hashtable LookupTable = new Hashtable();
        
        protected FieldCreator() { } 
        
        protected static void AddToLookupTable(Field<TEnum, TCreatable> field)
        {
            LookupTable.Add(field.FieldName, field);
        }

        public string GetFogBugzName(TEnum fieldName)
        {
            return ((Field<TEnum, TCreatable>) LookupTable[fieldName]).FogBugzName;
        }

        public TCreatable CreateField(TEnum fieldName)
        {
            return ((Field<TEnum, TCreatable>) LookupTable[fieldName]).CreateNew();
        }
        
    }
}