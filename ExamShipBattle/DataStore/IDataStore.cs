namespace ExamShipBattle.DataStore
{
    internal interface IDataStore<T>
    {
        bool AddItem(T item);
        bool UpdateItem(T item);
        string[] GetField(int id);
    }
}
