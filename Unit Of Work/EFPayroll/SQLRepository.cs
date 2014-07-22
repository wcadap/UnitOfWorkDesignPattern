using System.Data.Entity;
using System.Linq;
using Unit_Of_Work.DataModel;
using Unit_Of_Work.Interface;

namespace Unit_Of_Work.EFPayroll
{
    public class SQLRepository<T> : IGenericRepository<T> where T : class
    {
        internal EmployeeContext Context;
        internal DbSet<T> dbSet;

        public SQLRepository(EmployeeContext _Context)
        {
            Context = _Context;
            dbSet = Context.Set<T>();
            
        }
        public IQueryable<T> Query()
        {
            IQueryable<T> query = dbSet;
            return dbSet;
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
    }
}
