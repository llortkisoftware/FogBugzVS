namespace FogBugzAPI.Model.Fields {
    public interface ICreatableField<out T> {
        T CreateNew();
        T CreateNew(string value);
    }
}