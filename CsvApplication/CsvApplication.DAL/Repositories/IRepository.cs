using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvApplication.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(string id);
        public Task Delete(string id);
        public Task SaveAsync();
    }
}
