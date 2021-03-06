using System;

namespace FogBugzAPI.Model.Fields {
    public interface IFieldCreator<in TEnum, out TCreatable> 
        where TEnum : struct, IConvertible, IComparable, IFormattable 
        where TCreatable : ICreatableField<TCreatable> {
        TCreatable CreateField(TEnum fieldName);
        string GetFogBugzName(TEnum fieldName);
    }
}