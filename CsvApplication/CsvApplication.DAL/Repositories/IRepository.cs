using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvApplication.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task Delete(int id);
        public Task SaveAsync();
    }
}
