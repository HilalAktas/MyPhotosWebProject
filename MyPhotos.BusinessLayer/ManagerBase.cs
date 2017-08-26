using MyPhotosCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MyPhotos.DataAccessLayer.EntityFramework;

namespace MyPhotos.BusinessLayer
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T :class
    {
        private Repository<T> repo = new Repository<T>();

        public int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public List<T> Select()
        {
            return repo.Select();
        }

        public int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
