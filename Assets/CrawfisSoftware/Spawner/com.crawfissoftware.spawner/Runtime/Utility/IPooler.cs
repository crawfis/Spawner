namespace CrawfisSoftware
{
    public interface IPooler<T> //where T : class
    {
        T Get();
        void Release(T poolObject);
    }
}