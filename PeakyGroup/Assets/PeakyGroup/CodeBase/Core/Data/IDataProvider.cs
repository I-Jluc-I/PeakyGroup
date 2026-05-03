namespace SelectionSystem.Core.Data
{
    public interface IDataProvider<out T> where T : ISelectable
    {
        T GetData(string id);
        string[] GetAllIds();
    }
}