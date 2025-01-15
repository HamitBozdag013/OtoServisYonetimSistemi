using OtoServisYonetimSistemi.BusinessLayer.Abstract;
using OtoServisYonetimSistemi.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisYonetimSistemi.BusinessLayer.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext DB = new DataContext();
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = DB.Set<T>();
        }

        public void Add(T entity)
        {
            _objectSet.Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
            Save();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = "")
        {
            IQueryable<T> query = _objectSet;
            if (filter!=null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            if (orderby!=null)
            {
                return orderby(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(int id)
        {
            return _objectSet.Find(id);
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public void Save()
        {
            DB.SaveChanges();
        }

        public void Update(T entity)
        {
            _objectSet.Attach(entity);
            DB.Entry(entity).State = EntityState.Modified;
            Save();
        }
    }
}
