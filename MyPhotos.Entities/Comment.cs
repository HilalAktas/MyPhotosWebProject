using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MyPhotos.Entities
{
   public class Comment:EntityBase
    {   [MaxLength(300)]
        public string Text { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual MyPhotosUser Owner { get; set; }


    }
}
