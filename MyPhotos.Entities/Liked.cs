using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyPhotos.Entities
{
    public class Liked
    {
        public int Id { get; set; }

        public virtual Photo Photo { get; set; }

        public virtual MyPhotosUser LikedUser { get; set; }
    }
}
