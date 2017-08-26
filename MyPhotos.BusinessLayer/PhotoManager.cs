using MyPhotos.DataAccessLayer.EntityFramework;
using MyPhotos.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotos.BusinessLayer
{
  public  class PhotoManager:ManagerBase<Photo>
    {
         
         
        public List<Photo> GetPhoto()
        {
            return Select();
        }
    }
}
