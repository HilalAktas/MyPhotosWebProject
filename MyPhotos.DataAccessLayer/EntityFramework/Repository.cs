using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using MyPhotos.Entities;
using MyPhotos.Common;
using MyPhotosCore.DataAccess;


namespace MyPhotos.DataAccessLayer.EntityFramework
{
   public class Repository<T>:RepositoryBase, IDataAccess<T> where T:class
    {
       
        private DbSet<T> _obj;

        public Repository()
        {
            _obj = db.Set<T>();
        }

        public List<T> Select()
        {
          return _obj.ToList();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Insert(T obj)
        {
            if (obj is EntityBase)
            {
                EntityBase o = obj as EntityBase;

                o.ModifiedOn = DateTime.Now;
                o.ModifiedUser = App.Common.GetCurrentUserName();
                o.CreatedOn = DateTime.Now;
              

                T a = o as T;

                _obj.Add(a);

           
            }

            else _obj.Add(obj);


            return Save();
        
        }

        public int Update(T obj) {
          
            return Save();
        }

        public int Delete(T entity)
        {
            _obj.Remove(entity);
            return Save();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _obj.FirstOrDefault(where);
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _obj.Where(where).ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _obj.AsQueryable<T>();
        }
    }
}
