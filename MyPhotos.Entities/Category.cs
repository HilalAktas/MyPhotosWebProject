using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MyPhotos.Entities
{
   public class Category:EntityBase
    {   [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }

        public string CategoryPhoto { get; set; }
        public virtual List<Photo> Photos { get; set; }
    }
}
