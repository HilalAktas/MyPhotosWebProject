using MyPhotos.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotos.DataAccessLayer.EntityFramework
{
   public class RepositoryBase
    {
        protected static DatabaseContext db;
        public static object _lock = new object();

        protected RepositoryBase()
        {
            CreateContext();

        }

        private static void CreateContext()
        {
            if (db == null)
            {
                lock (_lock)
                {
                    if (db == null)
                    {
                        db = new DatabaseContext();
                    }
                }

            }
            
        }

    }
}
