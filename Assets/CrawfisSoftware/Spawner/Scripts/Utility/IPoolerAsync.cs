using System.Threading.Tasks;

namespace CrawfisSoftware
{
    public interface IPoolerAsync<T> where T : class
    {
        Task<T> GetAsync();
        Task ReleaseAsync(T poolObject);
    }
}