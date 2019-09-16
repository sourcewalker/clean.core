using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Infrastructure.Interfaces.Caching
{
    public interface ICacheProvider
    {

        void Add<T>(string key, T data, double expiry = 6) where T : class;

        T Get<T>(string key, double expiry = 6) where T : class;

        Task<T> GetAsync<T>(string key, double expiry = 6) where T : class;

        bool ContainsKey(string key);

        IEnumerable<string> GetAllCacheKeys();

        void Purge();

        void Remove(string key);
    }
}
