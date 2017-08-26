using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyPhotos.Entities
{
  public class Photo:EntityBase
    {

        public string Title { get; set; }


        public string PhotoPath { get; set; }
        public int LikeCount { get; set; }

        public virtual MyPhotosUser Owner { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual Category Category { get; set; }
  
        public int CategoryID { get; set; }


        public virtual List<Liked> Like { get; set; }
    }
}
