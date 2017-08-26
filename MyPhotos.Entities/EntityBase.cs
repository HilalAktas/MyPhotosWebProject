using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyPhotos.Entities
{
    public class EntityBase
    {   
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime  ModifiedOn { get; set; }
        public string ModifiedUser { get; set; }
    }
}
