using System.Linq;

namespace Unit_Of_Work.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Query();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
