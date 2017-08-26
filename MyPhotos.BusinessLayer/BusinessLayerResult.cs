using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhotos.Entities;


namespace MyPhotos.BusinessLayer
{

    public class BusinessLayerResult<T>where T :class
    {
        public List<ErrorCodeObj> Errors { get; set; }
        //Hata Yoksa
        public T Result { get; set; }


        public BusinessLayerResult()
        {
            Errors = new List<ErrorCodeObj>();
        }

        public void AddError(string message)
        {
           


            Errors.Add(new ErrorCodeObj {Message=message });
        }

        

    }
}
