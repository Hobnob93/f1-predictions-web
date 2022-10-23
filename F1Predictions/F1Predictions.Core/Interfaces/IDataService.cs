namespace F1Predictions.Core.Interfaces
{
    public interface IDataService<T>
    {
        List<T> Data { get; }

        T FindItem(string id);

        Task Initialize();
    }
}
